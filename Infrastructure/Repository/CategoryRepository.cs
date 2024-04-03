using Application.Dtos.CategoryDTO;
using Domain.Entities.Product.Category;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Ctor

        private readonly DataContext _datacontext;
        public CategoryRepository(DataContext dataContext)
        {
            _datacontext = dataContext;
        }
        #endregion


        #region GetAllCategories
        public async Task<List<Category>> GetAllCategories()
        {

            return await _datacontext.Categories.ToListAsync();

        }
        #endregion








    }
}
