using Clean.Architecture.Business.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace Clean.Architecture.API.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptions(context, e);
        }
    }

    private async Task HandleExceptions(HttpContext httpContext, Exception e)
    {
        var statusCode = GetStatusCode(e);
        httpContext.Response.Headers.ContentType = "application/json";
        var referrenceKey = Guid.NewGuid().ToString();
        var message = e.Message;
        var errors = GetExceptionErrors(e);

        if (statusCode == (int)HttpStatusCode.InternalServerError)
        {
            message = "An unhandled exception has occurred.";
            _logger.LogError(e, "ExceptionMessage: {exceptionMessage}", e.Message);
        }

        var response = new
        {
            Message = message,
            ReferenceKey = referrenceKey,
            Code = statusCode,
            CodeInfo = ((HttpStatusCode)statusCode).ToString(),
            Errors = errors
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }

    private IDictionary<string, string[]>? GetExceptionErrors(Exception ex)
    {
        IDictionary<string, string[]>? errors = null;
        if (ex is ValidationException validationException)
        {
            errors = validationException.Errors;
        }

        return errors;
    }

    private int GetStatusCode(Exception ex)
    {
        return ex switch
        {
            ForbiddenException => StatusCodes.Status403Forbidden,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            BadRequestException => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
