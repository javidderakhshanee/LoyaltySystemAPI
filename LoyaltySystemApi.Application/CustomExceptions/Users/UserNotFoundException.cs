namespace LoyaltySystemApi.Application.CustomExceptions.Users;

public sealed class UserNotFoundException : ApplicationException
{
    public UserNotFoundException() : base($"User not found!")
    {

    }
}
