namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using LogicaNegocio.WebApiClient;
    using System.Web.Mvc;
    using Utilidades.Settings;

    #endregion

    public class BaseController : Controller
    {
        #region Attributes

        private readonly string _urlServicio = SettingsManager.URLServicio;

        private AdministradorClient _clienteAdministrador;
        private AuditoriaIngresoClient _clientAuditoriaIngreso;
        private EmpresaClient _clienteEmpresa;
        private EncuestaClient _clienteEncuesta;
        private MetadataClient _clienteMetadata;
        private PagoPlanClient _clientePagoPlan;
        private PlanEncuestaClient _clientePlanEncuesta;
        private RespuestaClient _clienteRespuesta;
        private UsuarioEmpresaClient _clienteUsuario;

        #endregion

        #region Properties

        public AdministradorClient ClienteAdministrador
        {
            get
            {
                if (_clienteAdministrador == null)
                {
                    _clienteAdministrador = new AdministradorClient(_urlServicio);
                }

                return _clienteAdministrador;
            }
        }

        public AuditoriaIngresoClient ClienteAuditoriaIngreso
        {
            get
            {
                if (_clientAuditoriaIngreso == null)
                {
                    _clientAuditoriaIngreso = new AuditoriaIngresoClient(_urlServicio);
                }

                return _clientAuditoriaIngreso;
            }
        }

        public EmpresaClient ClienteEmpresa
        {
            get
            {
                if (_clienteEmpresa == null)
                {
                    _clienteEmpresa = new EmpresaClient(_urlServicio);
                }

                return _clienteEmpresa;
            }
        }

        public EncuestaClient ClienteEncuesta
        {
            get
            {
                if (_clienteEncuesta == null)
                {
                    _clienteEncuesta = new EncuestaClient(_urlServicio);
                }

                return _clienteEncuesta;
            }
        }

        public MetadataClient ClienteMetadata
        {
            get
            {
                if (_clienteMetadata == null)
                {
                    _clienteMetadata = new MetadataClient(_urlServicio);
                }

                return _clienteMetadata;
            }
        }

        public PagoPlanClient ClientePagoPlan
        {
            get
            {
                if (_clientePagoPlan == null)
                {
                    _clientePagoPlan = new PagoPlanClient(_urlServicio);
                }

                return _clientePagoPlan;
            }
        }

        public PlanEncuestaClient ClientePlanEncuesta
        {
            get
            {
                if (_clientePlanEncuesta == null)
                {
                    _clientePlanEncuesta = new PlanEncuestaClient(_urlServicio);
                }

                return _clientePlanEncuesta;
            }
        }

        public RespuestaClient ClienteRespuesta
        {
            get
            {
                if (_clienteRespuesta == null)
                {
                    _clienteRespuesta = new RespuestaClient(_urlServicio);
                }

                return _clienteRespuesta;
            }
        }

        public UsuarioEmpresaClient ClienteUsuario
        {
            get
            {
                if (_clienteUsuario == null)
                {
                    _clienteUsuario = new UsuarioEmpresaClient(_urlServicio);
                }

                return _clienteUsuario;
            }
        }

        #endregion
    }
}