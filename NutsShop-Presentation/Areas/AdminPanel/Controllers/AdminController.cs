using Application.Dtos.ProductDTO;
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
    private readonly IRoleService _IRoleService;

    public AdminController(IProductService productService,
        IOrderService orderService,
        IUserService userService,
        ICategoryService categoryService,
        IRoleService roleService)
    {

        _IProductService = productService;
        _IOrderService = orderService;
        _IUserService = userService;
        _ICategoryService = categoryService;
        _IRoleService = roleService;

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
        return View();
    }



    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductDTO productDTO)
    {
        await _IProductService.AddProduct(productDTO);

        return View();
    }



    //Edit

    [HttpGet]
    public async Task<IActionResult> EditProduct(int ProductId)
    {

        var product = await _IProductService.GetProductById(ProductId);

        return View(product);

    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(ProductDTO productDTO)
    {

        await _IProductService.EditProduct(productDTO);

        return RedirectToAction(nameof(Index));
    }


    //Remove

    public async Task<IActionResult> RemoveProduct(int ProductId)
    {



        return View();
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


        var UserDTOs = await _IUserService.GetAllUser();


        return View(UserDTOs);

    }


    #endregion


    #region RoleManagment

    public async Task<IActionResult> GetAllRoles()
    {

        var Roles = await _IRoleService.GetAllRoles();


        return View(Roles);

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
