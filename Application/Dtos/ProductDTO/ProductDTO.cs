using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ProductDTO
{
    public class ProductDTO
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }

        public string Image { get; set; }


    }
}
