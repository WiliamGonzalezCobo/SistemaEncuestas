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

    internal class AdministradorRepositorio : IAdministradorRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public AdministradorRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~AdministradorRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public IEnumerable<Administrador> GetAll()
        {
            return _connection.Connection.Query<Administrador>(
                "SELECT Usuario, Password, Nombre, Apellidos, Correo, Activo FROM Administrador").ToList();
        }

        public IEnumerable<Administrador> GetFullById(string id, string password)
        {
            return _connection.Connection.Query<Administrador>(
                "SELECT Usuario, Nombre, Apellidos, Correo FROM Administrador WHERE Usuario = @Id AND Password = @Password AND Activo = 1",
                new { Id = id, Password = password }).ToList();
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
