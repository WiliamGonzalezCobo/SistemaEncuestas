namespace SistemaEncuestas.Servicios.Module
{
    #region Namespaces

    using Autofac;
    using Autofac.Integration.WebApi;
    using Datos.Module;
    using System.Reflection;

    using Module = Autofac.Module;

    #endregion

    public class ServicesModule : Module
    {
        #region Protected Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetAssembly(typeof(ServicesModule)));
            builder.RegisterModule<DataModule>();
        }

        #endregion
    }
}