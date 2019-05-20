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

    internal class UsuarioEmpresaRepositorio : IUsuarioEmpresaRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public UsuarioEmpresaRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~UsuarioEmpresaRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public UsuarioEmpresa Find(string id)
        {
            return _connection.Connection.Query<UsuarioEmpresa>(
               "SELECT Usuario, Password, Nombre, PrimerApellido, SegundoApellido, IdEmpresa, Correo, IdRol, Activo FROM UsuarioEmpresa WHERE Usuario = @Id",
               new { Id = id }).SingleOrDefault();
        }

        public IEnumerable<UsuarioEmpresa> GetAll()
        {
            return _connection.Connection.Query<UsuarioEmpresa>(
               "SELECT Usuario, Password, Nombre, PrimerApellido, SegundoApellido, IdEmpresa, Correo, IdRol, Activo FROM UsuarioEmpresa").ToList();
        }

        public IEnumerable<UsuarioEmpresa> GetFullById(string id, string password)
        {
            var result = _connection.Connection.Query<UsuarioEmpresa, Empresa, RolEmpresa, UsuarioEmpresa>(
                "SELECT u.Usuario, u.Nombre, u.PrimerApellido, u.SegundoApellido, u.IdEmpresa, e.Nit, e.Nombre, e.Direccion, e.SitioWeb, u.IdRol, r.Descripcion " +
                "FROM UsuarioEmpresa u INNER JOIN Empresa e ON e.IdEmpresa = u.IdEmpresa INNER JOIN RolEmpresa r ON r.IdRol = u.IdRol " +
                "WHERE u.Usuario = @Id AND u.Password = @Password AND u.Activo = 1;",
                (u, e, r) =>
                {
                    u.Empresa = e;
                    u.RolEmpresa = r;

                    return u;
                }, new { Id = id, Password = password },
                splitOn: "IdEmpresa,IdRol").AsQueryable();

            return result.ToList();
        }

        public UsuarioEmpresa Insert(UsuarioEmpresa companyRol)
        {
            var sqlQuery = "INSERT INTO UsuarioEmpresa (Usuario, Password, Nombre, PrimerApellido, SegundoApellido, IdEmpresa, Correo, IdRol, Activo) " +
                "VALUES(@Usuario, @Password, @Nombre, @PrimerApellido, @SegundoApellido, @IdEmpresa, @Correo, @IdRol, @Activo);" +
                 "SELECT Usuario FROM UsuarioEmpresa WHERE Usuario = @Usuario;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, companyRol).Single();

            companyRol.IdRol = resultId;

            return companyRol;
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
