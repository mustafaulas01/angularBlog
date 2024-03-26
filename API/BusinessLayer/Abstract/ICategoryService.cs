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
    }
}