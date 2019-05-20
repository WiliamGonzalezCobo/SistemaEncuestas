namespace SistemaEncuestas.Entidades.Entities
{
    using System.Collections.Generic;

    public class PlanEncuesta
    {
        public string IdPlan { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Promocion { get; set; }
        public bool Activo { get; set; }
        public List<ItemPlanEncuesta> ItemPlanes { get; set; }
    }
}
