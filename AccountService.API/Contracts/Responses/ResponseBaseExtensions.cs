namespace AccountService.API.Contracts.Responses;

public static class ResponseBaseExtensions
{
    public static ApiResponse ToApiResponse(this Result responseBase)
    {
        return new()
        {
            Success = true,
            Data = responseBase,
            Messages = responseBase.Messages == default
                ? null
                : responseBase.Messages.Select(x => new ApiResponseMessage(x.Code, x.Message)).ToList()
        };
    }
}
