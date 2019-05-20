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

    [RoutePrefix("api/metadata")]
    public class MetadataController : ApiController
    {
        #region Attributes

        private readonly IMetadataRepositorio _metadataRepositorio;
        private readonly LogsManager _logsManager;

        #endregion

        #region Constructors

        public MetadataController(IMetadataRepositorio metadataRepositorio)
        {
            _metadataRepositorio = metadataRepositorio;
            _logsManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("obtener")]
        public HttpResponseMessage Get()
        {
            List<Metadata> listaMetadata = (List<Metadata>)_metadataRepositorio.GetAll();

            if (listaMetadata == null && listaMetadata.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeListaSuccess, listaMetadata.Count));
            _metadataRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, listaMetadata);
        }

        [HttpGet]
        [Route("obtenerporid/{idMetadata?}")]
        public HttpResponseMessage Get(string idMetadata = null)
        {
            if (string.IsNullOrWhiteSpace(idMetadata))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            Metadata metadata = _metadataRepositorio.Find(idMetadata);

            if (metadata == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, idMetadata));
            _metadataRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, metadata);
        }

        [HttpPost]
        [Route("agregar")]
        public HttpResponseMessage Post(Metadata metadata)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (metadata == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _metadataRepositorio.Insert(metadata).IdMetadata;
                mensaje = Mensajes.MensajeMetadataSuccess;

                _logsManager.Info(mensaje);
                _metadataRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _metadataRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        #endregion
    }
}
