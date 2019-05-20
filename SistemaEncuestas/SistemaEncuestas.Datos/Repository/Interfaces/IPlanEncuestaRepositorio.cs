namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IPlanEncuestaRepositorio : IDisposable
    {
        PlanEncuesta Find(string id);
        IEnumerable<PlanEncuesta> GetAll();
        PlanEncuesta Insert(PlanEncuesta pollPlan);
    }
}
