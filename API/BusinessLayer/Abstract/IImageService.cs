using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Domain;

namespace API.BusinessLayer.Abstract
{
    public interface IImageService
    {
        
        Task<BlogImage> UploadImage(IFormFile file,BlogImage model);

        Task<List<BlogImage>> GetAllImagesAsync();
    }
}