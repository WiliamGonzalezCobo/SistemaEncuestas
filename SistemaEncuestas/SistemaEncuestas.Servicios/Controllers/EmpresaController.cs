namespace SistemaEncuestas.Servicios.Controllers
{
    #region Namespaces

    using Datos.Repository.Interfaces;
    using Entidades.Entities;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using Utilidades.Logs;
    using Utilidades.Recursos;

    #endregion

    [RoutePrefix("api/empresa")]
    public class EmpresaController : ApiController
    {
        #region Attributes

        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly LogsManager _logsManager;

        #endregion

        #region Contructors

        public EmpresaController(IEmpresaRepositorio empresaRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
            _logsManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("obtener")]
        public HttpResponseMessage Get()
        {
            List<Empresa> listaEmpresas = (List<Empresa>)_empresaRepositorio.GetAll();

            if (listaEmpresas == null && listaEmpresas.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeListaSuccess, listaEmpresas.Count));
            _empresaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, listaEmpresas);
        }

        [HttpGet]
        [Route("obtenerporid/{idEmpresa?}")]
        public HttpResponseMessage Get(string idEmpresa = null)
        {
            if (string.IsNullOrWhiteSpace(idEmpresa))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

           Empresa empresa = _empresaRepositorio.Find(idEmpresa);

            if (empresa == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, idEmpresa));
            _empresaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, empresa);
        }

        [HttpPost]
        [Route("agregar")]
        public HttpResponseMessage Post(Empresa empresa)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (empresa == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _empresaRepositorio.Insert(empresa).IdEmpresa;
                mensaje = Mensajes.MensajeEmpresaSuccess;

                _logsManager.Info(mensaje);
                _empresaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _empresaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        [HttpPut]
        [Route("editar")]
        public HttpResponseMessage Put(Empresa empresa)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (empresa == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _empresaRepositorio.Update(empresa).IdEmpresa;
                mensaje = Mensajes.MensajeEmpresaSuccess;

                _logsManager.Info(mensaje);
                _empresaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _empresaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        #endregion
    }
}
