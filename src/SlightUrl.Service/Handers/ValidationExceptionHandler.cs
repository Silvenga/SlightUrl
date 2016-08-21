namespace SlightUrl.Service.Handers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;
    using System.Web.Http.ModelBinding;

    using SlightUrl.Components.Validations;

    public class ValidationExceptionHandler : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(HttpActionExecutedContext context, CancellationToken cancellationToken)
        {
            var exception = context.Exception as SlightValidationException;
            if (exception != null)
            {
                var validationException = exception;

                var modeState = new ModelStateDictionary();
                modeState.AddModelError(validationException.PropertyName, validationException.ErrorMessage);

                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, modeState);
            }

            return base.OnExceptionAsync(context, cancellationToken);
        }
    }
}