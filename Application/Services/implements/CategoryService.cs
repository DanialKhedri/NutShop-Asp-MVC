using Application.Dtos.CategoryDTO;
using Application.Services.Interfaces;
using Domain.Entities.Product.Category;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements
{
    public class CategoryService : ICategoryService
    {
        #region Ctor 

        private readonly ICategoryRepository _ICategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _ICategoryRepository = categoryRepository;
        }
        #endregion

        #region GetAllCategories

        public async Task<List<CategoryIndexDTO>> GetAllCategories() 
        {
            List<Category> categories = await _ICategoryRepository.GetAllCategories();

            List<CategoryIndexDTO> CategoryDTOList = new List<CategoryIndexDTO>();

            foreach (var item in categories)
            {
                CategoryIndexDTO categoryIndexDTO = new CategoryIndexDTO()
                {
                    Id = item.Id,
                    CategoryTitle = item.CategoryTitle,
                    CategoryUniqueName = item.CategoryUniqueName,
                    Image = item.Image,
                };


                CategoryDTOList.Add(categoryIndexDTO);
            }

            return  CategoryDTOList;
        }

        #endregion
    }
}
