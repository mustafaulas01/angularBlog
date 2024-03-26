using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTO
{
    public class CreateCategoryDto
    {
        
        public string Name { get; set; }
        public string UrlHandle { get; set; }
    }
}