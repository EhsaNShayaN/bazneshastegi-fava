using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Bazneshastegi.Presentation.AppCore;

public interface IResultHandler
{
    IResult Handle<T>(PrimitiveResult<T> result);
}

public sealed class ResultHandler : IResultHandler
{
    private readonly ILogger<ResultHandler> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHostEnvironment _env;

    public ResultHandler(
        ILogger<ResultHandler> logger,
        IHttpContextAccessor httpContextAccessor,
        IHostEnvironment env)
    {
        this._logger = logger;
        this._httpContextAccessor = httpContextAccessor;
        this._env = env;
    }

    public IResult Handle<T>(PrimitiveResult<T> result)
    {
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        var httpContext = this._httpContextAccessor.HttpContext!;
        string traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        string path = httpContext.Request.Path;

        // Log all errors for support/dev team
        this._logger.LogError("Request failed at {Path}, TraceId: {TraceId}, Errors: {@Errors}", path, traceId, result.Errors);

        var problemDetails = new ProblemDetails
        {
            Status = GetStatus(result.Error),
            Title = "Request failed",
            Detail = GetMessage(result.Errors),
            Instance = path
        };

        problemDetails.Extensions["traceId"] = traceId;
        problemDetails.Extensions["isFailure"] = result.IsFailure;
        problemDetails.Extensions["errors"] = result.Errors.Select(e => new
        {
            e.Code,
            e.Message,
            e.Internal
        });

        return Results.Problem(
            title: problemDetails.Title,
            detail: problemDetails.Detail,
            statusCode: problemDetails.Status,
            instance: problemDetails.Instance
        );
    }

    static string GetMessage(PrimitiveError[] errors)
    {
        var visibleErrors = errors.Where(e => !e.Internal).Select(e => e.Message).ToList();
        return
            visibleErrors.Any()
            ? string.Join(Environment.NewLine, visibleErrors)
            : "An error occurred";
    }
    static int GetStatus(PrimitiveError error)
    {
        var result = StatusCodes.Status500InternalServerError;

        if (error.Code.Equals(DomainErrorCodes.AccessDenied_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status401Unauthorized; //401

        if (error.Code.Equals(DomainErrorCodes.UnhandledException_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status500InternalServerError; //500

        if (error.Code.Equals(DomainErrorCodes.RpcException_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status503ServiceUnavailable; //503

        if (error.Code.Equals(DomainErrorCodes.InvalidCaptcha_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status412PreconditionFailed; //412

        if (error.Code.Equals(DomainErrorCodes.BadRequest_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status400BadRequest; //400

        if (error.Code.Equals(DomainErrorCodes.Validation_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status400BadRequest; //400

        if (error.Code.Equals(DomainErrorCodes.Concurrency_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status409Conflict; //409

        return result;
    }
}