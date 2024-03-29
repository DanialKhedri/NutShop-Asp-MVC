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


        public bool Isdelete { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        #endregion

        #region Navigation Properties


        public ICollection<SelectedRole.SelectedRole> selectedRoles { get; set; }

        #endregion

    }
}
