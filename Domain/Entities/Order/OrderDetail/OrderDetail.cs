using Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order.OrderDetail
{
    public class OrderDetail
    {
        #region Properties

        public int Id { get; set; }

        public int OrderId { get; set; }


        public int ProductId { get; set; }


        public int Count { get; set; }

        public int Price { get; set; }

        #endregion


        #region Navigation Properties

        public Order Order { get; set; }

        public Product.Product Product { get; set; }

        #endregion

    }
}
