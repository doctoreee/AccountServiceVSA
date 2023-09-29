using AccountService.API.Common;
using AccountService.API.ExternalServices.Interfaces;
using AccountService.API.Validation.Exceptions;

namespace AccountService.API.Features.Accounts.CreateAccount;

internal class TCKNValidationHandler: Handler<CreateAccountContext>
{
    private readonly ITCKNValidateService ValidationService;

    public TCKNValidationHandler(ITCKNValidateService validateService)
    {
        ValidationService = validateService;
    }

    public override void Handle(CreateAccountContext request)
    {
        if(!ValidationService.Validate(request.Command.ToTCKNValidationRequest()))
        {
            throw new DomainException(DomainExceptionContent.SocialSecurityNotValidated, request);
        }

        base.Handle(request);
    }
}
