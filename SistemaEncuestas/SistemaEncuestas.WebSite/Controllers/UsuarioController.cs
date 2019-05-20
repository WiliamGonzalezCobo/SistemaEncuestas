namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespace

    using Entidades.Comun;
    using Entidades.Entities;
    using Models;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    public class UsuarioController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public ActionResult Login()
        {
            Session.Clear();
            Session.Abandon();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(AutenticationModel autentication)
        {
            try
            {
                UsuarioEmpresa usuarioEntity = new UsuarioEmpresa
                {
                    Usuario = autentication.Usuario,
                    Password = autentication.Password
                };

                var respuesta = await ClienteUsuario.ValidarAccesoAsync(usuarioEntity);

                DataUser dataUser = new DataUser
                {
                    Usuario = respuesta.Usuario,
                    Nombre = string.Concat(respuesta.Nombre, " ", respuesta.PrimerApellido, " ", respuesta.SegundoApellido),
                    IdEmpresa = respuesta.Empresa.IdEmpresa,
                    NombreEmpresa = respuesta.Empresa.Nombre,
                    IdRol = respuesta.RolEmpresa.IdRol,
                    NombreRol = respuesta.RolEmpresa.Descripcion
                };

                Session["_SessionUser"] = dataUser;

                return RedirectToAction("MisEncuestas", "Encuesta", new { idEmpresa = respuesta.Empresa.IdEmpresa });
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;

                return View(autentication);
            }
        }

        #endregion
    }
}