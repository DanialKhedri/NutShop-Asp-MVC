using Domain.Entities.Product.Category;
using Domain.Entities.Product.SelectedCategory;
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

        public  Task AddSelectedCategory(SelectedCategory selectedCategory);

        public Task RemoveSelectedCategoriesByProductId(int ProductId);
        public Task SaveChangeAsync();


    }
}
