using FluentValidation;

namespace QualificationTaskAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException exception)
            {
                await HandleValidationException(context.Response, exception);
            }
        }

        private async Task HandleValidationException(HttpResponse response, ValidationException exception)
        {
            response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;

            var errors = exception.Errors
                .GroupBy(e => FormatPropertyName(e.PropertyName))
                .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray());

            await response.WriteAsJsonAsync(new { errors });
        }

        private string FormatPropertyName(string fullName)
        {
            return fullName.Substring(fullName.LastIndexOf('.') + 1);
        }
    }
}
