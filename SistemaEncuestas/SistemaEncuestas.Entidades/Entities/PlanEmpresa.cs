namespace SistemaEncuestas.Entidades.Entities
{
    using System;

    public class PlanEmpresa
    {
        public string IdEmpresa { get; set; }
        public string IdPlan { get; set; }
        public DateTime FechaInicioSuscripcion { get; set; }
        public DateTime FechaFinSuscripcion { get; set; }
    }
}
