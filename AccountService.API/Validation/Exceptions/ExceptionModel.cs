namespace AccountService.API.Validation.Exceptions;

public class ExceptionModel
{
    public ExceptionModel(ushort code, string message, dynamic data = null)
    {
        Code = code;
        Message = message;
        Data = data;
    }

    public dynamic Data { get; protected internal set; }
    public ushort Code { get; }
    public string Message { get; }
}
