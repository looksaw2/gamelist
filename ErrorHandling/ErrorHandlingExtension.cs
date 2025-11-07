using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETDemo1.ErrorHandling;

public static class ErrorHandlingExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.Run(async context =>
        {
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>()
                .CreateLogger("Error Handling");
            var exceptionDetails = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;
            logger.LogInformation(exception,"Could not process a request on machine {Machine} , TraceID {TraceID}",
                Environment.MachineName,
                Activity.Current?.TraceId
                );
            var problem = new ProblemDetails()
            {
                Title = "We made a mistake but  we're working on it!",
                Status = StatusCodes.Status500InternalServerError,
                Extensions =
                {
                    { "traceId", Activity.Current?.TraceId }
                }
            };

            var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();
            if (environment.IsDevelopment())
            {
                problem.Detail = exception?.ToString();
            }

            await Results.Problem(problem).ExecuteAsync(context);
        });
    }
}