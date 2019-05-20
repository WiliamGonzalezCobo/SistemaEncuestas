namespace SistemaEncuestas.Entidades.Entities
{
    using System.Collections.Generic;

    public class Pregunta
    {
        public string IdPregunta { get; set; }
        public string Descripcion { get; set; }
        public bool Requerido { get; set; }
        public string IdEncuesta { get; set; }
        public string IdMetadata { get; set; }
        public Metadata MetadataPregunta { get; set; }
        public List<ItemPregunta> ItemsPreguntas { get; set; }
        public List<Respuesta> Respuestas { get; set; }
        public bool Eliminado { get; set; }
    }
}
