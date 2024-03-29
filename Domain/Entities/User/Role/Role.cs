using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User.Role
{
    public class Role
    {

        #region Properties

        public int Id { get; set; }

        public string RoleTitle { get; set; }

        public string RoleUniqueName { get; set; }

        #endregion

        #region Navigation Properties


        public ICollection<SelectedRole.SelectedRole> selectedRoles { get; set; }

        #endregion

    }
}
