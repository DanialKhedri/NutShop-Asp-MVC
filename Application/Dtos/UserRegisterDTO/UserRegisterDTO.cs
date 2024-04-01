using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.UserRegisterDTO
{
    public class UserRegisterDTO
    {


        public string UserName { get; set; }
        public string Phone { get; set; }

        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="رمز و تکرار رمز متفاوت هستند")]
        public string RePassword { get; set; }


    }
}
