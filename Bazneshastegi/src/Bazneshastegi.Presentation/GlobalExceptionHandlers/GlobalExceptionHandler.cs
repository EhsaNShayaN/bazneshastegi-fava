using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace Bazneshastegi.Presentation.GlobalExceptionHandlers;
public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        this._logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        try
        {
            this._logger.LogCritical(exception, "Exception occurred: {Message}", exception.Message);

            var exceptionType = exception.GetType();

            if (exception is BadHttpRequestException)
            {
                httpContext.Response.StatusCode = HttpStatusCode.BadRequest.GetHashCode();
                await httpContext.Response
                    .WriteAsJsonAsync(
                    DefaultApiResponse.Failure(string.Empty, new DefaultApiError("Error", "Bad request")), cancellationToken);
                return true;
            }

            await httpContext.Response
                    .WriteAsJsonAsync(
                    DefaultApiResponse.Failure(string.Empty, new DefaultApiError("Error", "Server Error")), cancellationToken);
        }
        finally
        {
            try
            {
                await LogError(httpContext, exception, cancellationToken);
            }
            catch
            {

            }
        }
        return true;
    }

    static async Task LogError(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var sb = new StringBuilder();

        sb.AppendLine("========================================");
        sb.AppendLine("ERROR");
        sb.AppendLine("========================================");

        // Date / Time
        sb.AppendLine($"DateTime      : {DateTimeOffset.Now:yyyy/MM/dd HH:mm:ss.fff}");
        sb.AppendLine($"UtcDateTime   : {DateTimeOffset.UtcNow:yyyy/MM/dd HH:mm:ss.fff}");

        // Request
        sb.AppendLine();
        sb.AppendLine("------------- REQUEST ------------------");
        sb.AppendLine($"Method        : {httpContext.Request.Method}");
        sb.AppendLine($"Scheme        : {httpContext.Request.Scheme}");
        sb.AppendLine($"Host          : {httpContext.Request.Host}");
        sb.AppendLine($"PathBase      : {httpContext.Request.PathBase}");
        sb.AppendLine($"Path          : {httpContext.Request.Path}");
        sb.AppendLine($"QueryString   : {httpContext.Request.QueryString}");
        sb.AppendLine($"URL           : {httpContext.Request.GetDisplayUrl()}");
        sb.AppendLine($"Protocol      : {httpContext.Request.Protocol}");
        sb.AppendLine($"ContentType   : {httpContext.Request.ContentType}");
        sb.AppendLine($"ContentLength : {httpContext.Request.ContentLength}");
        sb.AppendLine($"IsHttps       : {httpContext.Request.IsHttps}");

        // Remote information
        sb.AppendLine();
        sb.AppendLine("------------- CONNECTION ---------------");
        sb.AppendLine($"RemoteIP      : {httpContext.Connection.RemoteIpAddress}");
        sb.AppendLine($"RemotePort    : {httpContext.Connection.RemotePort}");
        sb.AppendLine($"LocalIP       : {httpContext.Connection.LocalIpAddress}");
        sb.AppendLine($"LocalPort     : {httpContext.Connection.LocalPort}");

        // Headers
        sb.AppendLine();
        sb.AppendLine("------------- HEADERS ------------------");

        foreach (var header in httpContext.Request.Headers)
        {
            sb.AppendLine($"{header.Key}: {header.Value}");
        }

        // Query parameters
        sb.AppendLine();
        sb.AppendLine("------------- QUERY --------------------");

        foreach (var query in httpContext.Request.Query)
        {
            sb.AppendLine($"{query.Key}: {query.Value}");
        }

        // User
        sb.AppendLine();
        sb.AppendLine("------------- USER ---------------------");
        sb.AppendLine($"IsAuthenticated : {httpContext.User?.Identity?.IsAuthenticated}");
        sb.AppendLine($"Authentication  : {httpContext.User?.Identity?.AuthenticationType}");
        sb.AppendLine($"UserName        : {httpContext.User?.Identity?.Name}");

        if (httpContext.User?.Claims != null)
        {
            foreach (var claim in httpContext.User.Claims)
            {
                sb.AppendLine($"{claim.Type}: {claim.Value}");
            }
        }

        // Cancellation
        sb.AppendLine();
        sb.AppendLine("------------- CANCELLATION --------------");
        sb.AppendLine($"IsCancellationRequested : {cancellationToken.IsCancellationRequested}");

        // Exception
        sb.AppendLine();
        sb.AppendLine("------------- EXCEPTION ----------------");
        sb.AppendLine($"Type          : {exception.GetType().FullName}");
        sb.AppendLine($"Message       : {exception.Message}");
        sb.AppendLine($"Source        : {exception.Source}");
        sb.AppendLine($"HResult       : {exception.HResult}");
        sb.AppendLine($"TargetSite    : {exception.TargetSite}");
        sb.AppendLine($"StackTrace    : {exception.StackTrace}");

        // Inner exceptions
        var innerException = exception.InnerException;
        var level = 1;

        while (innerException != null)
        {
            sb.AppendLine();
            sb.AppendLine($"------------- INNER EXCEPTION {level} --------");

            sb.AppendLine($"Type          : {innerException.GetType().FullName}");
            sb.AppendLine($"Message       : {innerException.Message}");
            sb.AppendLine($"Source        : {innerException.Source}");
            sb.AppendLine($"HResult       : {innerException.HResult}");
            sb.AppendLine($"TargetSite    : {innerException.TargetSite}");
            sb.AppendLine($"StackTrace    : {innerException.StackTrace}");

            innerException = innerException.InnerException;
            level++;
        }

        // Request body
        sb.AppendLine();
        sb.AppendLine("------------- REQUEST BODY -------------");

        try
        {
            if (httpContext.Request.ContentLength > 0 &&
                httpContext.Request.Body.CanRead)
            {
                httpContext.Request.EnableBuffering();

                httpContext.Request.Body.Position = 0;

                using var reader = new StreamReader(
                    httpContext.Request.Body,
                    Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    leaveOpen: true);

                var body = await reader.ReadToEndAsync(cancellationToken);

                httpContext.Request.Body.Position = 0;

                sb.AppendLine(body);
            }
            else
            {
                sb.AppendLine("[Empty]");
            }
        }
        catch (Exception bodyException)
        {
            sb.AppendLine($"[Could not read body: {bodyException.Message}]");
        }

        sb.AppendLine();
        sb.AppendLine("========================================");
        sb.AppendLine();

        // Create log directory
        var logDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "Logs",
            "Errors");

        Directory.CreateDirectory(logDirectory);

        // One new file per error
        var fileName =
            $"{DateTimeOffset.Now:yyyyMMdd_HHmmss_fff}_{Guid.NewGuid():N}.log";

        var filePath = Path.Combine(logDirectory, fileName);

        await File.WriteAllTextAsync(
            filePath,
            sb.ToString(),
            Encoding.UTF8,
            cancellationToken);
    }
}
