using Application.Dtos.CategoryDTO;
using Domain.Entities.Product;
using Domain.Entities.Product.Category;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository;

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

        return await _datacontext.Categories.Where(c => c.Isdelete == false)
            .ToListAsync();

    }
    #endregion

    #region GetCategoryById

    public async Task<Category> GetCategoryById(int CategoryId)
    {


        return await _datacontext.Categories.FirstOrDefaultAsync(c => c.Id == CategoryId && c.Isdelete == false);



    }

    #endregion

    #region Add Category

    public async Task AddCategory(Category category)
    {
        await _datacontext.Categories.AddAsync(category);
        await _datacontext.SaveChangesAsync();
    }

    #endregion

    #region Edit Category
    public async Task EditCategory(Category category)
    {

        Category originCategory = await GetCategoryById(category.Id);

        originCategory.CategoryTitle = category.CategoryTitle;
        originCategory.CategoryUniqueName = category.CategoryUniqueName;


        if (category.Image != null)
            originCategory.Image = category.Image;




        _datacontext.Update(originCategory);
        await _datacontext.SaveChangesAsync();
    }

    #endregion

    #region Remove Category

    public async Task RemoveCategory(int CategoryId)
    {
        Category category = await GetCategoryById(CategoryId);

        category.Isdelete = true;

        _datacontext.Update(category);
        await _datacontext.SaveChangesAsync();


    }

    #endregion

}
