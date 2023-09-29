using AccountService.API.Contracts.Responses;
using AccountService.API.Validation.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace AccountService.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    readonly JsonSerializerSettings _serializerSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            await HandleBusinessExceptionAsync(context, ex);
        }
        catch (DomainException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleBusinessExceptionAsync(context, ex);
        }
    }

    private Task HandleValidationExceptionAsync(HttpContext context, DomainException exception)
    {
        var result = JsonConvert.SerializeObject(new ApiResponse
        {
            Success = false,
            Data = exception.Model.Data,
            Messages = new List<Contracts.Responses.ApiResponseMessage>
            {
                new Contracts.Responses.ApiResponseMessage(exception.Model.Code,  exception.Model.Message)
            }
        }, _serializerSettings);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        return context.Response.WriteAsync(result);
    }

    private Task HandleBusinessExceptionAsync(HttpContext context, Exception exception)
    {
        var result = JsonConvert.SerializeObject(new ApiResponse()
        {
            Success = false,
            Data = exception.Data,
            Messages = new List<Contracts.Responses.ApiResponseMessage>
            {
                new Contracts.Responses.ApiResponseMessage(500, exception.Message)
            }
        }, _serializerSettings);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        return context.Response.WriteAsync(result);
    }

    private Task HandleBusinessExceptionAsync(HttpContext context, BusinessException exception)
    {
        var result = JsonConvert.SerializeObject(new ApiResponse
        {
            Success = false,
            Data = exception.Data,
            Messages = new List<Contracts.Responses.ApiResponseMessage> { new Contracts.Responses.ApiResponseMessage(exception.Code, exception.Message) }
        }, _serializerSettings);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.OK;
        return context.Response.WriteAsync(result);
    }
}
