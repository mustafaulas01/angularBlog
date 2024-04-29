using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Writer")]
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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
            var blogpost= await _blogService.GetPostByIdAsync(id);

            if(blogpost!=null)
            return Ok(blogpost);

            else 
            return NoContent();

        }

        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult>GetBlogPostByUrlHandle([FromRoute] string urlHandle)
        {
                var blogpost= await _blogService.GetPostByUrlAsync(urlHandle);

            if(blogpost!=null)
            return Ok(blogpost);

            else 
            return null;
            
        } 



       [HttpPut]
       [Route("{id:Guid}")]
       [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateBlogPost([FromRoute] Guid id,[FromBody] UpdateBlogPostDto model)
        {
            var blogpost=await _blogService.UpdateBlogPost(id,model);

            if(blogpost!=null)
            return Ok(blogpost);

            else return NotFound();


        }


        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
            await _blogService.DeleteBlogPost(id);
            return Ok();
        }
    }
}