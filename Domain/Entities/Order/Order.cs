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

        public DateTime CreateTime { get; set; } =DateTime.Now;

        public int Sum { get; set; }

        public bool IsFinaly { get; set; } = false;

        public bool IsDelete { get; set; } = false;

        #endregion


        #region Navigation Properties

        public ICollection<OrderDetail.OrderDetail>? orderDetails { get; set; }

        #endregion

    }


}
