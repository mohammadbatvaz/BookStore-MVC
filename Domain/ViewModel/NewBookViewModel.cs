using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModel
{
    public class NewBookViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Author { get; set; }
        public int PagesNumber { get; set; }
        public int Price { get; set; }
        public IFormFile Image { get; set; }
    }
}
