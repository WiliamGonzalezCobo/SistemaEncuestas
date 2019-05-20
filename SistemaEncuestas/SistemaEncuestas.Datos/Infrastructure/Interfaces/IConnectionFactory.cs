namespace SistemaEncuestas.Datos.Infrastructure.Interfaces
{
    #region Namespaces

    using System.Data;

    #endregion

    internal interface IConnectionFactory
    {
        IDbConnection Connection { get; }
    }
}
