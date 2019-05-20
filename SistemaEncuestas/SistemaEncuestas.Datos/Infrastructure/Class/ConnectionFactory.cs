namespace SistemaEncuestas.Datos.Infrastructure.Class
{
    #region Namespaces

    using Interfaces;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;

    #endregion

    internal class ConnectionFactory : IConnectionFactory
    {
        #region Attributes

        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SistemaEncuestasConnection"].ConnectionString;

        #endregion

        #region Properties

        public IDbConnection Connection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                var connection = factory.CreateConnection();

                connection.ConnectionString = _connectionString;
                connection.Open();

                return connection;
            }
        }

        #endregion
    }
}
