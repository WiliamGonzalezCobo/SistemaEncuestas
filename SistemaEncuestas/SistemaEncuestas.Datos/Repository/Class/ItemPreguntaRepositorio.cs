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

    internal class ItemPreguntaRepositorio : IItemPreguntaRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public ItemPreguntaRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~ItemPreguntaRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public ItemPregunta Find(string id)
        {
            return _connection.Connection.Query<ItemPregunta>(
               "SELECT IdItemPregunta FROM ItemPregunta WHERE IdItemPregunta = @Id",
               new { Id = id }).SingleOrDefault();
        }

        public ItemPregunta Insert(ItemPregunta questionItem)
        {
            var sqlQuery = "INSERT INTO ItemPregunta (IdItemPregunta, IdPregunta, Valor) VALUES(@IdItemPregunta, @IdPregunta, @Valor);" +
                 "SELECT IdItemPregunta FROM ItemPregunta WHERE IdItemPregunta = @IdItemPregunta;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, questionItem).Single();

            questionItem.IdItemPregunta = resultId;

            return questionItem;
        }

        public void Remove(string id)
        {
            _connection.Connection.Execute("DELETE FROM ItemPregunta WHERE IdItemPregunta = @Id", new { Id = id });
        }

        public ItemPregunta Update(ItemPregunta questionItem)
        {
            var sqlQuery = "UPDATE ItemPregunta SET IdPregunta = @IdPregunta, Valor = @Valor WHERE IdItemPregunta = @IdItemPregunta";
            _connection.Connection.Execute(sqlQuery, questionItem);

            return questionItem;
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
