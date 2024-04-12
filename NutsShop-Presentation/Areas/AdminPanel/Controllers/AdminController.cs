using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NutsShop_Presentation.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class AdminController : Controller
{

    #region Ctor

    private readonly IProductService _IProductService;
    private readonly IOrderService _IOrderService;
    private readonly IUserService _IUserService;
    private readonly ICategoryService _ICategoryService;


    public AdminController(IProductService productService,
        IOrderService orderService, 
        IUserService userService,
        ICategoryService categoryService)
    {

        _IProductService = productService;
        _IOrderService = orderService;
        _IUserService = userService;
        _ICategoryService = categoryService;

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

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {


        var ProductList = await _IProductService.GetAllProducts();



        return View(ProductList);
    }

    #endregion



    #region Category Managment

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var Categories = await _ICategoryService.GetAllCategories();


        return View(Categories);

    }

    #endregion


    #region User Managment

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {


      


        return View();

    }


    #endregion


    #region Orders Managment

    public async Task<IActionResult> GetAllOrders()
    {

        var OrdersList = await _IOrderService.GetAllOrdersForAdminPanel();


        return View(OrdersList);

    }
    #endregion





}
