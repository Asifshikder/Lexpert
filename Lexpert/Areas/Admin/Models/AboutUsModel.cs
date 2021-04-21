using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lexpert.Areas.Admin.Models
{
    public class AboutUsModel
    {
        public int AboutUsId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
