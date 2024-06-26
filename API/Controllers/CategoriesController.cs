using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
  
    private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
          
            _categoryService=categoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateCategory([FromBody]CreateCategoryDto model)
        {
         
         var categoryDto= await _categoryService.CreateCategory(model);

         return Ok(categoryDto);
        }

        [HttpGet] 
        public async Task<IActionResult> GetAllCategories()
        {
            var categoryList=await _categoryService.GetCategoriesAsync();

            return Ok(categoryList);

        }
        
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult>GetCategoryById( [FromRoute] Guid id)
        {
            var category= await _categoryService.GetByCategoryIdAsync(id);

            if(category!=null)
            return Ok(category);
            
            return NotFound();

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult>UpdateCategory([FromRoute] Guid id,CategoryUpdateDto categoryModel)
        {
            var categoryDto= await _categoryService.UpdateCategoryAsync(id,categoryModel);

             return Ok(categoryDto);
           

        }

       [HttpDelete]
       [Route("{id:Guid}")]
       [Authorize(Roles = "Writer")]
        public async Task<IActionResult>DeleteCategory([FromRoute] Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
        
    }
}