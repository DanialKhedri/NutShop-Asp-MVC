using Application.Dtos.CategoryDTO;
using Application.Dtos.ProductDTO;
using Application.Dtos.UserLogInDTO;
using Application.Extensions.NameGenerator;
using Application.Services.Interfaces;
using Domain.Entities.Product;
using Domain.Entities.Product.Category;
using Domain.Entities.Product.SelectedCategory;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.implements;

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

    public async Task<List<CategoryDTO>> GetAllCategories()
    {
        List<Category> categories = await _ICategoryRepository.GetAllCategories();

        List<CategoryDTO> CategoryDTOList = new List<CategoryDTO>();

        foreach (var item in categories)
        {
            CategoryDTO categoryIndexDTO = new CategoryDTO()
            {
                Id = item.Id,
                CategoryTitle = item.CategoryTitle,
                CategoryUniqueName = item.CategoryUniqueName,
                Image = item.Image,
            };


            CategoryDTOList.Add(categoryIndexDTO);
        }

        return CategoryDTOList;
    }

    #endregion

    #region GetCategoryById

    public async Task<CategoryDTO> GetCategorybyId(int CategoryId)
    {

        Category category = await _ICategoryRepository.GetCategoryById(CategoryId);


        //Object Mapping

        CategoryDTO categoryDTO = new CategoryDTO()
        {
            Id = category.Id,
            CategoryTitle = category.CategoryTitle,
            CategoryUniqueName = category.CategoryUniqueName,
            Image = category.Image,
        };

        return categoryDTO;
    }

    #endregion



    #region Add Category

    public async Task AddCategory(CategoryDTO categoryDTO)
    {


        //Object Mapping

        Category category = new Category()
        {
            CategoryTitle = categoryDTO.CategoryTitle,
            CategoryUniqueName = categoryDTO.CategoryUniqueName,

        };


        #region AddImage 

        if (categoryDTO.ImageIFormFile != null)
        {
            //Save New Image
            category.Image = NameGenerator.GenerateUniqCode() + Path.GetExtension(categoryDTO.ImageIFormFile.FileName);

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Categories", category.Image);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                categoryDTO.ImageIFormFile.CopyTo(stream);
            }
        }

        #endregion


        await _ICategoryRepository.AddCategory(category);

    }

    #endregion

    #region Edit Category
    public async Task EditCategory(CategoryDTO categoryDTO)
    {
        Category Category = new Category()
        {
            Id = categoryDTO.Id,
            CategoryTitle = categoryDTO.CategoryTitle,
            CategoryUniqueName = categoryDTO.CategoryUniqueName,
            Image = categoryDTO.Image,

        };

        await _ICategoryRepository.EditCategory(Category);


    }

    #endregion

    #region Remove Category

    public async Task RemoveCategory(int CategoryId)
    {

        await _ICategoryRepository.RemoveCategory(CategoryId);


    }

    #endregion

    #region AddSelectedCategory


    public async Task AddSelectedCategory(List<int> SelectedCategories, ProductDTO productDTO)
    {

        if (SelectedCategories != null)
        {

            await _ICategoryRepository.RemoveSelectedCategoriesByProductId(productDTO.Id);


            foreach (var item in SelectedCategories)
            {

                SelectedCategory selectedCategory = new SelectedCategory()
                {

                    CategoryId = item,
                    ProductId = productDTO.Id,

                };

                await _ICategoryRepository.AddSelectedCategory(selectedCategory);

            }

            await _ICategoryRepository.SaveChangeAsync();   

        }




    }

    #endregion



}
