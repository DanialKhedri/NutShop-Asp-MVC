using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product
{
    public class Product
    {

        #region Properties

        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public int? Weight { get; set; }

        #endregion

        #region Navigation Properties


        public ICollection<SelectedCategory.SelectedCategory> selectedCategories { get; set; }
        #endregion
    }
}
