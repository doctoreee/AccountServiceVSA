using System.Text.Json.Serialization;

namespace AccountService.API.Common;

public class ResponseBase
{
    [JsonIgnore] public bool Success { get; set; }
    [JsonIgnore] public ushort Code { get; set; }
    [JsonIgnore] public string Message { get; set; }
}
