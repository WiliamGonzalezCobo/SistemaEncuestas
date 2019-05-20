namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IAuditoriaIngresoRepositorio : IDisposable
    {
        IEnumerable<AuditoriaIngreso> GetAll();
        IEnumerable<AuditoriaIngreso> GetFullById(string id);
        AuditoriaIngreso Insert(AuditoriaIngreso auditIncome);
    }
}
