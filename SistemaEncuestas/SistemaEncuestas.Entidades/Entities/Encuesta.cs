namespace SistemaEncuestas.Entidades.Entities
{
    using System.Collections.Generic;

    public class Encuesta
    {
        public string IdEncuesta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Interno { get; set; }
        public string IdEmpresa { get; set; }
        public List<Pregunta> Preguntas { get; set; }
        public string Url { get; set; }
        public bool Activo { get; set; }
    }
}
