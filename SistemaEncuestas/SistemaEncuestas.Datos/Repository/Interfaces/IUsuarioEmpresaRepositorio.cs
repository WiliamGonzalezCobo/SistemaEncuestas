namespace SistemaEncuestas.Datos.Repository.Interfaces
{
    #region Namespaces

    using Entidades.Entities;
    using System;
    using System.Collections.Generic;

    #endregion

    public interface IUsuarioEmpresaRepositorio : IDisposable
    {
        UsuarioEmpresa Find(string id);
        IEnumerable<UsuarioEmpresa> GetAll();
        IEnumerable<UsuarioEmpresa> GetFullById(string id, string password);
        UsuarioEmpresa Insert(UsuarioEmpresa companyRol);
    }
}
