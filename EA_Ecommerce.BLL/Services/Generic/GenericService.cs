using Azure;
using Azure.Core;
using EA_Ecommerce.BLL.Services.Files;
using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Categories;
using Mapster;
using Microsoft.AspNetCore.Http;
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
        private readonly IFileService _fileService;

        public GenericService(IGenericRepository<TEntity> genericRepository , IFileService fileService)
        {
            _genericRepository = genericRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateAsync(TRequest request , bool WithImage = false , string? fileName = null)
        {
            var entity = request.Adapt<TEntity>();
            entity.CreatedAt = DateTime.UtcNow;
            if(WithImage)
            {
                var mainImageProp = typeof(TRequest).GetProperty("MainImage");
                var file = mainImageProp?.GetValue(request) as IFormFile;
                if (file is { Length: > 0 })
                {
                    var (url, publicId) = await _fileService.UploadAsync(file, fileName);
                    typeof(TEntity).GetProperty("MainImage")?.SetValue(entity, url);
                    typeof(TEntity).GetProperty("MainImagePublicId")?.SetValue(entity, publicId);
                }
            }
            return await _genericRepository.CreateAsync(entity);
        }
        public async Task<int> DeleteAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if (entity is null)
                return 0;

            var publicId = typeof(TEntity).GetProperty("MainImagePublicId")?.GetValue(entity) as string;
            var result = await _genericRepository.DeleteAsync(entity);
            if (result > 0 && !string.IsNullOrWhiteSpace(publicId))
            {
                try
                {
                    await _fileService.DeleteAsync(publicId);
                }
                catch (Exception ex)
                {
                    throw new Exception("❌ Failed to delete image", ex);
                }
            }
            return result;
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
