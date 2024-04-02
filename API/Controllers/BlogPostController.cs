using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogPostController : ControllerBase
    {
       private readonly IBlogPostService _blogService;
        public BlogPostController(IBlogPostService blogService)
        {
           _blogService=blogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDto model)
        {
            var blog= await _blogService.CreateBlogPostAsync(model);
            
            if(blog!=null)
            return Ok(blog);

            return NotFound();
        }
        
        [HttpGet]
        public async Task<IActionResult>GetAllBlogPosts() 
        {
            var blogList=await _blogService.GetAllBlogPosts();
            
            return Ok(blogList);

        }
    }
}