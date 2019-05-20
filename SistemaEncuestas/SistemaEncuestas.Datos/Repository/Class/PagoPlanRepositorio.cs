namespace SistemaEncuestas.Datos.Repository.Class
{
    #region Namespaces

    using Dapper;
    using Entidades.Entities;
    using Infrastructure.Interfaces;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    internal class PagoPlanRepositorio : IPagoPlanRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public PagoPlanRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~PagoPlanRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public PagoPlan Find(string id)
        {
            return _connection.Connection.Query<PagoPlan>(
               "SELECT IdPago, TipoTarjeta, NumeroTarjeta, MesTarjeta, AnioTarjeta, PaisTarjeta, NumeroCuotas, " +
               "Franquicia, Usuario, IdPlan, Precio, IdEmpresa FROM PagoPlan WHERE IdPago = @Id",
               new { Id = id }).SingleOrDefault();
        }

        public IEnumerable<PagoPlan> GetAll()
        {
            return _connection.Connection.Query<PagoPlan>(
               "SELECT IdPago, TipoTarjeta, NumeroTarjeta, MesTarjeta, AnioTarjeta, PaisTarjeta, NumeroCuotas, " +
               "Franquicia, Usuario, IdPlan, Precio, IdEmpresa FROM PagoPlan").ToList();
        }

        public PagoPlan Insert(PagoPlan planPay)
        {
            var sqlQuery = "INSERT INTO PagoPlan (IdPago, TipoTarjeta, NumeroTarjeta, MesTarjeta, AnioTarjeta, PaisTarjeta, NumeroCuotas, " +
               "Franquicia, Usuario, IdPlan, Precio, IdEmpresa) VALUES(@IdPago, @TipoTarjeta, @NumeroTarjeta, @MesTarjeta, @AnioTarjeta, " +
               "@PaisTarjeta, @NumeroCuotas, @Franquicia, @Usuario, @IdPlan, @Precio, @IdEmpresa);" +
                "SELECT IdPago FROM PagoPlan WHERE IdPago = @IdPago;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, planPay).Single();

            planPay.IdPago = resultId;

            return planPay;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection.Connection.Close();
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
