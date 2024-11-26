using LoyaltySystemApi.Domain.Base;

namespace LoyaltySystemApi.Domain.Entities.Users;

public sealed class UserEntity : BaseEntity
{
    public UserEntity(string email, string name, string password, bool registerFromWeb, bool registerFromApp)
    {
        Email = email;
        Name = name;
        Password = password;
        RegisterFromWeb = registerFromWeb;
        RegisterFromApp = registerFromApp;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool RegisterFromWeb { get; private set; }
    public bool RegisterFromApp { get; private set; }
    public string? Mobile { get; private set; }
    public DateTime DateCreated { get; private set; } = DateTime.UtcNow;
    public DateTime? DateUpdated { get; private set; }
    public DateTime? Birthday { get; private set; }
    public List<UserActivityEntity> Activities { get; private set; } = new();
    public List<UserPointEarnEntity> EarnedPoints { get; private set; } = new();

    public UserEntity UpdateData(string name, string mobile, DateTime? birthday)
    {
        Name = name;
        Mobile = mobile;
        Birthday = birthday;
        DateUpdated = DateTime.UtcNow;
        return this;
    }
}
