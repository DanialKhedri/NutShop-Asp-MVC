using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CategoryDTO
{
    public class CategoryDTO
    {

        public int Id { get; set; }

        public string CategoryTitle { get; set; }

        public string CategoryUniqueName { get; set; }

        public string Image { get; set; }

        public IFormFile ImageIFormFile { get; set; }

    }
}
