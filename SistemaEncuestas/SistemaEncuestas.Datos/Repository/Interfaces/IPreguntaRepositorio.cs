namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;

    #endregion

    public interface IPreguntaRepositorio : IDisposable
    {
        Pregunta Find(string id);
        Pregunta Insert(Pregunta question);
        void Remove(string id);
        Pregunta Update(Pregunta question);
    }
}
