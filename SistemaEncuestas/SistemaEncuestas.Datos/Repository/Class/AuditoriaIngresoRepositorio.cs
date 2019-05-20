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

    internal class AuditoriaIngresoRepositorio : IAuditoriaIngresoRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public AuditoriaIngresoRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~AuditoriaIngresoRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public IEnumerable<AuditoriaIngreso> GetAll()
        {
            return _connection.Connection.Query<AuditoriaIngreso>(
                "SELECT Usuario, Fechaingreso FROM AuditoriaIngreso").ToList();
        }

        public IEnumerable<AuditoriaIngreso> GetFullById(string id)
        {
            return _connection.Connection.Query<AuditoriaIngreso>(
               "SELECT Usuario, Fechaingreso FROM AuditoriaIngreso WHERE Usuario = @Id",
               new { Id = id }).ToList();
        }

        public AuditoriaIngreso Insert(AuditoriaIngreso auditIncome)
        {
            var sqlQuery = "INSERT INTO AuditoriaIngreso (Usuario, Fechaingreso) VALUES(@Usuario, @Fechaingreso);" +
                 "SELECT Usuario FROM AuditoriaIngreso WHERE Usuario = @Usuario AND Fechaingreso = @Fechaingreso;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, auditIncome).Single();

            auditIncome.Usuario = resultId;

            return auditIncome;
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
