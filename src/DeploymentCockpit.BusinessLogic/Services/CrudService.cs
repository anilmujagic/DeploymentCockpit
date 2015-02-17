using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;

namespace DeploymentCockpit.Services
{
    public class CrudService<T> : DataService, ICrudService<T>
        where T : class
    {
        public CrudService(IUnitOfWorkFactory unitOfWorkFactory)
            : base(unitOfWorkFactory)
        {
        }

        public virtual void Insert(T entity)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                uow.Repository<T>().Insert(entity);
                uow.Commit();
            }
        }

        public virtual void Update(T entity)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                uow.Repository<T>().Update(entity);
                uow.Commit();
            }
        }

        public virtual void Delete(T entity)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                uow.Repository<T>().Delete(entity);
                uow.Commit();
            }
        }

        public virtual T GetByKey(params object[] keyValues)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<T>().GetByKey(keyValues);
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                return uow.Repository<T>().GetAll();
            }
        }
    }
}
