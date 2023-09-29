using System.Text.Json.Serialization;

namespace AccountService.API.Contracts.Responses;

public class ApiResponse
{
    public bool Success { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public dynamic Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ApiResponseMessage> Messages { get; set; }
}
