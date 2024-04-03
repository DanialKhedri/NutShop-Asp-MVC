using Application.Dtos.CategoryDTO;
using Application.Dtos.ProductDTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NutsShop_Presentation.Areas.SitePanel.ViewModels;

namespace NutsShop_Presentation.Areas.SitePanel.Controllers
{
    [Area("SitePanel")]
    public class HomeController : Controller
    {
        #region Ctor 

        private readonly IProductService _IProductService;
        private readonly ICategoryService _ICategoryService;
        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _IProductService = productService;
            _ICategoryService = categoryService;

        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<ProductIndexDTO> Products = await _IProductService.GetAllProducts();
            List<CategoryIndexDTO> categories = await _ICategoryService.GetAllCategories();

            IndexViewModel indexViewModel = new IndexViewModel()
            {
                ProductsDTOs = Products,
                CategoryDTOs = categories
            };

            return View(indexViewModel);
        }
        #endregion


    }
}
