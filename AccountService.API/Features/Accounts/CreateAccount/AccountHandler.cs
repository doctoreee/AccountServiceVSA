using AccountService.API.Common;
using AccountService.API.Features.Accounts.Infrastructure;
using AccountService.API.Validation.Exceptions;

namespace AccountService.API.Features.Accounts.CreateAccount;

internal class AccountHandler: Handler<CreateAccountContext>
{
    private readonly IAccountRepository AccountRepository;

    public AccountHandler(IAccountRepository accountRepository) 
    { 
        AccountRepository = accountRepository;
    }

    public override void Handle(CreateAccountContext request)
    {
        var account = AccountRepository.Get(x => x.SocialSecurityNumber == request.Command.SocialSecurityNumber).FirstOrDefault();
        if (account != default)
        {
            throw new DomainException(DomainExceptionContent.SocialSecurityAlreadyUsed);
        }

        request.SetAccount(AccountRepository.Add(request.Command.ToAccount()));
        base.Handle(request);
    }
}
