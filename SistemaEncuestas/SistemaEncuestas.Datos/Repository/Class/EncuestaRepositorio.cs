namespace SistemaEncuestas.Datos.Repository.Class
{
    #region Namespaces

    using Dapper;
    using Entidades.Entities;
    using Infrastructure.Interfaces;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    #endregion

    internal class EncuestaRepositorio : IEncuestaRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public EncuestaRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~EncuestaRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public Encuesta Find(string id)
        {
            return _connection.Connection.Query<Encuesta>(
               "SELECT IdEncuesta, Nombre, Descripcion, Interno, IdEmpresa, Url, Activo FROM Encuesta WHERE IdEncuesta = @Id",
               new { Id = id }).SingleOrDefault();
        }

        public IEnumerable<Encuesta> GetByIdEmpresa(string id)
        {
            return _connection.Connection.Query<Encuesta>(
               "SELECT IdEncuesta, Nombre, Descripcion, Interno, IdEmpresa, Url, Activo FROM Encuesta WHERE idEmpresa = @Id",
               new { Id = id }).ToList();
        }

        public Encuesta GetFullByIdEmpresaAndIdEncuesta(string idEmpresa, string idEncuesta)
        {
            var queryEncuesta = "SELECT IdEncuesta, Nombre, Descripcion, Interno, IdEmpresa, Activo FROM Encuesta WHERE IdEncuesta = @IdEncuesta AND IdEmpresa = @IdEmpresa;";
            var encuesta = _connection.Connection.Query<Encuesta>(queryEncuesta, new { IdEncuesta = idEncuesta, IdEmpresa = idEmpresa }).FirstOrDefault();

            if (encuesta != null)
            {
                var queryPreguntas = "SELECT p.IdPregunta, p.Descripcion, p.Requerido, p.IdEncuesta, p.IdMetadata, m.IdMetadata, m.Nombre, ip.IdItemPregunta, ip.IdPregunta, ip.Valor " +
                    "FROM Pregunta p INNER JOIN Metadata m ON m.IdMetadata = p.IdMetadata LEFT JOIN ItemPregunta ip ON ip.IdPregunta = p.IdPregunta WHERE p.IdEncuesta = @IdEncuesta;";

                var dataPregunta = new Dictionary<string, Pregunta>();
                _connection.Connection.Query<Pregunta, Metadata, ItemPregunta, Pregunta>(queryPreguntas,
                    (p, m, ip) =>
                    {
                        p.MetadataPregunta = m;

                        Pregunta pregunta;

                        if (!dataPregunta.TryGetValue(p.IdPregunta, out pregunta))
                        {
                            dataPregunta.Add(p.IdPregunta, pregunta = p);
                        }

                        if (pregunta.ItemsPreguntas == null)
                        {
                            pregunta.ItemsPreguntas = new List<ItemPregunta>();
                        }

                        pregunta.ItemsPreguntas.Add(ip);

                        return pregunta;
                    },
                    new { IdEncuesta = encuesta.IdEncuesta },
                    splitOn: "IdPregunta,IdMetadata,IdItemPregunta").AsQueryable();

                encuesta.Preguntas = dataPregunta.Values.ToList();
            }

            return encuesta;
        }

        public Encuesta Insert(Encuesta poll)
        {
            var sqlQuery = "INSERT INTO Encuesta (IdEncuesta, Nombre, Descripcion, Interno, IdEmpresa, Url, Activo) VALUES(@IdEncuesta, @Nombre, @Descripcion, @Interno, @IdEmpresa, @Url, @Activo);" +
                 "SELECT IdEncuesta FROM Encuesta WHERE IdEncuesta = @IdEncuesta;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, poll).Single();

            poll.IdEncuesta = resultId;

            return poll;
        }

        public int Remove(string id)
        {
            return _connection.Connection.Execute(
                "DELETE FROM Respuesta WHERE EXISTS (SELECT IdPregunta FROM Pregunta WHERE IdEncuesta = @Id); " +
                "DELETE FROM ItemPregunta WHERE EXISTS (SELECT IdPregunta FROM Pregunta WHERE IdEncuesta = @Id); " +
                "DELETE FROM Pregunta WHERE IdEncuesta = @Id; " +
                "DELETE FROM Encuesta WHERE IdEncuesta = @Id;", new { Id = id });
        }

        public Encuesta Update(Encuesta poll)
        {
            var sqlQuery = "UPDATE Encuesta SET Nombre = @Nombre, Descripcion = @Descripcion, Interno = @Interno, IdEmpresa = @IdEmpresa, Activo = @Activo WHERE IdEncuesta = @IdEncuesta";
            _connection.Connection.Execute(sqlQuery, poll);

            return poll;
        }

        public IEnumerable<RespuestasEncuesta> GetRespuestasEncuestaById(string idEncuesta)
        {
            return _connection.Connection.Query<RespuestasEncuesta>("GetCountRespuestasPorEncuesta", new { idEncuesta = idEncuesta },
                commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Dictionary<string, string>> GetRespuestasEncuesta(string idEncuesta)
        {
            var listResult = new List<Dictionary<string, string>>();

            IEnumerable<dynamic> results = _connection.Connection.Query<dynamic>("GetRespuestasPorEncuesta", new { idEncuesta = idEncuesta },
                commandType: CommandType.StoredProcedure);

            foreach (var row in results)
            {
                var fields = row as Dictionary<string, string>;
                listResult.Add(fields);
            }

            return listResult;
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
