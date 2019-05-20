namespace SistemaEncuestas.Servicios
{
    #region Namespaces

    using Autofac;
    using Autofac.Integration.WebApi;
    using Module;
    using System.Web;
    using System.Web.Http;

    #endregion

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;

            // Autofac Dependency Injection
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<ServicesModule>();

            containerBuilder.RegisterWebApiFilterProvider(config);
            containerBuilder.RegisterHttpRequestMessage(config);

            var container = containerBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
