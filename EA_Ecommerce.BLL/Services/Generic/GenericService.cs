/*using Azure;
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
*/
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
    public class GenericService<TRequest, TResponse, TEntity>
        : IGenericService<TRequest, TResponse, TEntity> where TEntity : BaseModel
    {
        private readonly IGenericRepository<TEntity> _genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<int> CreateAsync(TRequest request)
        {
            var entity = request.Adapt<TEntity>();
            return await _genericRepository.CreateAsync(entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity is null)
                return 0;

            return await _genericRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<TResponse>> GetAllAsync(bool onlyActive = false)
        {
            var entities = await _genericRepository.GetAllAsync();
            if (onlyActive)
                entities = entities.Where(e => e.Status == Status.Active);

            return entities.Adapt<IEnumerable<TResponse>>();
        }

        public async Task<TResponse?> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity is null)
                return false;

            entity.Status = entity.Status == Status.Active ? Status.Inactive : Status.Active;
            await _genericRepository.UpdateAsync(entity);
            return true;
        }

        public async Task<int> UpdateAsync(int id, TRequest request)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity is null)
                return 0;

            var updatedEntity = request.Adapt(entity);
            return await _genericRepository.UpdateAsync(updatedEntity);
        }
    }
}
