using System.Text.Json.Serialization;

namespace AccountService.API;

public readonly struct Result
{
    public bool Success { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object Data { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ApiResponseMessage> Messages { get; }

    public Result(bool success, object data, List<ApiResponseMessage> messages)
    {
        Success = success;
        Data = data;
        Messages = messages;
    }
}

public readonly struct ApiResponseMessage
{
    public ushort Code { get; }
    public string Message { get; }

    public ApiResponseMessage(ushort code, string message)
    {
        Code = code;
        Message = message;
    }
}