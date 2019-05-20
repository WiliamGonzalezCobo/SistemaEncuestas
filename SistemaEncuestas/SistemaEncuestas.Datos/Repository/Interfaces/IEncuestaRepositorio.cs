namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IEncuestaRepositorio : IDisposable
    {
        Encuesta Find(string id);
        IEnumerable<Encuesta> GetByIdEmpresa(string id);
        Encuesta GetFullByIdEmpresaAndIdEncuesta(string idEmpresa, string idEncuesta);
        Encuesta Insert(Encuesta poll);
        int Remove(string id);
        Encuesta Update(Encuesta poll);
        IEnumerable<Dictionary<string, string>> GetRespuestasEncuesta(string idEncuesta);
        IEnumerable<RespuestasEncuesta> GetRespuestasEncuestaById(string idEncuesta);
    }
}
