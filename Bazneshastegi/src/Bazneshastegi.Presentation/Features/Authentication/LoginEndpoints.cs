using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bazneshastegi.Presentation.Features.Authentication;
public static partial class LoginEndpoints
{
    const string TagName = "Authentication";

    static List<Func<IEndpointRouteBuilder, RouteHandlerBuilder>> _routeHandlerBuilders = new();
    static LoginEndpoints()
    {
        SSOLogin();
        SSOLoginRedirect();
        SSOTest();
    }
    static string GetPath() => Path.Combine(AppContext.BaseDirectory, $"response.txt");
    public static void AddAllEndpoitnts(WebApplication app, RouteGroupBuilder? group = null)
    {
        IEndpointRouteBuilder routeBuilder = group ?? (IEndpointRouteBuilder)app;

        foreach (var route in _routeHandlerBuilders)
        {
            route.Invoke(routeBuilder);
        }
    }
    static void SSOLogin()
    {
        _routeHandlerBuilders.Add((app) =>
        {
            var builder = app.MapGet("api/v1/sso/login",
                async (
                    IOptionsSnapshot<SSLOptions> opts,
                    CancellationToken cancellationToken) =>
                {
                    var optsValue = opts.Value;
                    var state = Guid.NewGuid().ToString("N");
                    var builder = new UriBuilder(optsValue.LoginUrl);
                    builder.AddQuery("response_type", "code");
                    builder.AddQuery("client_id", optsValue.ClientId);
                    builder.AddQuery("redirect_uri", optsValue.RedirectUrl);
                    builder.AddQuery("scope", optsValue.Scope);
                    builder.AddQuery("state", state);
                    return TypedResults.Redirect(builder.Uri.ToString());
                }).WithTags(TagName);
            return builder;
        });
    }
    static void SSOLoginRedirect()
    {
        _routeHandlerBuilders.Add((app) =>
        {
            var builder = app.MapGet("api/v1/sso/callback",
                async (
                    IOptionsSnapshot<SSLOptions> opts,
                    IHttpClientFactory httpClientFactory,
                    IHttpContextAccessor httpContextAccessor,
                    CancellationToken cancellationToken) =>
                {
                    if (!httpContextAccessor.HttpContext!.Request.Query.TryGetValue("state", out var state))
                    {

                    }
                    if (!httpContextAccessor.HttpContext!.Request.Query.TryGetValue("code", out var code))
                    {

                    }
                    using var client = httpClientFactory.CreateClient();
                    /*var tokenResponse2 = client.RequestAuthorizationCodeTokenAsync(new AuthorizationCodeTokenRequest
                    {
                        Address = "https://Url/ApiContainer.SSO.RCL1/connect/token",
                        ClientId = "ClientID",
                        ClientSecret = "ClientSecret",
                        Code = code,
                        RedirectUri = "Url",
                    }).Result;*/
                    var request = new HttpRequestMessage(HttpMethod.Post, opts.Value.AccessTokenUrl);
                    var parameters = new Dictionary<string, string>
                    {
                        ["Address"] = opts.Value.AccessTokenUrl,
                        ["client_id"] = opts.Value.ClientId,
                        ["ClientSecret"] = opts.Value.ClientSecret,
                        ["code"] = code!,
                    };
                    request.Content = new FormUrlEncodedContent(parameters);
                    try
                    {
                        var response = await client.SendAsync(
                            request,
                            cancellationToken).ConfigureAwait(false);
                        var content = await response.Content.ReadAsStringAsync(cancellationToken);
                        using StreamWriter outputFile = new(GetPath());
                        await outputFile.WriteAsync(content);
                        outputFile.Close();
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception($"Token request failed: {response.StatusCode} - {content}");
                        }
                        /*var resp = JsonSerializer.Deserialize<SSOTokenResponse>(
                           content,
                           new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                        var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                        var jwtToken = handler.ReadJwtToken(resp.Token);
                        var UserName = jwtToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value;*/
                    }
                    catch (Exception ex)
                    {
                        using StreamWriter outputFile = new(GetPath());
                        await outputFile.WriteAsync("Error: " + ex.GetBaseException().Message);
                        outputFile.Close();
                    }
                }).WithTags(TagName);
            return builder;
        });
    }
    static void SSOTest()
    {
        _routeHandlerBuilders.Add((app) =>
        {
            var builder = app.MapGet("api/v1/sso/test",
                async (CancellationToken cancellationToken) =>
                {
                    string docPath = Directory.GetCurrentDirectory();
                    var result = File.ReadAllText(GetPath());
                    return TypedResults.Ok(result);
                }).WithTags(TagName);
            return builder;
        });
    }
}

public sealed class SSLOptions
{
    public string LoginUrl { get; set; } = string.Empty;
    public string LogoutUrl { get; set; } = string.Empty;
    public string AccessTokenUrl { get; set; } = string.Empty;
    public string ClientId { get; set; } = string.Empty;
    public string ApiClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public string RedirectUrl { get; set; } = string.Empty;
}
static class UrlBuilderExtensions
{
    public static UriBuilder AddQuery(this UriBuilder src, string key, object value)
    {
        var query = System.Web.HttpUtility.ParseQueryString(src.Query);

        if (value is not null)
            query[key] = value.ToString();

        src.Query = query.ToString();

        return src;
    }
}
public sealed class SSOTokenResponse0
{
    [JsonPropertyName("access_token")]
    public string? Token { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("scope")]
    public string? Scope { get; set; }

    [JsonPropertyName("id_token")]
    public string? IdToken { get; set; }
}
public sealed class SSOTokenResponse
{
    [Display(Name = "nameid")]
    public long? Id { get; set; }
    [Display(Name = "unique_name")]
    public string? UserName { get; set; }
    [Display(Name = "DisplayName")]
    public string? DisplayName { get; set; }
    [Display(Name = "exp")]
    public DateTime? EXP { get; set; }
    [Display(Name = "UserType")]
    public string? Type { get; set; }
    [Display(Name = "Post")]
    public string? Post { get; set; }
    [Display(Name = "Permission")]
    public string? Permission { get; set; }
    [Display(Name = "nbf")]
    public string? NotBefore { get; set; }
    [Display(Name = "iat")]
    public string? IssuedAt { get; set; }
    [Display(Name = "iss")]
    public string? Issuer { get; set; }
    [Display(Name = "aud")]
    public string? Audience { get; set; }
}
