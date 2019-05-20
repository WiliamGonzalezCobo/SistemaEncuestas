namespace SistemaEncuestas.Entidades.Entities
{
    public class UsuarioEmpresa
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string IdEmpresa { get; set; }
        public Empresa Empresa { get; set; }
        public string Correo { get; set; }
        public string IdRol { get; set; }
        public RolEmpresa RolEmpresa { get; set; }
        public bool Activo { get; set; }
    }
}
