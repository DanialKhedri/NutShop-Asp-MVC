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

    public AdminController(IProductService productService, IOrderService orderService, IUserService userService)
    {

        _IProductService = productService;
        _IOrderService = orderService;
        _IUserService = userService;

    }


    #endregion











    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }



    #region Products Managment

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {

        return View();


    }

    #endregion



    #region Category Managment

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {



        return View();

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




        return View();

    }
    #endregion





}
