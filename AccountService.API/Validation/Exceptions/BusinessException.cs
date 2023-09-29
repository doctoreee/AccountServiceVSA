namespace AccountService.API.Validation.Exceptions;

public abstract class BusinessException : Exception
{
    protected BusinessException(string message) : base(message) { }

    public new abstract dynamic Data { get; }
    public abstract ushort Code { get; }
}
