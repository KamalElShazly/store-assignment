using Assignment.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Assignment.API.Exceptions;

public class AppExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        (int statusCode, string? errorMessage) = exception switch
        {
            NotFoundException => (404, null),
            _ => (500, "Something Went Wrong"),
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorMessage);

        return true;
    }
}
