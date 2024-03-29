using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cart
{
    public  class Cart
    {

        public int Id { get; set; }

        public List<Product.Product> Products { get; set; }


        public int SumPrice { get; set; }

    }
}
