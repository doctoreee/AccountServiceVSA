namespace AccountService.API.Contracts.Responses;

public class ApiResponseMessage
{
    public ushort Code { get; }
    public string Message { get; }

    public ApiResponseMessage(ushort code, string message)
    {
        Code = code;
        Message = message;
    }
}