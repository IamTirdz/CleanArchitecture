using Clean.Architecture.Business.Common.Exceptions;
<<<<<<< HEAD
using Clean.Architecture.Business.Common.Models;
=======
using Newtonsoft.Json;
>>>>>>> update template
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Clean.Architecture.API.Middleware;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
<<<<<<< HEAD
    private readonly JsonSerializerOptions _serializerOptions;
=======
>>>>>>> update template

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
        _serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            context.Response.Headers["OperationId"] = Activity.Current!.RootId;
            await next(context);
        }
<<<<<<< HEAD
        catch (BaseException e)
        {
            await HandleExceptions(context, e);
        }
        catch (Exception e)
=======
        catch (BaseException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
>>>>>>> update template
        {
            await HandleExceptionAsync(context, ex);
        }
    }

<<<<<<< HEAD
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
        {
            if (ex.ErrorResponse.Code == null) ex.ErrorResponse.Code = (int)statusCode;
            if (ex.ErrorResponse.CodeInfo == null) ex.ErrorResponse.CodeInfo = statusCode.ToString();

            result = JsonSerializer.Serialize(ex.ErrorResponse, _serializerOptions);
        }

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
=======
    private async Task HandleExceptionAsync(HttpContext context, BaseException ex)
    {
        var statusCode = ex switch
        {
            ForbiddenException => HttpStatusCode.Forbidden,
            NotFoundException => HttpStatusCode.NotFound,
            UnauthorizedException => HttpStatusCode.Unauthorized,
            BadRequestException => HttpStatusCode.BadRequest,
            ValidationException => HttpStatusCode.UnprocessableEntity,
            _ => HttpStatusCode.InternalServerError
        };

        var result = string.Empty;
        if (!string.IsNullOrEmpty(ex.ErrorResponse.Message))
        {
            ex.ErrorResponse.Code = (int)statusCode;
            ex.ErrorResponse.CodeInfo = statusCode.ToString();

            result = JsonConvert.SerializeObject(ex.ErrorResponse);
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(result);
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        var referrenceKey = Activity.Current!.RootId;
        const string message = "An unhandled exception has occurred.";

        var response = new
        {
            Message = message,
            ReferenceKey = referrenceKey,
            Code = statusCode,
            CodeInfo = statusCode.ToString()
        };

        var result = JsonConvert.SerializeObject(response);

        _logger.LogError(ex, "{Message}--{ReferenceKey}", message, referrenceKey);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(result);
>>>>>>> update template
    }
}
