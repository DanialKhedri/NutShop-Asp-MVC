﻿using Application.Dtos.CategoryDTO;
using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;
using Application.Dtos.ProductDTO;
using Application.Extensions;
using Application.Services.Interfaces;
using Domain.Entities.Order;
using Microsoft.AspNetCore.Mvc;
using NutsShop_Presentation.Areas.SitePanel.ViewModels;

namespace NutsShop_Presentation.Areas.SitePanel.Controllers;

[Area("SitePanel")]
public class HomeController : Controller
{
    #region Ctor 

    private readonly IProductService _IProductService;
    private readonly ICategoryService _ICategoryService;
    private readonly IOrderService _IOrderService;
    private readonly IShopService _IShopService;


    public HomeController(IProductService productService,
        ICategoryService categoryService, 
        IOrderService orderService,
        IShopService shopService)
    {
        _IProductService = productService;
        _ICategoryService = categoryService;
        _IOrderService = orderService;
        _IShopService = shopService;

    }

    #endregion


    #region Index
    public async Task<IActionResult> Index()
    {

        List<ProductDTO> Products = await _IProductService.GetAllProducts();
        List<CategoryDTO> categories = await _ICategoryService.GetAllCategories();

        TempData["Shop"] = await _IShopService.GetShopDetail();

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
            return RedirectToAction(nameof(Index));
        }


    }

    #endregion


}
