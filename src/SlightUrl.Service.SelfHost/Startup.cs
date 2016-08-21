namespace SlightUrl.Service.SelfHost
{
    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    using Owin;

    using SlightUrl.Data;
    using SlightUrl.Data.Postgres;
    using SlightUrl.Service.Interfaces;

    public class Startup
    {
        public virtual void Configuration(IAppBuilder app)
        {
            var config = WebApi.Configuration();

            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(config);
        }

        protected virtual IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IInstanceFactory>().To<NinjectInstanceFactory>();
            kernel.Bind<SlightContext>().To<PostgresSlightContext>();

            return kernel;
        }
    }

    public class NinjectInstanceFactory : IInstanceFactory
    {
        private readonly IKernel _kernel;

        public NinjectInstanceFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public T CreateInstance<T>()
        {
            return _kernel.Get<T>();
        }
    }
}