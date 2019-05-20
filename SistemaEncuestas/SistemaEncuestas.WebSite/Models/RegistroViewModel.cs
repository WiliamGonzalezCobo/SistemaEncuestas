namespace SistemaEncuestas.WebSite.Models
{
    public class RegistroViewModel
    {
        #region Constructors

        public RegistroViewModel()
        {
            Plan = new RegistroPlanViewModel();
            Empresa = new RegistroEmpresaViewModel();
            Usuario = new RegistroUsuarioViewModel();
            Pago = new RegistroPagoViewModel();
        }

        #endregion

        #region Properties

        public RegistroPlanViewModel Plan { get; set; }
        public RegistroEmpresaViewModel Empresa { get; set; }
        public RegistroUsuarioViewModel Usuario { get; set; }
        public RegistroPagoViewModel Pago { get; set; }

        #endregion
    }
}