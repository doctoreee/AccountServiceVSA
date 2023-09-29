using AccountService.API.Contracts.Requests;
using AccountService.API.Features.Accounts.CreateAccount;

namespace AccountService.API.Contracts;

public static class ContractsMapping
{
    public static CreateAccountCommand ToCommand(this CreateAccountRequest request)
    {
        return new CreateAccountCommand(request.FirstName, request.LastName, request.Email, request.SocialSecurityNumber, request.BirthDate, request.Password);
    }
}
