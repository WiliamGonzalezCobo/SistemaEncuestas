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

    [RoutePrefix("api/encuesta")]
    public class EncuestaController : ApiController
    {
        #region Attributes

        private readonly IEncuestaRepositorio _encuestaRepositorio;
        private readonly IPreguntaRepositorio _preguntaRepositorio;
        private readonly IItemPreguntaRepositorio _itemPreguntaRepositorio;

        private readonly LogsManager _logsManager;

        #endregion

        #region Constructors

        public EncuestaController(IEncuestaRepositorio encuestaRepositorio, IPreguntaRepositorio preguntaRepositorio, IItemPreguntaRepositorio itemPreguntaRepositorio)
        {
            _encuestaRepositorio = encuestaRepositorio;
            _preguntaRepositorio = preguntaRepositorio;
            _itemPreguntaRepositorio = itemPreguntaRepositorio;

            _logsManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("obtenerporid/{idEncuesta?}")]
        public HttpResponseMessage Get(string idEncuesta = null)
        {
            if (string.IsNullOrWhiteSpace(idEncuesta))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            Encuesta encuesta = _encuestaRepositorio.Find(idEncuesta);

            if (encuesta == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, idEncuesta));
            _encuestaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, encuesta);
        }

        [HttpGet]
        [Route("obtenerporids/{idEmpresa}/{idEncuesta}")]
        public HttpResponseMessage Get(string idEmpresa = null, string idEncuesta = null)
        {
            if (string.IsNullOrWhiteSpace(idEncuesta) && string.IsNullOrWhiteSpace(idEmpresa))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            Encuesta encuesta = _encuestaRepositorio.GetFullByIdEmpresaAndIdEncuesta(idEmpresa, idEncuesta);

            if (encuesta == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, idEncuesta));
            _encuestaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, encuesta);
        }

        [HttpGet]
        [Route("obtenerporidempresa/{idEmpresa?}")]
        public HttpResponseMessage GetByIdEmpresa(string idEmpresa = null)
        {
            List<Encuesta> listaEncuesta = (List<Encuesta>)_encuestaRepositorio.GetByIdEmpresa(idEmpresa);

            if (listaEncuesta == null && listaEncuesta.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeListaSuccess, listaEncuesta.Count));
            _encuestaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, listaEncuesta);
        }

        [HttpGet]
        [Route("obtenerrespuestas/{idEncuesta?}")]
        public HttpResponseMessage GetRespuestas(string idEncuesta = null)
        {
            if (string.IsNullOrWhiteSpace(idEncuesta))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            IEnumerable<Dictionary<string,string>> respuestas = _encuestaRepositorio.GetRespuestasEncuesta(idEncuesta);

            if (respuestas == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, idEncuesta));
            _encuestaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, respuestas);
        }

        [HttpGet]
        [Route("obtenerrespuestasporid/{idEncuesta?}")]
        public HttpResponseMessage GetRespuestasById(string idEncuesta = null)
        {
            if (string.IsNullOrWhiteSpace(idEncuesta))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            IEnumerable<RespuestasEncuesta> countRespuesta = _encuestaRepositorio.GetRespuestasEncuestaById(idEncuesta);

            if (countRespuesta == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, idEncuesta));
            _encuestaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, countRespuesta);
        }

        [HttpPost]
        [Route("agregar")]
        public HttpResponseMessage Post(Encuesta encuesta)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (encuesta == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _encuestaRepositorio.Insert(encuesta).IdEncuesta;

                if (string.IsNullOrWhiteSpace(content))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, Mensajes.MensajeErrorEncuesta);
                }

                foreach (var pregunta in encuesta.Preguntas)
                {
                    content = _preguntaRepositorio.Insert(pregunta).IdPregunta;

                    if (string.IsNullOrWhiteSpace(content))
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, Mensajes.MensajeErrorPregunta);
                    }

                    foreach (var itemPregunta in pregunta.ItemsPreguntas)
                    {
                        content = _itemPreguntaRepositorio.Insert(itemPregunta).IdItemPregunta;

                        if (string.IsNullOrWhiteSpace(content))
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, Mensajes.MensajeErrorOpcion);
                        }
                    }
                }

                mensaje = Mensajes.MensajeEncuestaSuccess;

                _logsManager.Info(mensaje);
                _encuestaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _encuestaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        [HttpPut]
        [Route("editar")]
        public HttpResponseMessage Put(Encuesta encuesta)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (encuesta == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _encuestaRepositorio.Update(encuesta).IdEncuesta;

                if (string.IsNullOrWhiteSpace(content))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, Mensajes.MensajeErrorEncuesta);
                }

                foreach (var pregunta in encuesta.Preguntas)
                {
                    if (pregunta.Eliminado)
                    {
                        _preguntaRepositorio.Remove(pregunta.IdPregunta);
                    }
                    else if (_preguntaRepositorio.Find(pregunta.IdPregunta) == null)
                    {
                        content = _preguntaRepositorio.Insert(pregunta).IdPregunta;
                    }
                    else
                    {
                        content = _preguntaRepositorio.Update(pregunta).IdPregunta;
                    }

                    if (string.IsNullOrWhiteSpace(content))
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, Mensajes.MensajeErrorPregunta);
                    }

                    foreach (var itemPregunta in pregunta.ItemsPreguntas)
                    {
                        if (itemPregunta.Eliminado)
                        {
                            _itemPreguntaRepositorio.Remove(itemPregunta.IdItemPregunta);
                        }
                        else if (_itemPreguntaRepositorio.Find(itemPregunta.IdItemPregunta) == null)
                        {
                            content = _itemPreguntaRepositorio.Insert(itemPregunta).IdItemPregunta;
                        }
                        else
                        {
                            content = _itemPreguntaRepositorio.Update(itemPregunta).IdItemPregunta;
                        }

                        if (string.IsNullOrWhiteSpace(content))
                        {
                            return Request.CreateResponse(HttpStatusCode.InternalServerError, Mensajes.MensajeErrorOpcion);
                        }
                    }
                }

                mensaje = Mensajes.MensajeEncuestaSuccess;

                _logsManager.Info(mensaje);
                _encuestaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _encuestaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        [HttpDelete]
        [Route("delete/{idEncuesta?}")]
        public HttpResponseMessage Delete(string idEncuesta = null)
        {
            if (string.IsNullOrWhiteSpace(idEncuesta))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            if (_encuestaRepositorio.Remove(idEncuesta) > 0)
            {

                _logsManager.Info(string.Format(Mensajes.MessageDeleteEncuestaSuccess, idEncuesta));
                _encuestaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, Mensajes.MessageDeleteEncuestaSuccess);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, Mensajes.MessageDeleteEncuestaFailed);
            }
        }

        #endregion
    }
}
