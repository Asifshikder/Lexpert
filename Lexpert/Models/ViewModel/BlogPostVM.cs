using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Models
{
    public class BlogPostVM
    {
        [Key]
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogImage { get; set; }
        public IFormFile BlogImageFile { get; set; }
        public string Content { get; set; }
        public DateTime? PostTime { get; set; }
    }
}
