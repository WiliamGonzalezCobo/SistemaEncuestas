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

    [RoutePrefix("api/usuario")]
    public class UsuarioEmpresaController : ApiController
    {
        #region Attributes

        private readonly IUsuarioEmpresaRepositorio _usuarioEmpresaRepositorio;
        private readonly LogsManager _logsManager;

        #endregion

        #region Constructors

        public UsuarioEmpresaController(IUsuarioEmpresaRepositorio usuarioEmpresaRepositorio)
        {
            _usuarioEmpresaRepositorio = usuarioEmpresaRepositorio;
            _logsManager = new LogsManager(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Public Methods

        [HttpGet]
        [Route("obtener")]
        public HttpResponseMessage Get()
        {
            List<UsuarioEmpresa> listaUsuarios = (List<UsuarioEmpresa>)_usuarioEmpresaRepositorio.GetAll();

            if (listaUsuarios == null && listaUsuarios.Count < 0)
            {
                string mensaje = Mensajes.MensajeListaWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeListaSuccess, listaUsuarios.Count));
            _usuarioEmpresaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, listaUsuarios);
        }

        [HttpGet]
        [Route("obtenerporid/{login?}")]
        public HttpResponseMessage Get(string login = null)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                string mensaje = Mensajes.MensajeParametroWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
            }

            UsuarioEmpresa precioPlan = _usuarioEmpresaRepositorio.Find(login);

            if (precioPlan == null)
            {
                string mensaje = Mensajes.MensajeBusquedaEntidadWarning;

                _logsManager.Warning(mensaje);

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, mensaje);
            }

            _logsManager.Info(string.Format(Mensajes.MensajeBusquedaEntidadSuccess, login));
            _usuarioEmpresaRepositorio.Dispose();

            return Request.CreateResponse(HttpStatusCode.OK, precioPlan);
        }

        [HttpPost]
        [Route("agregar")]
        public HttpResponseMessage Post(UsuarioEmpresa usuario)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (usuario == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;

                    _logsManager.Warning(mensaje);

                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, mensaje);
                }

                content = _usuarioEmpresaRepositorio.Insert(usuario).Usuario;
                mensaje = Mensajes.MensajeUsuarioSuccess;

                _logsManager.Info(mensaje);
                _usuarioEmpresaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, mensaje);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _usuarioEmpresaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        [HttpPost]
        [Route("validarcredenciales")]
        public HttpResponseMessage PostValidarAcceso(UsuarioEmpresa usuario)
        {
            string content = string.Empty;
            string mensaje = string.Empty;

            try
            {
                if (usuario == null)
                {
                    mensaje = Mensajes.MensajeParametroWarning;
                    _logsManager.Warning(mensaje);
                    Request.CreateResponse(HttpStatusCode.BadRequest, mensaje);
                }

                UsuarioEmpresa datosUsuario = _usuarioEmpresaRepositorio.GetFullById(usuario.Usuario, usuario.Password).FirstOrDefault();

                if (datosUsuario == null)
                {
                    mensaje = Mensajes.MensajeLoginError;
                    _logsManager.Warning(mensaje);
                    return Request.CreateResponse(HttpStatusCode.Unauthorized, mensaje);
                }

                mensaje = Mensajes.MensajeLoginSuccess;

                _logsManager.Info(mensaje);
                _usuarioEmpresaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.OK, datosUsuario);
            }
            catch (ArgumentNullException ex)
            {
                mensaje = ex.Message;

                _logsManager.Error(mensaje, ex);
                _usuarioEmpresaRepositorio.Dispose();

                return Request.CreateResponse(HttpStatusCode.InternalServerError, mensaje);
            }
        }

        #endregion
    }
}
