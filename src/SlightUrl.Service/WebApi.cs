namespace SlightUrl.Service
{
    using System.Web.Http;

    using SlightUrl.Service.Handers;

    public static class WebApi
    {
        public static HttpConfiguration Configuration(HttpConfiguration config = null)
        {
            config = config ?? new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.Filters.Add(new ValidationExceptionHandler());

            return config;
        }
    }
}