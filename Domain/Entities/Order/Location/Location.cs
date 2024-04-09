using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Order.Location
{
    public class Location
    {
        #region Properties

        public int Id { get; set; }

        public int OrderId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int PhoneNumber { get; set; }

        public int PostCode { get; set; }

        #endregion

        #region Navigation Properties

        public Order order { get; set; }

        #endregion




    }
}
