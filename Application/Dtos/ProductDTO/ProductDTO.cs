using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;


namespace Application.Dtos.ProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }


        public string? Image { get; set; }

        public IFormFile? ImageIformFile { get; set; }

    }
}
