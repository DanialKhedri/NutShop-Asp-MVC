using Application.Dtos.CategoryDTO;
using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;
using Application.Dtos.ProductDTO;
using Application.Extensions;
using Application.Services.Interfaces;
using Domain.Entities.Order;
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
        private readonly IOrderService _IOrderService;

        public HomeController(IProductService productService, ICategoryService categoryService, IOrderService orderService)
        {
            _IProductService = productService;
            _ICategoryService = categoryService;
            _IOrderService = orderService;

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


        #region ShowProduct

        public async Task<IActionResult> ShowProduct(int Id)
        {

            var productdto = await _IProductService.GetProductById(Id);


            return View(productdto);

        }

        #endregion


        #region ShowAllProducts

        public async Task<IActionResult> ShowAllProducts()
        {

            List<ProductIndexDTO> products = await _IProductService.GetAllProducts();


            ShowProductsViewModel showProductsViewModel = new ShowProductsViewModel()
            {

                ProductsDTOs = products,
            };

            return View(showProductsViewModel);


        }

        #endregion

        #region Cart
        public async Task<IActionResult> Cart()
        {

            if (User.Identity.IsAuthenticated)
            {
                int userid = User.GetUserId();

                CartViewModel cartViewModel = new CartViewModel();

                cartViewModel.OrderDTO = await _IOrderService.GetOrderByUserID(userid);
                cartViewModel.OrderDetailDTOs = await _IOrderService.GetAllOrderDetails(userid);

                return View(cartViewModel);
            }

            else
            {
                return RedirectToAction(nameof(Index));
            }


        }

        #endregion


    }
}
