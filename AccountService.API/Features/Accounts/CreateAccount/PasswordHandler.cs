using AccountService.API.Common;
using AccountService.API.Features.Accounts.Infrastructure;
using AccountService.API.Helpers;
using AccountService.API.Validation.Exceptions;

namespace AccountService.API.Features.Accounts.CreateAccount;

internal class PasswordHandler: Handler<CreateAccountContext>
{
    private readonly IPasswordRepository PasswordRepository;
    public PasswordHandler(IPasswordRepository passwordRepository)
    {
        PasswordRepository = passwordRepository;
    }

    public override void Handle(CreateAccountContext request)
    {
        if(request.Account == default)
        {
            throw new DomainException(DomainExceptionContent.AccountCannotCreated); 
        }

        var passwordHash = CryptoHelper.PasswordHash(request.Command.Password.PasswordToCompare);
        PasswordRepository.AddAsync(new Entity.Password(request.Account.Id, passwordHash));
        
        base.Handle(request);
    }
}
