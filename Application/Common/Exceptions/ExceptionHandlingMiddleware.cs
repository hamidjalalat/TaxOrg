using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Nazm.Results;
namespace Application.Common.Exceptions
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new
            {
                status = statusCode,
                message = exception.Message,
                errors = GetErrors(exception)
            };
            
            var result =new  FluentResults.Result<object>();
            result.WithValue(response);

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(result.ConvertToDtatResult()));
        }

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                ForbiddenAccessException=> StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

       

        private static IDictionary<string, string[]> GetErrors(Exception exception)
        {
            IDictionary<string, string[]> errors = null;

            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors;
            }

            return errors;
        }
    }
}
