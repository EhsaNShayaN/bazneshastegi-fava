using Bazneshastegi.Application.Services.UserContextAccessorServices;

namespace Bazneshastegi.Presentation.Services.ApplicationServices.UserContextAccessorServices;
sealed class UserContextAccessor : IUserContextAccessor
{
    private static readonly AsyncLocal<UserContext?> _current = new();
    public UserContext? Current
    {
        get => _current.Value ?? UserContext.Guest;
        set => _current.Value = value;
    }
    public UserContext GetCurrent() => Current ?? UserContext.Guest;
    public string CurrentPersonId => this.GetCurrent().PersonId;
    public bool IsGuest()
    {
        var current = this.GetCurrent();
        return current.Equals(UserContext.Guest) || current.PersonId.Equals(default) || string.IsNullOrWhiteSpace(current.PersonId);
    }
}
