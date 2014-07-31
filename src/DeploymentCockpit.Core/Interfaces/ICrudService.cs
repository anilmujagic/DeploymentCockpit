using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeploymentCockpit.Interfaces
{
    public interface ICrudService<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetByKey(params object[] keyValues);
        IEnumerable<T> GetAll();
        IEnumerable<TDto> GetAllAs<TDto>();
    }
}
