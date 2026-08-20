namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderLoginRequest(string Username, string Password);

public readonly record struct ProviderLoginResponse(
    string Token,
    string RefreshToken,
    string Error,
    DateTime? Expiredate);
