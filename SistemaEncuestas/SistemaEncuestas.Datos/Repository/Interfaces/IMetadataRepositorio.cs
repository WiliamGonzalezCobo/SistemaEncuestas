namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IMetadataRepositorio : IDisposable
    {
        Metadata Find(string id);
        IEnumerable<Metadata> GetAll();
        Metadata Insert(Metadata metadata);
    }
}
