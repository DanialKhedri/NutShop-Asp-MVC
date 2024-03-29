using Domain.Entities.User.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User.SelectedRole
{
    public class SelectedRole
    {

        #region Properties

        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        #endregion


        #region Navigation Properties


        public User User { get; set; }

        public Role.Role Role { get; set; } 

        #endregion

    }
}
