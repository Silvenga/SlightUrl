namespace SlightUrl.Service.Handers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.ModelBinding;
    using System.Web.Http.Results;

    using SlightUrl.Components.Validations;

    public class ValidationExceptionHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var validationException = (SlightValidationException) context.Exception;

            var modeState = new ModelStateDictionary();
            modeState.AddModelError(validationException.PropertyName, validationException.ErrorMessage);

            context.Result = new ResponseMessageResult(context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, modeState));

            return base.HandleAsync(context, cancellationToken);
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            if (context.Exception is SlightValidationException)
            {
                return true;
            }

            return base.ShouldHandle(context);
        }
    }
}