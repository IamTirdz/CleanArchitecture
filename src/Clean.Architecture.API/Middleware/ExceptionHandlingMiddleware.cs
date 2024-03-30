using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Common.Models;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Clean.Architecture.API.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly JsonSerializerOptions _serializerOptions;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BaseException e)
        {
            await HandleExceptions(context, e);
        }
        catch (Exception e)
        {
            await HandleExceptions(context, e);
        }
    }

    private async Task HandleExceptions(HttpContext httpContext, BaseException ex)
    {
        var statusCode = ex switch
        {
            BadRequestException => HttpStatusCode.BadRequest,
            UnauthorizedException => HttpStatusCode.Unauthorized,
            ForbiddenException => HttpStatusCode.Forbidden,
            NotFoundException => HttpStatusCode.NotFound,
            ValidationException => HttpStatusCode.UnprocessableEntity,
            BusinessException => HttpStatusCode.InternalServerError,
            _ => HttpStatusCode.InternalServerError
        };

        var result = string.Empty;
        if (!string.IsNullOrEmpty(ex.Message))
            result = JsonSerializer.Serialize(ex.ErrorResponse, _serializerOptions);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsync(result);
    }

    private async Task HandleExceptions(HttpContext httpContext, Exception ex)
    {
        var referenceKey = Activity.Current!.RootId!;
        var baseMessage = "An unhandled exception has occurred.";
        var message = new ErrorResponseDto(baseMessage, referenceKey);
        var result = JsonSerializer.Serialize(message, _serializerOptions);

        _logger.LogError(ex, "{Message} - {ReferenceKey}", baseMessage, message.ReferenceKey);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await httpContext.Response.WriteAsync(result);
    }
}
