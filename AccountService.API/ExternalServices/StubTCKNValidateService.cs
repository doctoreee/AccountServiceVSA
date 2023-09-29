using AccountService.API.ExternalServices.Interfaces;

namespace AccountService.API.ExternalServices;

public class StubTCKNValidateService: ITCKNValidateService
{
    public bool Validate(TCKNValidationRequest validationRequest)
    {
        return true;
    }
}