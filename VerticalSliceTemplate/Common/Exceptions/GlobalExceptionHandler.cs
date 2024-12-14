using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace VerticalSliceTemplate.Common.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = exception switch
            {
                FluentValidation.ValidationException fluentException => HandleFluentValidationException(fluentException),
                _ => HandleDefaultException(),
            };

            #region comment
            /*
            if (exception is FluentValidation.ValidationException fluentException)
            {
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = "El request enviado tiene errores.";
                problemDetails.Extensions.Add("traceId", Guid.NewGuid().ToString());
                List<string> validationErrors = new List<string>();
                foreach (var error in fluentException.Errors)
                {
                    validationErrors.Add(error.ErrorMessage);
                }
                problemDetails.Extensions.Add("errors", validationErrors);
            }
            else
            {
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Title = "Se producieron errores en el servidor.";
                problemDetails.Extensions.Add("traceId", Guid.NewGuid().ToString());
            }
            */
            #endregion

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
            return true;
        }

        public static ProblemDetails HandleFluentValidationException(FluentValidation.ValidationException exception)
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "El request enviado tiene errores.";
            problemDetails.Extensions.Add("traceId", Guid.NewGuid().ToString());
            List<string> validationErrors = new List<string>();
            foreach (var error in exception.Errors)
            {
                validationErrors.Add(error.ErrorMessage);
            }
            problemDetails.Extensions.Add("errors", validationErrors);

            return problemDetails;
        }

        public static ProblemDetails HandleDefaultException()
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Status = StatusCodes.Status500InternalServerError;
            problemDetails.Title = "Se producieron errores en el servidor.";
            problemDetails.Extensions.Add("traceId", Guid.NewGuid().ToString());
            return problemDetails;
        }
    }
}
