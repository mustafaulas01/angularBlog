using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.BusinessLayer.Concrete
{
    public class BlogPostManager : IBlogPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryService _categoryService;

        public BlogPostManager(ApplicationDbContext context,ICategoryService categoryService)
        {

            _context=context;
            _categoryService=categoryService;
            
        }
        public async Task<BlogPostDto> CreateBlogPostAsync(CreateBlogDto model)
        { 
            var blog=new BlogPost () 
            {
                
            Title=model.Title,
            ShortDescription=model.ShortDescription,
            UrlHandle=model.UrlHandle,
            Content=model.Content,
            FeatureImageUrl=model.FeatureImageUrl,
            Author=model.Author,
            PublishedDate=model.PublishedDate ,
            IsVisible=model.IsVisible,
            Categories=new List<Category>()

            };
        
        await  _context.BlogPosts.AddAsync(blog);
        await _context.SaveChangesAsync();

        foreach(var categoryGuid in model.Categories)
        {
            var existingCategroy=await _categoryService.GetByCategoryIdAsync(categoryGuid);
            if(existingCategroy!=null)
            {
                blog.Categories.Add(new Category() {Id=existingCategroy.Id,Name=existingCategroy.Name,UrlHandle=existingCategroy.UrlHandle});
                await _context.SaveChangesAsync();
            }
        }
            

  
            
           return new BlogPostDto() {
            Id=blog.Id,
            Title=blog.Title,
            ShortDescription=blog.ShortDescription,
            UrlHandle=blog.UrlHandle,
            FeatureImageUrl=blog.FeatureImageUrl,
            Author=blog.Author,
            Content=blog.Content,
            PublishedDate=blog.PublishedDate,
            IsVisible=blog.IsVisible,
            Categories=blog.Categories.Select(a=>new CategoryDto{Id=a.Id,Name=a.Name,UrlHandle=a.UrlHandle}).ToList()
            
            }; 
        }

        public async Task<List<BlogPostDto>> GetAllBlogPosts()
        {
            var list= await (from bl in _context.BlogPosts select new BlogPostDto { Id=bl.Id,Title=bl.Title,Content=bl.Content,UrlHandle=bl.UrlHandle,
            FeatureImageUrl=bl.FeatureImageUrl,Author=bl.Author,ShortDescription=bl.ShortDescription,
            PublishedDate=bl.PublishedDate,Categories=bl.Categories.Select(a=>new CategoryDto{Id=a.Id,Name=a.Name,UrlHandle=a.UrlHandle}).ToList()  }).ToListAsync();
           
            
           

           return list;
        }


    //  public Guid Id { get; set; }
    // public string Title { get; set; }
    // public string ShortDescription { get; set; }
    // public string Content { get; set; }
    // public string  FeatureImageUrl { get; set; }
    // public string UrlHandle { get; set; }
    // public DateTime PublishedDate { get; set; }
    // public string Author { get; set; }

    // public bool IsVisible { get; set; }

    // public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();


        public async Task<BlogPostDto> GetPostByIdAsync(Guid id)
        {
            var blog = await _context.BlogPosts.FirstOrDefaultAsync(a => a.Id == id);

            if (blog != null)
            {
                var blogDto = new BlogPostDto()
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    ShortDescription = blog.ShortDescription,
                    Content = blog.Content,
                    FeatureImageUrl = blog.FeatureImageUrl,
                    UrlHandle = blog.UrlHandle,
                    PublishedDate = blog.PublishedDate,
                    Author = blog.Author

                };

                foreach (var cat in blog.Categories)
                {
                    blogDto.Categories.Add(new CategoryDto() { Id = cat.Id, Name = cat.Name, UrlHandle = cat.UrlHandle });
                }
              return blogDto;
            }

            else 
            return null;

        }
    }
}