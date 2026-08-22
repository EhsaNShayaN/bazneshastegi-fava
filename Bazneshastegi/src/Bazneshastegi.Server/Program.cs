using Bazneshastegi.Application.Options;
using Bazneshastegi.Infrastructure;
using Bazneshastegi.Presentation;
using Bazneshastegi.Presentation.Abstraction;
using Bazneshastegi.Presentation.Features.Authentication;
using Bazneshastegi.Presentation.Services.ApplicationServices.UserContextAccessorServices;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SRH.PrimitiveTypes.Result;
using SRH.ServiceInstaller;

var builder = WebApplication.CreateBuilder(args);

var processEnv = CommandArgReader.Read(
    builder.Configuration,
    "env",
    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development");
Console.WriteLine($"Process Env = {processEnv}");

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
    options.Limits.MaxConcurrentConnections = 10000;  // Set the number of concurrent connections
    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;  // 10 MBs
    options.Limits.MaxResponseBufferSize = 10 * 1024 * 1024;  // 10 MBs
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2); // Timeout for keeping the connection alive
    options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30); // Timeout for request headers
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var authSettings = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<AuthenticationOptions>>().Value;

    setup.AddSecurityDefinition(authSettings.CSRFTokenHeaderName, new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = string.Empty,
    });
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Authorization"
                },
                Scheme = "",
                Name = "Authorization",
                In = ParameterLocation.Header,

            },
            []
        }
    });

});
builder.Services.AddOutputCache();
builder.Services.AddAntiforgery(opts =>
{
    var authSettings = builder.Services.BuildServiceProvider()!.GetRequiredService<IOptions<AuthenticationOptions>>().Value;

    opts.HeaderName = authSettings.CSRFTokenHeaderName;
    opts.Cookie.Name = authSettings.CSRFTokenCookieName;
    opts.Cookie.SecurePolicy = authSettings.AntiForgeyCookieSecurePolicy;
    opts.Cookie.SameSite = authSettings.CookieOptions.SameSite;
});

builder.Services.Configure<SSLOptions>(builder.Configuration.GetSection("SSLOptions"));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;

    options.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;
    options.ConsentCookie.HttpOnly = true;
});

ServiceInstallerHelper.InstallServicesRecursively(builder.Services,
    builder.Configuration,
    [InfrastructureAssemblyReference.Assembly, PresentationAssemblyReference.Assembly]);

////////////////////////////////
var app = builder.Build();

app.MapGet("/api/v1/cookie-consent/status", (HttpContext context) =>
{
    var consentFeature = context.Features.Get<ITrackingConsentFeature>();

    return DefaultResponseFactory.Instance.CreateOk(
        PrimitiveResult.Success(new
        {
            accepted = consentFeature?.CanTrack ?? false
        }));
});

app.MapPost("/api/v1/cookie-consent", (HttpContext context) =>
{
    var consentFeature = context.Features.Get<ITrackingConsentFeature>();

    if (consentFeature == null)
        return Results.BadRequest();

    var cookie = consentFeature.CreateConsentCookie();

    context.Response.Headers.Append("Set-Cookie", cookie);

    return DefaultResponseFactory.Instance.CreateOk(
        PrimitiveResult.Success(new { done = true }));
});

app.UseHsts();
app.UseDefaultFiles();
app.MapStaticAssets();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseSession();
//app.UseCors(opts =>
//{
//    opts.AllowAnyMethod();
//    opts.AllowAnyHeader();
//    opts.AllowAnyOrigin();
//});

app.UseOutputCache();
app.MapMinimalEndpoits();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseCookiePolicy();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    // Collapse all endpoints by default
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});
app.UseAuthorization();
//app.UseExceptionHandler();
app.UseMiddleware<UserContextAccessorMiddleware>();
LoginEndpoints.AddAllEndpoitnts(app);
app.UseSession();

app.MapControllers();
app.MapFallbackToFile("/index.html");
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
static class CommandArgReader
{
    public static string Read(IConfiguration config, string key, string defaultValue)
    {
        var result = config[key];
        return string.IsNullOrWhiteSpace(result) ? defaultValue : result;
    }
}