using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task DeleteBlogPost(Guid id)
        {
             var blog = await _context.BlogPosts.FirstOrDefaultAsync(a => a.Id == id);
             if(blog!=null)
             {
             var blogCount=blog.Categories.Count;

             blog.Categories.RemoveRange(0,blogCount);
             await _context.SaveChangesAsync();

            
              await Task.Run(()=>{  _context.BlogPosts.Remove(blog);} );
              await _context.SaveChangesAsync();
             }

    

        }

        public async Task<List<BlogPostDto>> GetAllBlogPosts()
        {
            var list= await (from bl in _context.BlogPosts select new BlogPostDto { Id=bl.Id,Title=bl.Title,Content=bl.Content,UrlHandle=bl.UrlHandle,
            FeatureImageUrl=bl.FeatureImageUrl,Author=bl.Author,ShortDescription=bl.ShortDescription,
            IsVisible=bl.IsVisible,
            PublishedDate=bl.PublishedDate,Categories=bl.Categories.Select(a=>new CategoryDto{Id=a.Id,Name=a.Name,UrlHandle=a.UrlHandle}).ToList()  }).ToListAsync();
           
            
           

           return list;
        }


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
                    Author = blog.Author,
                    IsVisible=blog.IsVisible

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

        public async Task<BlogPostDto> GetPostByUrlAsync(string urlHandle)
        {
           
              var blog = await _context.BlogPosts.FirstOrDefaultAsync(a => a.UrlHandle == urlHandle);

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
                    Author = blog.Author,
                    IsVisible=blog.IsVisible

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

        public async Task<BlogPostDto> UpdateBlogPost(Guid id, UpdateBlogPostDto model)
        {
            var blog = await _context.BlogPosts.FirstOrDefaultAsync(a => a.Id == id);

            if (blog != null)
            {
                blog.Title = model.Title;
                blog.Author = model.Author;
                blog.ShortDescription = model.ShortDescription;
                blog.Content = model.Content;
                blog.UrlHandle = model.UrlHandle;
                blog.FeatureImageUrl = model.FeatureImageUrl;
                blog.IsVisible = model.IsVisible;
                blog.PublishedDate = model.PublishedDate;

                await _context.SaveChangesAsync();
                

                blog.Categories.RemoveRange(0,blog.Categories.Count);

                 await _context.SaveChangesAsync();
                          

                foreach (var cat in model.Categories)
                {
                    var categor = await _context.Categories.FirstOrDefaultAsync(a => a.Id == cat);
                    if (categor != null)
                    {
                     
                            blog.Categories.Add(categor);
                            await _context.SaveChangesAsync();
                        
                    }

                }

                var blogDto = new BlogPostDto()
                {
                    Id = blog.Id,
                    Title=blog.Title,
                    Author = blog.Author,
                    ShortDescription = blog.ShortDescription,
                    Content = blog.Content,
                    UrlHandle = blog.UrlHandle,
                    FeatureImageUrl = blog.FeatureImageUrl,
                    IsVisible = blog.IsVisible,
                    PublishedDate = blog.PublishedDate,
                    Categories = blog.Categories.Select(a => new CategoryDto { Id = a.Id, Name = a.Name, UrlHandle = a.UrlHandle }).ToList()

                };

                return blogDto;

            }

              else return null;
        }
    }
}