using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserLogInDTO
{
    public class UserAdminPanelDTO
    {

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

 
        public DateTime CreateDate { get; set; } = DateTime.Now;





    }
}
