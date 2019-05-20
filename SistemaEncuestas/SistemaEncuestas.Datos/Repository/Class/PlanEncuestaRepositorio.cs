namespace SistemaEncuestas.Datos.Repository.Class
{
    #region Namespaces

    using Dapper;
    using Entidades.Entities;
    using Infrastructure.Interfaces;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    internal class PlanEncuestaRepositorio : IPlanEncuestaRepositorio
    {
        #region Attributes

        private bool _disposed = false;
        private readonly IConnectionFactory _connection;

        #endregion

        #region Constructors and Destructors

        public PlanEncuestaRepositorio(IConnectionFactory connection)
        {
            _connection = connection;
        }

        ~PlanEncuestaRepositorio()
        {
            Dispose(false);
        }

        #endregion

        #region Public Methods

        public PlanEncuesta Find(string id)
        {
            return _connection.Connection.Query<PlanEncuesta>(
               "SELECT IdPlan, Nombre, Descripcion, Precio, Promocion, Activo FROM PlanEncuesta WHERE IdPlan = @Id",
               new { Id = id }).SingleOrDefault();
        }

        public IEnumerable<PlanEncuesta> GetAll()
        {
            var dataPlanesItemPlanes = new Dictionary<string, PlanEncuesta>();

            _connection.Connection.Query<PlanEncuesta, ItemPlanEncuesta, PlanEncuesta>(
               "SELECT p.IdPlan, p.Nombre, p.Descripcion, p.Precio, p.Promocion, p.Activo, i.ItemPlan, i.IdPlan, " +
               "i.Descripcion, i.Activo FROM PlanEncuesta p LEFT JOIN ItemPlanEncuesta i ON i.IdPlan = p.IdPlan",
               (p, i) =>
               {
                   PlanEncuesta plan;

                   if (!dataPlanesItemPlanes.TryGetValue(p.IdPlan, out plan))
                   {
                       dataPlanesItemPlanes.Add(p.IdPlan, plan = p);
                   }

                   if (plan.ItemPlanes == null)
                   {
                       plan.ItemPlanes = new List<ItemPlanEncuesta>();
                   }

                   plan.ItemPlanes.Add(i);

                   return plan;
               }, splitOn: "IdPlan").AsQueryable();

            return dataPlanesItemPlanes.Values.ToList();
        }

        public PlanEncuesta Insert(PlanEncuesta pollPlan)
        {
            var sqlQuery = "INSERT INTO PlanEncuesta (IdPlan, Nombre, Descripcion, Precio, Promocion, Activo) " +
                "VALUES(@IdPlan, @Nombre, @Descripcion, @Precio, @Promocion, @Activo); " +
                "SELECT IdPlan FROM PlanEncuesta WHERE IdPlan = @IdPlan;";

            var resultId = _connection.Connection.Query<string>(sqlQuery, pollPlan).Single();

            pollPlan.IdPlan = resultId;

            return pollPlan;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection.Connection.Close();
                }

                _disposed = true;
            }
        }

        #endregion
    }
}
