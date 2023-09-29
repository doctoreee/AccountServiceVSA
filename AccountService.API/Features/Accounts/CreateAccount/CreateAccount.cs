using AccountService.API.ExternalServices.Interfaces;
using AccountService.API.Features.Accounts.Entity;
using AccountService.API.Features.Accounts.Infrastructure;
using MediatR;

namespace AccountService.API.Features.Accounts.CreateAccount;

internal class CreateAccountContext
{
    public CreateAccountCommand Command { get; }
    public Account? Account { get; private set; }

    public CreateAccountContext(CreateAccountCommand command)
    {
        Command = command;
    }

    internal void SetAccount(Account account)
    {
        Account = account;
    }
}

public record CreateAccountCommand(string FirstName, string LastName, string Email, string SocialSecurityNumber, DateOnly BirthDate, Contracts.Requests.Password Password):IRequest<Result>
{
    public Account ToAccount()
    {
        return new Account(FirstName, LastName, Email, SocialSecurityNumber, BirthDate);
    }

    public TCKNValidationRequest ToTCKNValidationRequest()
    {
        return new TCKNValidationRequest(FirstName, LastName, SocialSecurityNumber, BirthDate);   
    }
}

public sealed class CreateAccountHandler: IRequestHandler<CreateAccountCommand, Result>
{
    private readonly IAccountRepository AccountRepository;
    private readonly IPasswordRepository PasswordRepository;
    private readonly ITCKNValidateService TCKNValidateService;

    public CreateAccountHandler(IAccountRepository accountRepository, IPasswordRepository passwordRepository, ITCKNValidateService tCKNValidateService)
    {
        AccountRepository = accountRepository;
        PasswordRepository = passwordRepository;
        TCKNValidateService = tCKNValidateService;
    }

    public async Task<Result> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var context = new CreateAccountContext(request);
        var handler = new ValidationHandler();
        handler.SetNext(new TCKNValidationHandler(TCKNValidateService))
            .SetNext(new AccountHandler(AccountRepository))
            .SetNext(new PasswordHandler(PasswordRepository));
            
        handler.Handle(context);

        return new Result(true, context.Account, default);
    }
}