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

        protected virtual IEnumerable<TDto> GetAll()
        {
            return _service.GetAll()
                .Select(e => this.EntityToDto(this.ModifyEntityForResponse(e)))
                .ToList();
        }

        public virtual TDto Get(TKey id)
        {
            return this.EntityToDto(this.ModifyEntityForResponse(_service.GetByKey(id)));
        }

        public virtual HttpResponseMessage Post(TDto dto)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, this.GetValidationErrorMessages());

            var entity = this.ModifyEntityForInsert(this.DtoToEntity(dto));
            _service.Insert(entity);

            var dtoToReturn = this.EntityToDto(this.ModifyEntityForResponse(entity));
            
            var response = Request.CreateResponse<TDto>(HttpStatusCode.Created, dtoToReturn);
            response.Headers.Location = new Uri(Request.RequestUri + "/" + this.GetID(entity).ToString());
            return response;
        }

        public virtual HttpResponseMessage Put(TKey id, TDto dto)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, this.GetValidationErrorMessages());

            var entity = this.ModifyEntityForUpdate(this.DtoToEntity(dto));

            if (!this.GetID(entity).Equals(id))
                return Request.CreateResponse((HttpStatusCode)422,  // 422: Unprocessable Entity
                    new[] { "ID property of submitted entity must match the ID parameter in URL." });

            _service.Update(entity);

            var dtoToReturn = this.EntityToDto(this.ModifyEntityForResponse(entity));

            return Request.CreateResponse<TDto>(HttpStatusCode.OK, dtoToReturn);
        }

        public virtual HttpResponseMessage Delete(TKey id)
        {
            var entity = new TEntity();
            this.SetID(entity, id);
            _service.Delete(entity);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        protected abstract TKey GetID(TEntity entity);
        protected abstract void SetID(TEntity entity, TKey id);

        protected virtual TEntity ModifyEntityForResponse(TEntity entity)
        {
            return entity;
        }
        protected virtual TEntity ModifyEntityForInsert(TEntity entity)
        {
            return entity;
        }
        protected virtual TEntity ModifyEntityForUpdate(TEntity entity)
        {
            return entity;
        }

        protected virtual TDto EntityToDto(TEntity entity)
        {
            return Mapper.Map<TDto>(entity);
        }
        protected virtual TEntity DtoToEntity(TDto dto)
        {
            return Mapper.Map<TEntity>(dto);
        }
    }
}