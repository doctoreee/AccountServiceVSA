namespace AccountService.API.Validation.Exceptions;

public class DomainException : Exception
{
    public DomainException(ExceptionModel model, dynamic data = null) : base(model.Message)
    {
        Model = model;
        Model.Data = data;
    }

    public ExceptionModel Model { get; }
}
