namespace SistemaEncuestas.Entidades.Entities
{
    public class Administrador
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public bool Activo { get; set; }
    }
}
