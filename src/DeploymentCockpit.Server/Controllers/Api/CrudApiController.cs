using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using DeploymentCockpit.Interfaces;

namespace DeploymentCockpit.Server.Controllers.Api 
{
    public abstract class CrudApiController<TDto, TEntity, TKey, TCrudService> : ApiController
        where TDto : class, new()
        where TEntity : class, new()
        where TCrudService : ICrudService<TEntity>
    {
        protected readonly TCrudService _service;

        public CrudApiController(TCrudService service)
        {
            if (service == null)
                throw new ArgumentNullException("service");
            _service = service;
        }

        public virtual HttpResponseMessage Post(TDto dto)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, this.GetValidationErrorMessages());

            var entity = this.DtoToEntity(dto);

            this.OnBeforeInsert(entity, dto);
            _service.Insert(entity);
            this.OnAfterInsert(entity, dto);

            var dtoToReturn = this.EntityToDto(this.ModifyEntityForResponse(entity));
            
            var response = Request.CreateResponse<TDto>(HttpStatusCode.Created, dtoToReturn);
            response.Headers.Location = new Uri(Request.RequestUri + "/" + this.GetID(entity).ToString());
            return response;
        }

        public virtual HttpResponseMessage Put(TKey id, TDto dto)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, this.GetValidationErrorMessages());

            var entity = this.DtoToEntity(dto);

            if (!this.GetID(entity).Equals(id))
                return Request.CreateResponse((HttpStatusCode)422,  // 422: Unprocessable Entity
                    new[] { "ID property of submitted entity must match the ID parameter in URL." });

            this.OnBeforeUpdate(entity, dto);
            _service.Update(entity);
            this.OnAfterUpdate(entity, dto);

            var dtoToReturn = this.EntityToDto(this.ModifyEntityForResponse(entity));

            return Request.CreateResponse<TDto>(HttpStatusCode.OK, dtoToReturn);
        }

        public virtual HttpResponseMessage Delete(TKey id)
        {
            var entity = new TEntity();
            this.SetID(entity, id);

            this.OnBeforeDelete(id);
            _service.Delete(entity);
            this.OnAfterDelete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public TDto Get(TKey id)
        {
            return this.EntityToDto(this.ModifyEntityForResponse(this.GetByKey(id)));
        }

        protected IEnumerable<TDto> GetAll()
        {
            return _service.GetAll()
                .Select(e => this.EntityToDto(this.ModifyEntityForResponse(e)))
                .ToList();
        }

        protected virtual TEntity GetByKey(TKey id)
        {
            return _service.GetByKey(id);
        }

        protected virtual TEntity ModifyEntityForResponse(TEntity entity)
        {
            return entity;
        }

        protected virtual void OnBeforeInsert(TEntity entity, TDto dto)
        {
        }
        protected virtual void OnAfterInsert(TEntity entity, TDto dto)
        {
        }

        protected virtual void OnBeforeUpdate(TEntity entity, TDto dto)
        {
        }
        protected virtual void OnAfterUpdate(TEntity entity, TDto dto)
        {
        }

        protected virtual void OnBeforeDelete(TKey id)
        {
        }
        protected virtual void OnAfterDelete(TKey id)
        {
        }

        protected abstract TKey GetID(TEntity entity);
        protected abstract void SetID(TEntity entity, TKey id);

        private TDto EntityToDto(TEntity entity)
        {
            return Mapper.Map<TDto>(entity);
        }
        private TEntity DtoToEntity(TDto dto)
        {
            return Mapper.Map<TEntity>(dto);
        }
    }
}