namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;

    #endregion

    public interface IRespuestaRepositorio : IDisposable
    {
        Respuesta Insert(Respuesta respuesta);
    }
}
