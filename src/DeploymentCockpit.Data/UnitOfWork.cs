using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Data.Repositories;
using DeploymentCockpit.Interfaces;
using DeploymentCockpit.Models;

namespace DeploymentCockpit.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DeploymentCockpitEntities _db;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(DeploymentCockpitEntities db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            _db = db;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public IRepository<T> Repository<T>()
            where T : class
        {
            var key = typeof(T);
            if (!_repositories.ContainsKey(key))
                _repositories.Add(key, new Repository<T>(_db));

            return _repositories[key] as Repository<T>;
        }

        private IDashboardRepository _dashboardRepository;
        public IDashboardRepository DashboardRepository
        {
            get
            {
                if (_dashboardRepository == null)
                    _dashboardRepository = new DashboardRepository(_db);
                return _dashboardRepository;
            }
        }
    }
}
