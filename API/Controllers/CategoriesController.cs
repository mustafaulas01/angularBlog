using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Data;
using API.Models.Domain;
using API.Models.DTO;
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
        public async Task<IActionResult> CreateCategory(CreateCategoryDto model)
        {
         
         var categoryDto= await _categoryService.CreateCategory(model);

         return Ok(categoryDto);
        }
    }
}