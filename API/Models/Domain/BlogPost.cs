using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Domain
{
    public class BlogPost
    {
    
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string Content { get; set; }
    public string  FeatureImageUrl { get; set; }
    public string UrlHandle { get; set; }
    public DateTime PublishedDate { get; set; }
    public string Author { get; set; }

    public bool IsVisible { get; set; }

    public virtual List<Category> Categories { get; set; }=new List<Category>();
        
    }
}