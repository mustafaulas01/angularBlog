using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTO;

namespace API.BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        Task<CategoryDto>CreateCategory(CreateCategoryDto categoryModel);
        Task<List<CategoryDto>> GetCategoriesAsync();

        Task<CategoryDto?> GetByCategoryIdAsync(Guid id);

        Task<CategoryDto?> UpdateCategoryAsync(Guid id,CategoryUpdateDto categoryModel);

        Task DeleteCategoryAsync(Guid id);
    }
}