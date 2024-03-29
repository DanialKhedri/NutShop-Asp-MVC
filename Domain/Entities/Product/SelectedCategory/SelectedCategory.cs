using Domain.Entities.Product.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Product.SelectedCategory
{
    public class SelectedCategory
    {

        #region Properties

        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CategoryId { get; set; }


        #endregion


        #region Navigation Properties

        public Product.Product Product { get; set; }

        public Category Category { get; set; }

        #endregion



    }
}
