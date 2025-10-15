using Azure;
using Azure.Core;
using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Categories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Generic
{
    public class GenericService<TRequest, TResponse, TEntity> : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int Create(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return _genericRepository.Create(entity);
        }

        public int Delete(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null)
            {
                return 0;
            }
            return _genericRepository.Delete(entity);
        }

        public IEnumerable<TResponse> GetAll(bool onlyActive = false)
        {
            var entites = _genericRepository.GetAll();
            if (onlyActive)
            {
                entites = entites.Where(e => e.Status == Status.Active);
            }
            return entites.Adapt<IEnumerable<TResponse>>();
        }

        public TResponse? GetById(int id )
        {
            var entity = _genericRepository.GetById(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public bool ToggleStatus(int id)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null) return false;
            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active;
            _genericRepository.Update(entity);
            return true;
        }

        public int Update(int id, TRequest request)
        {
            var entity = _genericRepository.GetById(id);
            if (entity is null)
            {
                return 0;
            }
            var updatedEntity = request.Adapt(entity);
            return _genericRepository.Update(updatedEntity);
        }
    }
}
