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
        public Task<Category> GetCategoryById(int CategoryId);



        public Task AddCategory(Category category);
        public Task EditCategory(Category category);
        public Task RemoveCategory(int CategoryId);


    }
}
