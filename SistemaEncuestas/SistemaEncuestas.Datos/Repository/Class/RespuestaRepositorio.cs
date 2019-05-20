namespace SistemaEncuestas.Datos.Repository.Class
{
    #region Namespaces

    using Dapper;
    using Infrastructure.Interfaces;
    using Interfaces;
    using Entidades.Entities;
    using System;
    using System.Linq;

    #endregion

    internal class RespuestaRepositorio : IRespuestaRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public RespuestaRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~RespuestaRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public Respuesta Insert(Respuesta respuesta)
        {
            var sqlQuery = "INSERT INTO Respuesta (IdRespuesta, IdPregunta, Valor, Referencia) VALUES(@IdRespuesta, @IdPregunta, @Valor, @Referencia);" +
                 "SELECT IdRespuesta FROM Respuesta WHERE IdRespuesta = @IdRespuesta;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, respuesta).Single();

            respuesta.IdRespuesta = resultId;

            return respuesta;
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
