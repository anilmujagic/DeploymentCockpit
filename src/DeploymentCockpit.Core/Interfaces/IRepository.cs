using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DeploymentCockpit.Interfaces
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        
        T GetByKey(params object[] keyValues);

        #region GetCount

        int GetCount();
        int GetCount(Expression<Func<T, bool>> whereCondition);

        #endregion

        #region GetAll

        IEnumerable<T> GetAll();

        // Include
        IEnumerable<T> GetAll<TInclude1>(
            Expression<Func<T, TInclude1>> includeProperty1);
        IEnumerable<T> GetAll<TInclude1, TInclude2>(
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2);
        IEnumerable<T> GetAll<TInclude1, TInclude2, TInclude3>(
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2,
            Expression<Func<T, TInclude3>> includeProperty3);

        // Where, Include
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> whereCondition);
        IEnumerable<T> GetAll<TInclude1>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1);
        IEnumerable<T> GetAll<TInclude1, TInclude2>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2);
        IEnumerable<T> GetAll<TInclude1, TInclude2, TInclude3>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2,
            Expression<Func<T, TInclude3>> includeProperty3);

        #endregion

        #region GetAllAs

        IEnumerable<TDto> GetAllAs<TDto>();

        // Include
        IEnumerable<TDto> GetAllAs<TDto, TInclude1>(
            Expression<Func<T, TInclude1>> includeProperty1);
        IEnumerable<TDto> GetAllAs<TDto, TInclude1, TInclude2>(
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2);
        IEnumerable<TDto> GetAllAs<TDto, TInclude1, TInclude2, TInclude3>(
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2,
            Expression<Func<T, TInclude3>> includeProperty3);
        IEnumerable<TDto> GetAllAs<TDto, TInclude1, TInclude2, TInclude3>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2,
            Expression<Func<T, TInclude3>> includeProperty3);

        // Where, Include
        IEnumerable<TDto> GetAllAs<TDto>(
            Expression<Func<T, bool>> whereCondition);
        IEnumerable<TDto> GetAllAs<TDto, TInclude1>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1);
        IEnumerable<TDto> GetAllAs<TDto, TInclude1, TInclude2>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2);

        #endregion
    }
}
