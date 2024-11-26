namespace LoyaltySystemApi.Domain.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static readonly string InvalidUserId = "Invalid user id";
        public static readonly string InvalidEmailAddress = "Enter valid email address";
        public static readonly string NameRequired = "Please enter name";
        public static readonly string PasswordRequired= "Your password cannot be empty";
        public static readonly string PasswordMustBe= @"Your password length must be at least 8.
                                                        Your password length must not exceed 16.
                                                        Your password must contain at least one uppercase letter.
                                                        Your password must contain at least one lowercase letter.
                                                        Your password must contain at least one number.
                                                        Your password must contain at least one (!? *.).";

        public static readonly string MobileMeBe = @"Mobile must not be less than 11 characters.
                                                     Mobile is not valid";
    }
}
