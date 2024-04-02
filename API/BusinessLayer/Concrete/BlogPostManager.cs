using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Data;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace API.BusinessLayer.Concrete
{
    public class BlogPostManager : IBlogPostService
    {
        private readonly ApplicationDbContext _context;

        public BlogPostManager(ApplicationDbContext context)
        {

            _context=context;
            
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
            IsVisible=model.IsVisible

            };
            await  _context.BlogPosts.AddAsync(blog);
            await _context.SaveChangesAsync();
            
           return new BlogPostDto() {
            Id=blog.Id,
            Title=blog.Title,
            ShortDescription=blog.ShortDescription,
            UrlHandle=blog.UrlHandle,
            FeatureImageUrl=blog.FeatureImageUrl,
            Author=blog.Author,
            Content=blog.Content,
            PublishedDate=blog.PublishedDate,
            IsVisible=blog.IsVisible}; 
        }

        public async Task<List<BlogPostDto>> GetAllBlogPosts()
        {
            var list= await (from bl in _context.BlogPosts select new BlogPostDto { Id=bl.Id,Title=bl.Title,Content=bl.Content,UrlHandle=bl.UrlHandle,
            FeatureImageUrl=bl.FeatureImageUrl,Author=bl.Author,ShortDescription=bl.ShortDescription,PublishedDate=bl.PublishedDate}).ToListAsync();

           return list;
        }
    }
}