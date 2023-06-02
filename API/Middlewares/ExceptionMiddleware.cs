using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Middlewares;

public class ExceptionMiddleware
{

    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = GetStatusCode(ex);
        string template = "[{datetime}] {message}";
        object[] args = new object[] { DateTime.Now.ToString(), ex.Message };

        if (statusCode > 499)
            _logger.LogError(ex, template, args);
        else
            _logger.LogInformation(ex, template, args);

        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = statusCode;

        var modelErros = new List<ExceptionModel>();

        GetErrorMessage(modelErros, ex);

        var result = System.Text.Json.JsonSerializer.Serialize(modelErros);
        return response.WriteAsync(result);
    }

    private static int GetStatusCode(Exception ex)
    {
        if (ex is UnauthorizedAccessException)
            return (int)HttpStatusCode.Unauthorized;

        else if (ex is ValidationException)
            return (int)HttpStatusCode.BadRequest;

        else if (ex is ApplicationException)
            return (int)HttpStatusCode.BadRequest;

        else if (ex is KeyNotFoundException)
            return (int)HttpStatusCode.NotFound;

        return (int)HttpStatusCode.InternalServerError;
    }

    private static void GetErrorMessage(List<ExceptionModel> modelErros, Exception ex)
    {
        modelErros.Add(new ExceptionModel(ex.Message));

        if (ex.InnerException != null)
            GetErrorMessage(modelErros, ex.InnerException);

    }

}

