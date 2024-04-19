using Application.Dtos.CategoryDTO;
using Application.Dtos.LocationDTO;
using Application.Dtos.OrderDetailDTO;
using Application.Dtos.OrderDTO;
using Application.Dtos.ProductDTO;
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

        var Categories = await _ICategoryService.GetAllCategories();

        EditProductViewModel editProductViewModel = new EditProductViewModel()
        {

            productDTO = product,
            categories = Categories,
        };


        return View(editProductViewModel);

    }

    [HttpPost]
    public async Task<IActionResult> EditProduct(ProductDTO productDTO)
    {

        await _IProductService.EditProduct(productDTO);

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
        await _IRoleService.AddSelectedRole(SelectedRoles, userAdminPanelDTO);



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





}
