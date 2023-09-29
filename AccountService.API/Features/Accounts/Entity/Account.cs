using AccountService.API.Common;

namespace AccountService.API.Features.Accounts.Entity;

public sealed class Account: Entity<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string SocialSecurityNumber { get; set; }
    public string BirthDate { get; set; }

    public Account() { }

    public Account(string firstName, string lastName, string email, string socialSecurityNumber, DateOnly birthday)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        SocialSecurityNumber = socialSecurityNumber;
        BirthDate = birthday.ToString("yyyy-MM-dd");
    }
}
