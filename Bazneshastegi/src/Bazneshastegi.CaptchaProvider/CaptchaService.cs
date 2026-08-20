using Microsoft.AspNetCore.Http;
using SRH.PrimitiveTypes.Result;
using System.Text;
using static Bazneshastegi.CaptchaProvider.RandomStringGenerator;

namespace Bazneshastegi.CaptchaProvider;

public sealed class CaptchaService : ICaptchaService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CaptchaService(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<PrimitiveResult<string>> GenerateNewCaptchaString(
        int length,
        TimeSpan expiry,
        PasswordGeneratorOptions passwordGeneratorOptions,
        CancellationToken cancellationToken)
    {
        var captchaText = RandomStringGenerator.Generate(length, passwordGeneratorOptions);

        var result = Guid.NewGuid().ToString();

        this._httpContextAccessor.HttpContext.Session.Set(
            GenerateCacheTokenKey(result),
            Encoding.UTF8.GetBytes(captchaText));

        return result;
    }

    public async ValueTask<PrimitiveResult<string>> GenerateNewCaptchaString(CancellationToken cancellationToken) =>
        await this.GenerateNewCaptchaString(
            6,
            TimeSpan.FromMinutes(2),
            RandomStringGenerator.PasswordGeneratorOptions.UseLowercaseChars
            | RandomStringGenerator.PasswordGeneratorOptions.UseUppercaseChars
            | RandomStringGenerator.PasswordGeneratorOptions.UseDigits,
            cancellationToken)
        .ConfigureAwait(false);

    public async ValueTask<PrimitiveResult<string>> GenerateNewCaptchaDigit(CancellationToken cancellationToken) =>
        await this.GenerateNewCaptchaString(
            6,
            TimeSpan.FromMinutes(2),
            RandomStringGenerator.PasswordGeneratorOptions.UseDigits,
            cancellationToken)
        .ConfigureAwait(false);

    public async ValueTask<PrimitiveResult<string>> GenerateNewCaptchaDigit(TimeSpan expiry, CancellationToken cancellationToken) =>
        await this.GenerateNewCaptchaString(
            6,
            expiry,
            RandomStringGenerator.PasswordGeneratorOptions.UseDigits,
            cancellationToken)
        .ConfigureAwait(false);

    public async ValueTask<PrimitiveResult<MemoryStream>> GetCaptchaById(
        string id,
        int width,
        int height,
        CancellationToken cancellationToken)
    {
        var cacheKey = GenerateCacheTokenKey(id);

        if (this._httpContextAccessor.HttpContext.Session.TryGetValue(cacheKey, out byte[] bytes))
        {
            string captchaText = Encoding.UTF8.GetString(bytes);
            return CaptchaHelper.GenerateCaptchaImageUsingLittleDots(captchaText, width, height, 5000, 80, 150);
        }

        return PrimitiveResult.Failure<MemoryStream>("", "Captcha not found");
    }

    public async ValueTask<PrimitiveResult> Validate(string id, string value, CancellationToken cancellationToken)
    {
        var cacheKey = GenerateCacheTokenKey(id);

        if (this._httpContextAccessor.HttpContext.Session.TryGetValue(cacheKey, out byte[] bytes))
        {
            string captchaText = Encoding.UTF8.GetString(bytes);
            if (string.IsNullOrEmpty(captchaText))
            {
                return PrimitiveResult.Failure("", "Captcha not found");
            }
            if (!captchaText.Equals(value))
            {
                return PrimitiveResult.Failure("", "Invalid captcha value");
            }
            this._httpContextAccessor.HttpContext.Session.Remove(cacheKey);
        }

        return PrimitiveResult.Success();
    }


    static string GenerateCacheTokenKey(string id) => $"Bazneshastegi:Captcha:{id}";
    public async ValueTask<PrimitiveResult<RenewCaptchaResult>> RenewCaptchaDigit(
        string oldId,
        TimeSpan expiry,
        int width,
        int height,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(oldId))
        {

            var cacheKey = GenerateCacheTokenKey(oldId);
            this._httpContextAccessor.HttpContext.Session.Remove(cacheKey);
        }

        var result = await this.GenerateNewCaptchaDigit(expiry, cancellationToken)
            .Map(id => this.GetCaptchaById(id, width, height, cancellationToken).Map(c => new RenewCaptchaResult(id, c)))
            .ConfigureAwait(false);

        return result;
    }
}
