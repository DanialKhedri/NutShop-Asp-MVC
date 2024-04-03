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
        public Task<List<CategoryIndexDTO>> GetAllCategories();

    }
}
