using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order
{
    public class Order
    {
        #region Properties

        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreateTime { get; set; }

        public int Sum { get; set; }

        public bool IsFinaly { get; set; }

        #endregion


        #region Navigation Properties


        public ICollection<OrderDetail.OrderDetail> orderDetails { get; set; }



        #endregion

    }


}
