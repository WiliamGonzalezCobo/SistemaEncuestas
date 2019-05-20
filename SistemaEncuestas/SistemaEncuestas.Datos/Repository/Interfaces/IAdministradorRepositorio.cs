namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IAdministradorRepositorio : IDisposable
    {
        IEnumerable<Administrador> GetAll();
        IEnumerable<Administrador> GetFullById(string id, string password);
    }
}
