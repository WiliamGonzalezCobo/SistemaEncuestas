namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;

    #endregion

    public interface IItemPreguntaRepositorio : IDisposable
    {
        ItemPregunta Find(string id);
        ItemPregunta Insert(ItemPregunta questionItem);
        void Remove(string id);
        ItemPregunta Update(ItemPregunta questionItem);
    }
}
