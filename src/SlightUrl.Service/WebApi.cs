namespace SlightUrl.Service
{
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;

    using SlightUrl.Service.Handers;

    public static class WebApi
    {
        public static HttpConfiguration Configure()
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Services.Add(typeof(IExceptionLogger), new ValidationExceptionHandler());

            return config;
        }
    }
}