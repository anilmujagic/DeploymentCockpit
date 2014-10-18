using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DeploymentCockpit.Interfaces;
using AutoMapper.QueryableExtensions;

namespace DeploymentCockpit.Data.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        public Repository(DeploymentCockpitEntities db)
        {
            if (db == null)
                throw new ArgumentNullException("db");

            _db = db;
        }

        protected readonly DeploymentCockpitEntities _db;

        public void Insert(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _db.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            if (_db.Entry<T>(entity).State == EntityState.Detached)
                _db.Entry<T>(entity).State = EntityState.Unchanged;
            _db.Set<T>().Remove(entity);
        }

        public T GetByKey(params object[] keyValues)
        {
            return _db.Set<T>().Find(keyValues);
        }

        public int GetCount()
        {
            return _db.Set<T>().Count();
        }

        public int GetCount(Expression<Func<T, bool>> whereCondition)
        {
            return _db.Set<T>().Count(whereCondition);
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }

        #region GetAll overloads

        // Include

        public IEnumerable<T> GetAll<TInclude1>(
            Expression<Func<T, TInclude1>> includeProperty1)
        {
            return this.GetAll<TInclude1, object>(includeProperty1, null);
        }

        public IEnumerable<T> GetAll<TInclude1, TInclude2>(
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2)
        {
            return this.GetAll<TInclude1, TInclude2, object>(includeProperty1, includeProperty2, null);
        }

        public IEnumerable<T> GetAll<TInclude1, TInclude2, TInclude3>(
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2,
            Expression<Func<T, TInclude3>> includeProperty3)
        {
            return this.GetAll(null, includeProperty1, includeProperty2, includeProperty3);
        }

        // Where, Include

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>> whereCondition)
        {
            return this.GetAll<object>(whereCondition, null);
        }

        public IEnumerable<T> GetAll<TInclude1>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1)
        {
            return this.GetAll<TInclude1, object>(whereCondition, includeProperty1, null);
        }

        public IEnumerable<T> GetAll<TInclude1, TInclude2>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2)
        {
            return this.GetAll<TInclude1, TInclude2, object>(whereCondition, includeProperty1, includeProperty2, null);
        }

        public IEnumerable<T> GetAll<TInclude1, TInclude2, TInclude3>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2,
            Expression<Func<T, TInclude3>> includeProperty3)
        {
            return this.GetQuery<TInclude1, TInclude2, TInclude3>(
                    whereCondition, includeProperty1, includeProperty2, includeProperty3)
                .ToList();
        }

        #endregion

        public IEnumerable<TDto> GetAllAs<TDto>()
        {
            return _db.Set<T>().Project().To<TDto>().ToList();
        }

        #region GetAllAs overloads

        // Include

        public IEnumerable<TDto> GetAllAs<TDto, TInclude1>(
            Expression<Func<T, TInclude1>> includeProperty1)
        {
            return this.GetAllAs<TDto, TInclude1, object>(includeProperty1, null);
        }

        public IEnumerable<TDto> GetAllAs<TDto, TInclude1, TInclude2>(
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2)
        {
            return this.GetAllAs<TDto, TInclude1, TInclude2, object>(includeProperty1, includeProperty2, null);
        }

        public IEnumerable<TDto> GetAllAs<TDto, TInclude1, TInclude2, TInclude3>(
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2,
            Expression<Func<T, TInclude3>> includeProperty3)
        {
            return this.GetAllAs<TDto, TInclude1, TInclude2, TInclude3>(
                null, includeProperty1, includeProperty2, includeProperty3);
        }

        // Where, Include

        public IEnumerable<TDto> GetAllAs<TDto>(
            Expression<Func<T, bool>> whereCondition)
        {
            return this.GetAllAs<TDto, object>(whereCondition, null);
        }

        public IEnumerable<TDto> GetAllAs<TDto, TInclude1>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1)
        {
            return this.GetAllAs<TDto, TInclude1, object>(whereCondition, includeProperty1, null);
        }

        public IEnumerable<TDto> GetAllAs<TDto, TInclude1, TInclude2>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2)
        {
            return this.GetAllAs<TDto, TInclude1, TInclude2, object>(
                whereCondition, includeProperty1, includeProperty2, null);
        }

        public IEnumerable<TDto> GetAllAs<TDto, TInclude1, TInclude2, TInclude3>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2,
            Expression<Func<T, TInclude3>> includeProperty3)
        {
            return this.GetQuery<TInclude1, TInclude2, TInclude3>(
                    whereCondition, includeProperty1, includeProperty2, includeProperty3)
                .Project().To<TDto>()
                .ToList();
        }

        #endregion

        private IQueryable<T> GetQuery<TInclude1, TInclude2, TInclude3>(
            Expression<Func<T, bool>> whereCondition,
            Expression<Func<T, TInclude1>> includeProperty1,
            Expression<Func<T, TInclude2>> includeProperty2,
            Expression<Func<T, TInclude3>> includeProperty3)
        {
            var query = _db.Set<T>().AsQueryable();

            if (includeProperty1 != null)
                query = query.Include(includeProperty1);
            if (includeProperty2 != null)
                query = query.Include(includeProperty2);
            if (includeProperty3 != null)
                query = query.Include(includeProperty3);

            if (whereCondition != null)
                query = query.Where(whereCondition);

            return query;
        }
    }
}
