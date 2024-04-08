using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.OrderDTO
{
    public class OrderDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreateTime { get; set; }

        public int Sum { get; set; }

        public bool IsFinaly { get; set; }

    }
}
