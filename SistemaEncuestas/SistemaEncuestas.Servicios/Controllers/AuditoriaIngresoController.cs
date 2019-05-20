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

    [RoutePrefix("api/auditoriaingreso")]
    public class AuditoriaIngresoController : ApiController
    {
        #region Attributes

        private readonly IAuditoriaIngresoRepositorio _auditoriaIngresoRepositorio;
        private readonly LogsManager _logsManager;

        #endregion

        #region Constructors

        public AuditoriaIngresoController(IAuditoriaIngresoRepositorio auditoriaIngresoRepositorio)
        {
            _auditoriaIngresoRepositorio = auditoriaIngresoRepositorio;
            _logsManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("obtener")]
        public HttpResponseMessage Get()
        {
            List<AuditoriaIngreso> listaAuditoriaIngreso = (List<AuditoriaIngreso>)_auditoriaIngresoRepositorio.GetAll();

            if (listaAuditoriaIngreso == null && listaAuditoriaIngreso.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeListaSuccess, listaAuditoriaIngreso.Count));
            _auditoriaIngresoRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, listaAuditoriaIngreso);
        }

        [HttpGet]
        [Route("obtenerporid/{idAuditoria?}")]
        public HttpResponseMessage Get(string idAuditoria = null)
        {
            if (string.IsNullOrWhiteSpace(idAuditoria))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            List<AuditoriaIngreso> listaAuditoriaIngreso = (List<AuditoriaIngreso>)_auditoriaIngresoRepositorio.GetFullById(idAuditoria);

            if (listaAuditoriaIngreso == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, idAuditoria));
            _auditoriaIngresoRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, listaAuditoriaIngreso);
        }

        [HttpPost]
        [Route("agregar")]
        public HttpResponseMessage Post(AuditoriaIngreso auditoriaIngreso)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (auditoriaIngreso == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _auditoriaIngresoRepositorio.Insert(auditoriaIngreso).Usuario;
                mensaje = Mensajes.MensajeAuditoriaIngresoSuccess;

                _logsManager.Info(mensaje);
                _auditoriaIngresoRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _auditoriaIngresoRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        #endregion
    }
}
