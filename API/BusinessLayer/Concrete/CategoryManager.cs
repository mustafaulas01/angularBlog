using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteCategoryAsync(Guid id)
        {
             var category = await _context.Categories.FirstOrDefaultAsync(a=>a.Id==id);
             if(category!=null)
             {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
             }
            
        }

        public async Task<CategoryDto?> GetByCategoryIdAsync(Guid id)
        {
            var category=await _context.Categories.AsNoTracking().FirstOrDefaultAsync(a=>a.Id==id);

            if(category!=null)
            return  new CategoryDto() {Id=category.Id,Name=category.Name,UrlHandle=category.UrlHandle};
            else 
            return null;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categoriesDtoList= from cat in _context.Categories
            select new CategoryDto {Id=cat.Id,Name=cat.Name,UrlHandle=cat.UrlHandle};
            var categoryAsyncList=await categoriesDtoList.ToListAsync();

            return categoryAsyncList;
        }

        public async Task<CategoryDto?> UpdateCategoryAsync(Guid id, CategoryUpdateDto categoryModel)
        {
            var category= await _context.Categories.FirstOrDefaultAsync(a=>a.Id==id);
            if(category!=null)
            {
                category.Name=categoryModel.Name;
                category.UrlHandle=categoryModel.UrlHandle;

                await _context.SaveChangesAsync();

                return new CategoryDto () {Id=category.Id,Name=categoryModel.Name,UrlHandle=category.UrlHandle};
            }

            return null;
    
        }
    }
}