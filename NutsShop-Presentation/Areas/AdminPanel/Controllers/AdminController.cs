﻿using Application.Dtos.AboutUsDTO;
using Application.Dtos.CategoryDTO;
using Application.Dtos.LocationDTO;
using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;
using Application.Dtos.ProductDTO;
using Application.Dtos.ShopDTO;
using Application.Dtos.UserLogInDTO;
using Application.Extensions;
using Application.Services.Interfaces;
using Domain.Entities.Order;
using Domain.Entities.Order.OrderDetail;
using Microsoft.AspNetCore.Mvc;
using NutsShop_Presentation.Areas.AdminPanel.ViewModels;

namespace NutsShop_Presentation.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class AdminController : Controller
{

    #region Ctor

    private readonly IProductService _IProductService;
    private readonly IOrderService _IOrderService;
    private readonly IUserService _IUserService;
    private readonly ICategoryService _ICategoryService;
    private readonly IRoleService _IRoleService;
    private readonly IShopService _IShopService;
    private readonly IAboutUsService _IAboutUs;

    public AdminController(IProductService productService,
        IOrderService orderService,
        IUserService userService,
        ICategoryService categoryService,
        IRoleService roleService,
        IShopService shopService,
        IAboutUsService aboutUsService)
    {

        _IProductService = productService;
        _IOrderService = orderService;
        _IUserService = userService;
        _ICategoryService = categoryService;
        _IRoleService = roleService;
        _IShopService = shopService;
        _IAboutUs = aboutUsService;

    }


    #endregion



    #region Index

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }

    #endregion



    #region Products Managment


    //Get All Products

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {


        var ProductList = await _IProductService.GetAllProducts();



        return View(ProductList);
    }



    //Add

    [HttpGet]
    public async Task<IActionResult> AddProduct()
    {

        AddProductViewModel addProductViewModel = new AddProductViewModel()
        {

            categories = await _ICategoryService.GetAllCategories(),

        };




        return View(addProductViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDTO productDTO, List<int> SelectedCategories)
    {

        await _IProductService.AddProduct(productDTO, SelectedCategories);


        return RedirectToAction(nameof(GetAllProducts));
    }



    //Edit

    [HttpGet]
    public async Task<IActionResult> EditProduct(int ProductId)
    {

        var product = await _IProductService.GetProductById(ProductId);

        var Categories = await _ICategoryService.GetAllCategories();

        EditProductViewModel editProductViewModel = new EditProductViewModel()
        {

            productDTO = product,
            categories = Categories,
        };


        return View(editProductViewModel);

    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(ProductDTO productDTO, List<int> SelectedCategories)
    {

        await _IProductService.EditProduct(productDTO);

        await _ICategoryService.AddSelectedCategory(SelectedCategories, productDTO);

        return RedirectToAction(nameof(GetAllProducts));
    }


    //Remove

    public async Task<IActionResult> RemoveProduct(int ProductId)
    {
        await _IProductService.RemoveProduct(ProductId);


        return RedirectToAction(nameof(GetAllProducts));
    }



    #endregion



    #region Category Managment

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var Categories = await _ICategoryService.GetAllCategories();


        return View(Categories);

    }

    //Add 

    [HttpGet]
    public async Task<IActionResult> AddCategory()
    {


        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CategoryDTO CategoryDTO)
    {


        await _ICategoryService.AddCategory(CategoryDTO);


        return RedirectToAction(nameof(GetAllCategories));


    }

    //Edit

    public async Task<IActionResult> EditCategory(int CategoryId)
    {
        CategoryDTO Category = await _ICategoryService.GetCategorybyId(CategoryId);

        return View(Category);

    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(CategoryDTO categoryDTO)
    {

        await _ICategoryService.EditCategory(categoryDTO);

        return RedirectToAction(nameof(GetAllCategories));

    }


    //Remove

    public async Task<IActionResult> RemoveCategory(int CategoryId)
    {
        await _ICategoryService.RemoveCategory(CategoryId);
        return RedirectToAction(nameof(GetAllCategories));
    }


    #endregion



    #region User Managment

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {


        var UserDTOs = await _IUserService.GetAllUser();


        return View(UserDTOs);

    }



    //Edit

    [HttpGet]
    public async Task<IActionResult> EditUser(int UserId)
    {

        var user = await _IUserService.GetUserById(UserId);

        var Roles = await _IRoleService.GetAllRoles();

        EditUserViewModel editUserViewModel = new EditUserViewModel()
        {
            UserAdminPanelDTO = user,
            Roles = Roles,
        };

        return View(editUserViewModel);


    }

    [HttpPost]
    public async Task<IActionResult> EditUser(UserAdminPanelDTO userAdminPanelDTO, List<int> SelectedRoles)
    {

        await _IUserService.EditUser(userAdminPanelDTO);

        if (SelectedRoles.Count != 0)
        {
            await _IRoleService.AddSelectedRole(SelectedRoles, userAdminPanelDTO);
        }




        return RedirectToAction(nameof(GetAllUsers));
    }

    //Remove

    public async Task<IActionResult> RemoveUser(int UserId)
    {
        await _IUserService.RemoveProduct(UserId);

        return RedirectToAction(nameof(GetAllUsers));

    }




    #endregion





    #region Orders Managment

    //Get All Orders

    public async Task<IActionResult> GetAllOrders()
    {

        var OrdersList = await _IOrderService.GetAllFinaledOrders();


        return View(OrdersList);

    }

    //Details

    [HttpGet]
    public async Task<IActionResult> GetOrderDetails(int OrderId)
    {

        //Get Order By Order Id
        var Order = await _IOrderService.GetFinalyOrderByOrderId(OrderId);

        //Get orderDetails By Order Id
        List<OrderDetailDTO>? orderDetails = await _IOrderService.GetAllOrderDetailsByOrderId(Order.Id);

        //Location
        LocationDTO? location = await _IOrderService.GetLocationByOrderId(Order.Id);


        GetOrderDetailsViewModel getOrderDetailsViewModel = new GetOrderDetailsViewModel()
        {
            OrderDTO = Order,
            orderDetails = orderDetails,
            LocationDTO = location,

        };

        return View(getOrderDetailsViewModel);

    }


    //Remove

    public async Task<IActionResult> RemoveOrder(int OrderId)
    {
        await _IOrderService.RemoveOrder(OrderId);

        return RedirectToAction(nameof(GetAllOrders));
    }

    #endregion



    #region Shop Detail Managment

    public async Task<IActionResult> ShopDetails()
    {

        var shop = await _IShopService.GetShopDetail();

        return View(shop);
    }

    public async Task<IActionResult> EditShopDetails(ShopDTO shopDTO)
    {

        await _IShopService.EditShopDetail(shopDTO);

        return RedirectToAction($"{nameof(Index)}");

    }


    #endregion


    #region AboutUsManagment

    public async Task<IActionResult> GetAboutUs()
    {
        var AboutUs = await _IAboutUs.GetAboutUs();

        return View(AboutUs);

    }


    public async Task<IActionResult> EditAboutUs(AboutUsDTO aboutUsDTO)
    {

        await _IAboutUs.EditAboutUs(aboutUsDTO);

        return RedirectToAction(nameof(Index));
    }

    #endregion

}
