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

    internal class MetadataRepositorio : IMetadataRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public MetadataRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~MetadataRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public Metadata Find(string id)
        {
            return _connection.Connection.Query<Metadata>(
               "SELECT IdMetadata, Nombre FROM Metadata WHERE IdMetadata = @Id",
               new { Id = id }).SingleOrDefault();
        }

        public IEnumerable<Metadata> GetAll()
        {
            return _connection.Connection.Query<Metadata>(
                "SELECT IdMetadata, Nombre FROM Metadata").ToList();
        }

        public Metadata Insert(Metadata metadata)
        {
            var sqlQuery = "INSERT INTO Metadata (IdMetadata, Nombre) VALUES(@IdMetadata, @Nombre);" +
                 "SELECT IdMetadata FROM Metadata WHERE IdMetadata = @IdMetadata;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, metadata).Single();

            metadata.IdMetadata = resultId;

            return metadata;
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
