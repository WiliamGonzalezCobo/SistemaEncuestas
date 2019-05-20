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

    internal class EmpresaRepositorio : IEmpresaRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public EmpresaRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~EmpresaRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public Empresa Find(string id)
        {
            return _connection.Connection.Query<Empresa>(
               "SELECT IdEmpresa, Nit, Nombre, Direccion, SitioWeb, Activo FROM Empresa WHERE IdEmpresa = @Id",
               new { Id = id }).SingleOrDefault();
        }

        public IEnumerable<Empresa> GetAll()
        {
            return _connection.Connection.Query<Empresa>(
                "SELECT IdEmpresa, Nit, Nombre, Direccion, SitioWeb, Activo FROM Empresa").ToList();
        }

        public Empresa Insert(Empresa company)
        {
            var sqlQuery = "INSERT INTO Empresa (IdEmpresa, Nit, Nombre, Direccion, SitioWeb, Activo) VALUES(@IdEmpresa, @Nit, @Nombre, @Direccion, @SitioWeb, @Activo);" +
                 "SELECT IdEmpresa FROM Empresa WHERE IdEmpresa = @IdEmpresa;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, company).Single();

            company.IdEmpresa = resultId;

            return company;
        }

        public Empresa Update(Empresa company)
        {
            var sqlQuery = "UPDATE Empresa SET Nit = @Nit, Nombre = @Nombre, Direccion = @Direccion, SitioWeb = @SitioWeb, Activo = @Activo WHERE IdEmpresa = @IdEmpresa";
            _connection.Connection.Execute(sqlQuery, company);

            return company;
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
