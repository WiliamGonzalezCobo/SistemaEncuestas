namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IEmpresaRepositorio : IDisposable
    {
        Empresa Find(string id);
        IEnumerable<Empresa> GetAll();
        Empresa Insert(Empresa company);
        Empresa Update(Empresa company);
    }
}
