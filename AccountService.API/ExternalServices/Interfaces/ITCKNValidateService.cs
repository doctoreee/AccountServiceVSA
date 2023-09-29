namespace AccountService.API.ExternalServices.Interfaces;

public interface ITCKNValidateService
{
    bool Validate(TCKNValidationRequest validationRequest);
}

public record TCKNValidationRequest(string Name, string LastName, string SocialSecurityNumber, DateOnly BirthDate);
