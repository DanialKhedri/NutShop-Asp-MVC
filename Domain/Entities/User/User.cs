using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.User
{
    public class User
    {
        #region Properties

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }


        public bool Isdelete { get; set; } = false;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        #endregion


        #region Navigation Properties


        public ICollection<SelectedRole.SelectedRole> selectedRoles { get; set; }

        #endregion

    }
}
