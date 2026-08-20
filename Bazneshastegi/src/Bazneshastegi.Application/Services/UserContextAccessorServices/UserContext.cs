namespace Bazneshastegi.Application.Services.UserContextAccessorServices;

public sealed record UserContext(string PersonId)
{
    public readonly static UserContext Guest = new(string.Empty);

    public static UserContext Create(string id) => new(id);
}
