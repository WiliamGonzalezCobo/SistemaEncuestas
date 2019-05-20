namespace SistemaEncuestas.Servicios.Controllers
{
    #region Namespaces

    using Datos.Repository.Interfaces;
    using Entidades.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using Utilidades.Logs;
    using Utilidades.Recursos;

    #endregion

    [RoutePrefix("api/administrador")]
    public class AdministradorController : ApiController
    {
        #region Attributes

        private readonly IAdministradorRepositorio _administradorRepositorio;
        private readonly LogsManager _logsManager;

        #endregion

        #region Constructors

        public AdministradorController(IAdministradorRepositorio administradorRepositorio)
        {
            _administradorRepositorio = administradorRepositorio;
            _logsManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("obtener")]
        public HttpResponseMessage Get()
        {
            List<Administrador> listaAdministrador = (List<Administrador>)_administradorRepositorio.GetAll();

            if (listaAdministrador == null && listaAdministrador.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeListaSuccess, listaAdministrador.Count));
            _administradorRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, listaAdministrador);
        }

        [HttpPost]
        [Route("validarcredenciales")]
        public HttpResponseMessage PostValidarAcceso(Administrador administrador)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (administrador == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;
                    _logsManager.Warning(mensaje);
                    Request.CreateResponse(HttpStatusCode.BadRequest, mensaje);
                }

                Administrador datosAdministrador = _administradorRepositorio.GetFullById(administrador.Usuario, administrador.Password).FirstOrDefault();

                if (datosAdministrador == null)
                {
                    mensaje = Mensajes.MensajeLoginError;

                    _logsManager.Warning(mensaje);

                    return Request.CreateResponse(HttpStatusCode.Unauthorized, mensaje);
                }

                mensaje = Mensajes.MensajeLoginSuccess;

                _logsManager.Info(mensaje);
                _administradorRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, datosAdministrador);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _administradorRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        #endregion
    }
}