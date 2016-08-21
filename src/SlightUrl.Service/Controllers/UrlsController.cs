namespace SlightUrl.Service.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using SlightUrl.Components.Commands;
    using SlightUrl.Service.Interfaces;

    [RoutePrefix("api/urls")]
    public class UrlsController : ApiController
    {
        private readonly IInstanceFactory _factory;

        public UrlsController(IInstanceFactory factory)
        {
            _factory = factory;
        }

        [Route, HttpPost]
        public async Task<IHttpActionResult> CreateAsync([FromBody] CreateShortUrl.Command command)
        {
            var instance = _factory.CreateInstance<CreateShortUrl>();
            var result = await instance.HandleAsync(command);
            return Ok(result);
        }
    }
}