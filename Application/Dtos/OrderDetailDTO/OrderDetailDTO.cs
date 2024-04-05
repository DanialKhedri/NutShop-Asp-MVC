using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.OrderDetailDTO
{
    public class OrderDetailDTO
    {


        public int Id { get; set; }

        public int Price { get; set; }

        public string? Producttitle { get; set; }

        public string? ProductImage { get; set; }

    }

}
