using Application.Dtos.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<CategoryDTO>> GetAllCategories();
        public Task<CategoryDTO> GetCategorybyId(int CategoryId);

        public Task AddCategory(CategoryDTO categoryDTO);
        public Task EditCategory(CategoryDTO categoryDTO);

        public Task RemoveCategory(int CategoryId);

    }
}
