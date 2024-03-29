using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product.Category
{
    public class Category
    {
        #region Properties

        public int Id { get; set; }

        public string CategoryTitle { get; set; }

        public string CategoryUniqueName { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<SelectedCategory.SelectedCategory> selectedCategories { get; set; }

        #endregion
    }
}
