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

        public HomeController(IProductService productService) 
        {
            _IProductService = productService;

        }
        #endregion


        public async Task<IActionResult> Index()
        {
           List<ProductIndexDTO> Products = await  _IProductService.GetAllProducts();

            IndexViewModel indexViewModel = new IndexViewModel()
            {
                ProductsDTOs = Products,
            };
            
            return View(indexViewModel);
        }

       

    }
}
