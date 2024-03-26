using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Data;
using API.Models.Domain;
using API.Models.DTO;

namespace API.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ApplicationDbContext  _context;

        public CategoryManager(ApplicationDbContext context)
        {
              _context=context;
        }
        public async Task<CategoryDto> CreateCategory(CreateCategoryDto categoryModel)
        {
                //Map DTO to Domain Model
         var category=new Category
         {   Name=categoryModel.Name,
             UrlHandle=categoryModel.UrlHandle 
         };

         await _context.Categories.AddAsync(category);
         await _context.SaveChangesAsync();
         //Domain model to DTO
         var categoryDto=new CategoryDto
         {
            Id=category.Id,
            Name=category.Name,
            UrlHandle=category.UrlHandle
         };

         return categoryDto;
        }
    }
}