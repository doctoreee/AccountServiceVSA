using AccountService.API.Common;
using AccountService.API.Validation.Exceptions;

namespace AccountService.API.Features.Accounts.CreateAccount;

internal class ValidationHandler: Handler<CreateAccountContext>
{
    public override void Handle(CreateAccountContext request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var command = request.Command;
        if (string.IsNullOrWhiteSpace(command.FirstName))
        {
            throw new DomainException(DomainExceptionContent.FirstNameCannotBeEmpty);
        }

        if (string.IsNullOrWhiteSpace(command.LastName))
        {
            throw new DomainException(DomainExceptionContent.LastNameCannotBeEmpty);
        }

        if (string.IsNullOrWhiteSpace(command.SocialSecurityNumber))
        {
            throw new DomainException(DomainExceptionContent.SocialSecurityNumberCannotBeEmpty);
        }

        if (command.SocialSecurityNumber.Length != 11)
        {
            throw new DomainException(DomainExceptionContent.SocialSecurityNumberMustBeElevenChars);
        }

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            throw new DomainException(DomainExceptionContent.EmailCannotBeEmpty);
        }

        if (command.BirthDate.Equals(default))
        {
            throw new DomainException(DomainExceptionContent.BirthDateCannotBeEmpty);
        }

        if (command.BirthDate.Year.ToString().Length != 4)
        {
            throw new DomainException(DomainExceptionContent.BirthYearMustBeFourChars);
        }

        if (string.IsNullOrWhiteSpace(command.Password.PasswordToCompare))
        {
            throw new DomainException(DomainExceptionContent.PasswordCannotBeEmpty);
        }

        if (string.IsNullOrWhiteSpace(command.Password.ComparisonEntry))
        {
            throw new DomainException(DomainExceptionContent.ReEnteredPasswordCannotBeEmpty);
        }

        if (!command.Password.PasswordToCompare.Equals(command.Password.ComparisonEntry))
        {
            throw new DomainException(DomainExceptionContent.PasswordMismatch);
        }

        base.Handle(request);
    }
}
