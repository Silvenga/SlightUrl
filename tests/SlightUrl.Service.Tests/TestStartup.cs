namespace SlightUrl.Service.Tests
{
    using System;

    using Ninject;

    using SlightUrl.Data;
    using SlightUrl.Service.SelfHost;

    public class TestStartup : Startup
    {
        [ThreadStatic] public static Action<IKernel> PostKerrelCreation;

        private static Action<IKernel> _postKerrelCreationLocal;

        public Action<IKernel> PostKerrelCreationLocal
        {
            get { return _postKerrelCreationLocal = _postKerrelCreationLocal ?? PostKerrelCreation; }
        }

        protected override IKernel CreateKernel()
        {
            var kernel = base.CreateKernel();

            kernel.Rebind<SlightContext>().To<SlightContext>();
            PostKerrelCreationLocal?.Invoke(kernel);

#if NCRUNCH
                        kernel.Load(typeof(Ninject.Web.WebApi.WebApiModule).Assembly);
#endif

            return kernel;
        }
    }
}