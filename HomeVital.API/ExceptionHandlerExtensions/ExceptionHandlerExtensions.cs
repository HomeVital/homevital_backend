using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using HomeVital.Models.Exceptions;

namespace HomeVital.API.ExceptionHandlerExtensions
{
    public static class ExceptionHandlerExtensions
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        var statusCode = (int)HttpStatusCode.InternalServerError;
                        var response = new { error = "An unexpected error occurred." };

                        context.Response.ContentType = "application/json";

                        // Map exceptions to specific status codes and responses
                        if (exception is ResourceNotFoundException)
                        {
                            statusCode = (int)HttpStatusCode.NotFound;
                            response = new { error = exception.Message };
                        }
                        else if (exception is ModelFormatException)
                        {
                            statusCode = StatusCodes.Status412PreconditionFailed;
                            response = new { error = exception.Message };
                        }
                        else if (exception is ArgumentOutOfRangeException)
                        {
                            statusCode = (int)HttpStatusCode.BadRequest;
                            response = new { error = exception.Message };
                        }
                        else if (exception is ExternalApiException)
                        {
                            statusCode = (int)HttpStatusCode.BadGateway;
                            response = new { error = exception.Message };
                        }
                        else if (exception is UserException)
                        {
                            statusCode = (int)HttpStatusCode.Conflict;
                            response = new { error = exception.Message };
                        }
                        else if (exception is SecurityTokenException)
                        {
                            statusCode = (int)HttpStatusCode.Unauthorized;
                            response = new { error = exception.Message };
                        }
                        else if (exception is MethodNotAllowedException)
                        {
                            statusCode = (int)HttpStatusCode.MethodNotAllowed;
                            response = new { error = exception.Message };
                        }
                        else if (exception is NotImplementedException)
                        {
                            statusCode = (int)HttpStatusCode.NotImplemented;
                            response = new { error = exception.Message };
                        }
                        else if (exception is UnauthorizedAccessException)
                        {
                            statusCode = (int)HttpStatusCode.Unauthorized;
                            response = new { error = exception.Message };
                        }

                        // Set the status code and write the response
                        context.Response.StatusCode = statusCode;
                        await context.Response.WriteAsJsonAsync(response);
                    }
                });
            });
        }
    }
}