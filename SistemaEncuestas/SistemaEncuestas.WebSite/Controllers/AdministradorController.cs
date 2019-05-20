namespace SistemaEncuestas.WebSite.Controllers
{
    #region Namespaces

    using Entidades.Entities;
    using Models;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    #endregion

    public class AdministradorController : BaseController
    {
        #region Public Methods

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(AutenticationModel autentication)
        {
            try
            {
                Administrador administradorEntity = new Administrador()
                {
                    Usuario = autentication.Usuario,
                    Password = autentication.Password
                };

                var respuesta = await ClienteAdministrador.ValidarAccesoAsync(administradorEntity);

                return RedirectToAction("Dashboard", "MainAdministrador");
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