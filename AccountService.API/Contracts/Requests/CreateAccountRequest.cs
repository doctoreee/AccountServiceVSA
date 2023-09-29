namespace AccountService.API.Contracts.Requests;

public sealed class CreateAccountRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string SocialSecurityNumber { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required Password Password { get; set; }
}

public struct Password
{
    public string PasswordToCompare { get; set; }
    public string ComparisonEntry { get; set; }

    public Password(string password, string passwordReentered)
    {
        PasswordToCompare = password;
        ComparisonEntry = passwordReentered;
    }
}
