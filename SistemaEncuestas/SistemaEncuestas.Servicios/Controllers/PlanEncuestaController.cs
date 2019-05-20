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

    [RoutePrefix("api/plan")]
    public class PlanEncuestaController : ApiController
    {
        #region Attributes

        private IPlanEncuestaRepositorio _planEncuestaRepositorio;
        private readonly LogsManager _logsManager;

        #endregion

        #region Constructor

        public PlanEncuestaController(IPlanEncuestaRepositorio planEncuestaRepositorio)
        {
            _planEncuestaRepositorio = planEncuestaRepositorio;
            _logsManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("obtener")]
        public HttpResponseMessage Get()
        {
            List<PlanEncuesta> listaPlanes = (List<PlanEncuesta>)_planEncuestaRepositorio.GetAll();

            if (listaPlanes == null || listaPlanes.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeListaSuccess, listaPlanes.Count));
            _planEncuestaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, listaPlanes);
        }

        [HttpGet]
        [Route("obtenerporid/{idPlan?}")]
        public HttpResponseMessage Get(string idPlan = null)
        {
            if (string.IsNullOrWhiteSpace(idPlan))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            PlanEncuesta plan = _planEncuestaRepositorio.Find(idPlan);

            if (plan == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, idPlan));
            _planEncuestaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, plan);
        }

        [HttpPost]
        [Route("agregar")]
        public HttpResponseMessage Post(PlanEncuesta plan)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (plan == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _planEncuestaRepositorio.Insert(plan).IdPlan;
                mensaje = Mensajes.MensajePlanSuccess;

                _logsManager.Info(mensaje);
                _planEncuestaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _planEncuestaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        #endregion
    }
}
