using Domain.Entities.Product.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface ICategoryRepository
    {


        public Task<List<Category>> GetAllCategories();

    }
}
