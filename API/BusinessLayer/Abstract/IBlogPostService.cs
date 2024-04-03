using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.DTO;

namespace API.BusinessLayer.Abstract
{
    public interface IBlogPostService
    {
        
    
      Task<BlogPostDto> CreateBlogPostAsync(CreateBlogDto model);

      Task<List<BlogPostDto>> GetAllBlogPosts();

      Task<BlogPostDto>GetPostByIdAsync(Guid id);
    

    }
}