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

    [RoutePrefix("api/pagoplan")]
    public class PagoPlanController : ApiController
    {
        #region Attributes

        private readonly IPagoPlanRepositorio _pagoPlanRepositorio;
        private readonly LogsManager _logsManager;

        #endregion

        #region Constructors

        public PagoPlanController(IPagoPlanRepositorio pagoPlanRepositorio)
        {
            _pagoPlanRepositorio = pagoPlanRepositorio;
            _logsManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("obtener")]
        public HttpResponseMessage Get()
        {
            List<PagoPlan> listaPagoPlan = (List<PagoPlan>)_pagoPlanRepositorio.GetAll();

            if (listaPagoPlan == null && listaPagoPlan.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeListaSuccess, listaPagoPlan.Count));
            _pagoPlanRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, listaPagoPlan);
        }

        [HttpGet]
        [Route("obtenerporid/{idPago?}")]
        public HttpResponseMessage Get(string idPago = null)
        {
            if (string.IsNullOrWhiteSpace(idPago))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            PagoPlan pagoPlan = _pagoPlanRepositorio.Find(idPago);

            if (pagoPlan == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, idPago));
            _pagoPlanRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, pagoPlan);
        }

        [HttpPost]
        [Route("agregar")]
        public HttpResponseMessage Post(PagoPlan pagoPlan)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (pagoPlan == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _pagoPlanRepositorio.Insert(pagoPlan).IdPago;
                mensaje = Mensajes.MensajePagoPlanSuccess;

                _logsManager.Info(mensaje);
                _pagoPlanRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _pagoPlanRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        #endregion
    }
}
