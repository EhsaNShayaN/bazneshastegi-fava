namespace Bazneshastegi.Application.Services.UserContextAccessorServices;
public interface IUserContextAccessor
{
    UserContext? Current { get; set; }

    UserContext GetCurrent();

    string CurrentPersonId { get; }

    bool IsGuest();
}