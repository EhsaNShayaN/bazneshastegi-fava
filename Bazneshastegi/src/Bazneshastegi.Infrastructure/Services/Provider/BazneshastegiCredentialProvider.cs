namespace Bazneshastegi.Infrastructure.Services.Provider;

public sealed class BazneshastegiCredentialProvider : IBazneshastegiCredentialProvider
{
    public ValueTask<BazneshastegiCredentialInfo> GetUserCredentialInfo()
        => ValueTask.FromResult(new BazneshastegiCredentialInfo("baz-1", "abcd/1234"));
}
public sealed record BazneshastegiCredentialInfo(string Username, string Pasword);
public interface IBazneshastegiCredentialProvider
{
    ValueTask<BazneshastegiCredentialInfo> GetUserCredentialInfo();
}
