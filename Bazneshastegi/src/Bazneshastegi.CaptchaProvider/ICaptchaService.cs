using SRH.PrimitiveTypes.Result;
using static Bazneshastegi.CaptchaProvider.RandomStringGenerator;

namespace Bazneshastegi.CaptchaProvider;

public interface ICaptchaValidator
{
    ValueTask<PrimitiveResult> Validate(string id, string value, CancellationToken cancellationToken);
}
public interface ICaptchaService : ICaptchaValidator
{
    ValueTask<PrimitiveResult<string>> GenerateNewCaptchaString(
        int length,
        TimeSpan expiry,
        PasswordGeneratorOptions passwordGeneratorOptions,
        CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<string>> GenerateNewCaptchaString(CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<string>> GenerateNewCaptchaDigit(CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<string>> GenerateNewCaptchaDigit(TimeSpan expiry, CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<MemoryStream>> GetCaptchaById(
        string id,
        int width,
        int height,
        CancellationToken cancellationToken);

    ValueTask<PrimitiveResult<RenewCaptchaResult>> RenewCaptchaDigit(
        string oldId,
        TimeSpan expiry,
        int width,
        int height,
        CancellationToken cancellationToken);


}
public sealed record RenewCaptchaResult(string Id, MemoryStream MemoryStream);

