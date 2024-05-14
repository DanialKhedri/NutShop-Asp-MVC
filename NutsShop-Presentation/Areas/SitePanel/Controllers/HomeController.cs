using Application.Dtos.CategoryDTO;
using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;
using Application.Dtos.ProductDTO;
using Application.Extensions;
using Application.Services.Interfaces;
using Domain.Entities.Order;
using Microsoft.AspNetCore.Mvc;
using NutsShop_Presentation.Areas.SitePanel.ViewModels;
using System.Net.WebSockets;

namespace NutsShop_Presentation.Areas.SitePanel.Controllers;

[Area("SitePanel")]
public class HomeController : Controller
{
    #region Ctor 

    private readonly IProductService _IProductService;
    private readonly ICategoryService _ICategoryService;
    private readonly IOrderService _IOrderService;
    private readonly IShopService _IShopService;
    private readonly IAboutUsService _IAboutUsService;

    public HomeController(IProductService productService,
        ICategoryService categoryService,
        IOrderService orderService,
        IShopService shopService,
        IAboutUsService aboutUsService)
    {
        _IProductService = productService;
        _ICategoryService = categoryService;
        _IOrderService = orderService;
        _IShopService = shopService;
        _IAboutUsService = aboutUsService;
    }

    #endregion


    #region Index
    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            int UserId = User.GetUserId();
            TempData["CartCount"] = await _IOrderService.GetOrderDetailsCount(UserId);

        }
        else
            TempData["CartCount"] = 0;

        TempData["Shop"] = await _IShopService.GetShopDetail();
        TempData["Categories"] = await _ICategoryService.GetAllCategories();





        List<ProductDTO> Products = await _IProductService.GetAllProducts();
        List<CategoryDTO> categories = await _ICategoryService.GetAllCategories();



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
        if (User.Identity.IsAuthenticated)
        {
            int UserId = User.GetUserId();
            TempData["CartCount"] = await _IOrderService.GetOrderDetailsCount(UserId);

        }
        else
            TempData["CartCount"] = 0;


        var productdto = await _IProductService.GetProductById(Id);
        TempData["Shop"] = await _IShopService.GetShopDetail();
        TempData["Categories"] = await _ICategoryService.GetAllCategories();

        return View(productdto);

    }

    #endregion


    #region ShowAllProducts

    public async Task<IActionResult> ShowAllProducts()
    {
        if (User.Identity.IsAuthenticated)
        {
            int UserId = User.GetUserId();
            TempData["CartCount"] = await _IOrderService.GetOrderDetailsCount(UserId);

        }
        else
            TempData["CartCount"] = 0;

        TempData["Shop"] = await _IShopService.GetShopDetail();
        TempData["Categories"] = await _ICategoryService.GetAllCategories();

        List<ProductDTO> products = await _IProductService.GetAllProducts();


        ShowProductsViewModel showProductsViewModel = new ShowProductsViewModel()
        {

            ProductsDTOs = products,
        };

        return View(showProductsViewModel);


    }

    #endregion


    #region ShowProductsByCategory

    public async Task<IActionResult> ShowProductByCategory(int CategoryId)
    {
        if (User.Identity.IsAuthenticated)
        {
            int UserId = User.GetUserId();
            TempData["CartCount"] = await _IOrderService.GetOrderDetailsCount(UserId);

        }
        else
            TempData["CartCount"] = 0;

        TempData["Shop"] = await _IShopService.GetShopDetail();
        TempData["Categories"] = await _ICategoryService.GetAllCategories();
        TempData["Category"] = await _ICategoryService.GetCategorybyId(CategoryId);

        List<ProductDTO>? productsDTOList = await _IProductService.GetProductsByCategoryId(CategoryId);

        ShowProductByCategoryIdViewModel showProductByCategoryIdViewModel = new ShowProductByCategoryIdViewModel()
        {


            productIndexDTOs = productsDTOList,


        };


        return View(showProductByCategoryIdViewModel);
    }

    #endregion


    #region Cart
    public async Task<IActionResult> Cart()
    {

        if (User.Identity.IsAuthenticated)
        {
            if (User.Identity.IsAuthenticated)
            {
                int UserId = User.GetUserId();
                TempData["CartCount"] = await _IOrderService.GetOrderDetailsCount(UserId);

            }
            else
                TempData["CartCount"] = 0;

            TempData["Shop"] = await _IShopService.GetShopDetail();
            TempData["Categories"] = await _ICategoryService.GetAllCategories();
            int userid = User.GetUserId();

            CartViewModel cartViewModel = new CartViewModel();

            cartViewModel.OrderDTO = await _IOrderService.GetUnFinaledOrderByUserId(userid);

            if (cartViewModel.OrderDTO != null)
            {
                cartViewModel.OrderDetailDTOs = await _IOrderService.GetAllOrderDetailsByOrderId(cartViewModel.OrderDTO.Id);
            }


            return View(cartViewModel);
        }

        else
        {
            return RedirectToAction("LogIn", "User");
        }


    }

    #endregion


    #region About Us

    public async Task<IActionResult> AboutUs()
    {
        if (User.Identity.IsAuthenticated)
        {
            int UserId = User.GetUserId();
            TempData["CartCount"] = await _IOrderService.GetOrderDetailsCount(UserId);

        }
        else
            TempData["CartCount"] = 0;

        TempData["Shop"] = await _IShopService.GetShopDetail();
        TempData["Categories"] = await _ICategoryService.GetAllCategories();


        var aboutus = await _IAboutUsService.GetAboutUs();    
        return View(aboutus);
    }

    #endregion


}
