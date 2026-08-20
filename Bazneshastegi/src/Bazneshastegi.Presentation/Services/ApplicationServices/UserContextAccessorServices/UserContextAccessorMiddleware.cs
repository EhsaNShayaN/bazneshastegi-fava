using Bazneshastegi.Application.Services.UserAuthenticationServices;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bazneshastegi.Presentation.Services.ApplicationServices.UserContextAccessorServices;

public sealed class UserContextAccessorMiddleware
{
    const string AuthorizationHeaderName = "Authorization";
    private readonly RequestDelegate _next;

    public UserContextAccessorMiddleware(RequestDelegate requestDelegate)
    {
        this._next = requestDelegate;
    }

    public async Task Invoke(
        HttpContext httpContext,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<UserContextAccessorMiddleware> logger)
    {
        UserContext currentUser = UserContext.Guest;
        IUserContextAccessor? userContextAccessor = null;
        try
        {
            using var scope = serviceScopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            userContextAccessor = serviceProvider.GetRequiredService<IUserContextAccessor>();
            var userAuthenticationTokenService = serviceProvider.GetRequiredService<IUserAuthenticationTokenService>();

            if (httpContext.Request.Headers.TryGetValue(AuthorizationHeaderName, out var authHeader)
                && !string.IsNullOrWhiteSpace(authHeader))
            {
                var tokenItems = authHeader.ToString().Split(" ");
                if (tokenItems.Length == 2 && tokenItems.First().Equals("bearer", StringComparison.InvariantCultureIgnoreCase))
                {
                    var userId = await userAuthenticationTokenService.ValidateToken(tokenItems.Last())
                        .Map(t => t.FindFirst("id")?.Value ?? string.Empty)
                        .Match(
                            s => s,
                            _ => string.Empty
                        )
                        .ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(userId))
                    {
                        currentUser = new UserContext(userId);
                    }
                }
            }

            userContextAccessor.Current = currentUser;
            await this._next(httpContext);
        }
        catch
        {
            throw;
        }
        finally
        {
            if (userContextAccessor is not null)
            {
                userContextAccessor.Current = null;
            }
        }
    }
}