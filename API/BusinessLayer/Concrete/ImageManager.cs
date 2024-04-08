using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Data;
using API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.BusinessLayer.Concrete
{
    public class ImageManager : IImageService
    {

        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
       private IWebHostEnvironment _env;

        public ImageManager(ApplicationDbContext context,IWebHostEnvironment env,IHttpContextAccessor httpContextAccessor)
        {
             _env=env;
            _context=context;
            _httpContext=httpContextAccessor;
        }

        public async Task<List<BlogImage>> GetAllImagesAsync()
        {
           return   await _context.BlogImages.ToListAsync();
            
        }

        public async Task<BlogImage> UploadImage(IFormFile file, BlogImage model)
        {
            //1-upload the Image to API/Images
            var localPath=Path.Combine(_env.ContentRootPath,"Images",$"{model.FileName}{model.FileExtension} ");

            using var stream =new FileStream(localPath,FileMode.Create);
            await file.CopyToAsync(stream);
            //2-Update database
            var request=_httpContext.HttpContext.Request;

            var urlPath= $"{request.Scheme}://{request.Host}{request.PathBase}/Images/{model.FileName}{model.FileExtension}";
            
            model.Url=urlPath;

            await _context.BlogImages.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;

        }
    }
}