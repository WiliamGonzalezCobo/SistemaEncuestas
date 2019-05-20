namespace SistemaEncuestas.Utilidades.Correo
{
    public class CorreoEntity
    {
        public string SMTP { get; set; }
        public int Puerto { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public bool EsSSL { get; set; }
        public bool EsHTML { get; set; }
    }
}
