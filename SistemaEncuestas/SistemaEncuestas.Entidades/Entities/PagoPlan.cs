namespace SistemaEncuestas.Entidades.Entities
{
    public class PagoPlan
    {
        public string IdPago { get; set; }
        public string TipoTarjeta { get; set; }
        public string NumeroTarjeta { get; set; }
        public string MesTarjeta { get; set; }
        public string AnioTarjeta { get; set; }
        public string PaisTarjeta { get; set; }
        public int NumeroCuotas { get; set; }
        public string Franquicia { get; set; }
        public string Usuario { get; set; }
        public string IdPlan { get; set; }
        public double Precio { get; set; }
        public string IdEmpresa { get; set; }
    }
}
