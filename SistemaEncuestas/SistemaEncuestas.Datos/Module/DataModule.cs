namespace SistemaEncuestas.Datos.Module
{
    #region Namespaces

    using Autofac;
    using Infrastructure.Class;
    using Infrastructure.Interfaces;
    using Repository.Class;
    using Repository.Interfaces;

    #endregion

    public class DataModule : Module
    {
        #region Protected Methods

        protected override void Load(ContainerBuilder builder)
        {
            // Infrastructure
            builder.RegisterType<ConnectionFactory>().As<IConnectionFactory>();

            // Repository
            builder.RegisterType<AdministradorRepositorio>().As<IAdministradorRepositorio>();
            builder.RegisterType<AuditoriaIngresoRepositorio>().As<IAuditoriaIngresoRepositorio>();
            builder.RegisterType<EmpresaRepositorio>().As<IEmpresaRepositorio>();
            builder.RegisterType<EncuestaRepositorio>().As<IEncuestaRepositorio>();
            builder.RegisterType<ItemPreguntaRepositorio>().As<IItemPreguntaRepositorio>();
            builder.RegisterType<MetadataRepositorio>().As<IMetadataRepositorio>();
            builder.RegisterType<PagoPlanRepositorio>().As<IPagoPlanRepositorio>();
            builder.RegisterType<PlanEncuestaRepositorio>().As<IPlanEncuestaRepositorio>();
            builder.RegisterType<PreguntaRepositorio>().As<IPreguntaRepositorio>();
            builder.RegisterType<RespuestaRepositorio>().As<IRespuestaRepositorio>();
            builder.RegisterType<UsuarioEmpresaRepositorio>().As<IUsuarioEmpresaRepositorio>();
        }

        #endregion
    }
}
