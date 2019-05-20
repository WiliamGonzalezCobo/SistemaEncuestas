namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IPagoPlanRepositorio : IDisposable
    {
        PagoPlan Find(string id);
        IEnumerable<PagoPlan> GetAll();
        PagoPlan Insert(PagoPlan planPay);
    }
}
