namespace SistemaEncuestas.Datos.Repository.Class
{
    #region Namespaces

    using Dapper;
    using Entidades.Entities;
    using Infrastructure.Interfaces;
    using Interfaces;
    using System;
    using System.Linq;

    #endregion

    internal class PreguntaRepositorio : IPreguntaRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public PreguntaRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~PreguntaRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public Pregunta Find(string id)
        {
            return _connection.Connection.Query<Pregunta>(
               "SELECT IdPregunta FROM Pregunta WHERE IdPregunta = @Id",
               new { Id = id }).SingleOrDefault();
        }

        public Pregunta Insert(Pregunta question)
        {
            var sqlQuery = "INSERT INTO Pregunta (IdPregunta, Descripcion, Requerido, IdEncuesta, IdMetadata) " +
                "VALUES(@IdPregunta, @Descripcion, @Requerido, @IdEncuesta, @IdMetadata);" +
                 "SELECT IdPregunta FROM Pregunta WHERE IdPregunta = @IdPregunta;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, question).Single();

            question.IdPregunta = resultId;

            return question;
        }

        public void Remove(string id)
        {
            _connection.Connection.Execute("DELETE FROM Pregunta WHERE IdPregunta = @Id", new { Id = id });
        }

        public Pregunta Update(Pregunta question)
        {
            var sqlQuery = "UPDATE Pregunta SET Descripcion = @Descripcion, Requerido = @Requerido, IdEncuesta = @IdEncuesta, IdMetadata = @IdMetadata " +
                "WHERE IdPregunta = @IdPregunta";
            _connection.Connection.Execute(sqlQuery, question);

            return question;
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
