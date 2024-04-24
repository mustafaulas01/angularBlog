using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BusinessLayer.Abstract;
using API.Models.Domain;
using API.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {

        private readonly IImageService _imageService;

        
        public ImagesController(IImageService service)
        {
            _imageService=service;
            
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
    
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, [FromForm] string fileName, [FromForm] string title)
        {
            ValidateFileUpload(file);

            if (ModelState.IsValid)
            {
                fileName=fileName.Trim();
                //File Upload
                var blogImage = new BlogImage
                {
                    FileExtension = file.FileName,
                    FileName = fileName,
                    Title = title,
                    DateCreated = DateTime.Now
                };

                var addedBlogImage = await _imageService.UploadImage(file, blogImage);
                
                var blogImageDto=new BlogImageDto 
                {
                    Id=addedBlogImage.Id,
                    Title=addedBlogImage.Title,
                    FileName=addedBlogImage.FileName,
                    FileExtension=addedBlogImage.FileExtension,
                    Url=addedBlogImage.Url,
                    DateCreated=addedBlogImage.DateCreated

                };
                return Ok(blogImageDto);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images= await  _imageService.GetAllImagesAsync();
            var response= new List<BlogImageDto>();

            foreach (var image in images)
            {
                response.Add(new BlogImageDto
                {
                    Id = image.Id,
                    Title = image.Title,
                    FileName = image.FileName,
                    FileExtension = image.FileExtension,
                    Url = image.Url,
                    DateCreated = image.DateCreated

                });
            }

            
            return Ok(response);
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowExtensions=new string [] {".jpg",".jpeg",".png"};

            if(!allowExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            {
                ModelState.AddModelError("file","Unsuppoerted file format");
            }

            if(file.Length>10485760)
            {
                ModelState.AddModelError("file","File size can not be more than 10MB");
            }

        }
    }
}