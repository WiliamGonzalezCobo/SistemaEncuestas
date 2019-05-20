namespace SistemaEncuestas.Servicios.Controllers
{
    #region Namespace

    using Datos.Repository.Interfaces;
    using Entidades.Entities;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;
    using Utilidades.Logs;
    using Utilidades.Recursos;

    #endregion

    [RoutePrefix("api/respuesta")]
    public class RespuestaController : ApiController
    {
        #region Attributes

        private readonly IRespuestaRepositorio _respuestaRepositorio;
        private readonly LogsManager _logsManager;

        #endregion

        #region Constructors

        public RespuestaController(IRespuestaRepositorio respuestaRepositorio)
        {
            _respuestaRepositorio = respuestaRepositorio;
            _logsManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Public Methods

        [HttpPost]
        [Route("agregar")]
        public HttpResponseMessage Post(Respuesta respuesta)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (respuesta == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _respuestaRepositorio.Insert(respuesta).IdRespuesta;
                mensaje = Mensajes.MensajeRespuestaSuccess;

                _logsManager.Info(mensaje);
                _respuestaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _respuestaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        #endregion
    }
}