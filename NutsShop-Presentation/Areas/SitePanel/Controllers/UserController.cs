using Application.Dtos.LocationDTO;
using Application.Dtos.UserLogInDTO;
using Application.Dtos.UserRegisterDTO;
using Application.Extensions;
using Application.Services.implements;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NutsShop_Presentation.Areas.SitePanel.Controllers;



[Area("SitePanel")]
public class UserController : Controller
{

    #region Ctor

    private readonly IUserService _IUserService;
    private readonly IOrderService _IOrderService;
    private readonly IShopService _IShopService;
    private readonly ISmsService _SmsService;
    public UserController(IUserService userService,
        IOrderService orderService,
        IShopService ShopService,
        ISmsService smsService)
    {
        _IUserService = userService;
        _IOrderService = orderService;
        _IShopService = ShopService;
        _SmsService = smsService;
    }

    #endregion



    #region Register


    [HttpGet]
    public async Task<IActionResult> Register()
    {
        TempData["Shop"] = await _IShopService.GetShopDetail();
        return View();

    }

    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
    {


        if (ModelState.IsValid)
        {

            if (userRegisterDTO.Password == userRegisterDTO.RePassword)
            {
                var IsSucces = await _IUserService.Register(userRegisterDTO);

                if (IsSucces)
                {

                    _IUserService.SaveChange();
                    return RedirectToAction("Index", "Home");

                }

                else
                {
                    TempData["Message"] = "This Username or Phone Used Befor";
                    return View();
                }

            }

            else
            {

                TempData["Message"] = "Passwor and Repassword Are Different";
                return View();

            }

        }

        else
        {

            return View();

        }


    }
    #endregion



    #region LogIn

    [HttpGet]
    public async Task<IActionResult> LogIn()
    {
        TempData["Shop"] = await _IShopService.GetShopDetail();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(UserLogInDTO userLogInDTO)
    {
        if (ModelState.IsValid && userLogInDTO != null)
        {
            var IsSucces = await _IUserService.LogIn(userLogInDTO);

            if (IsSucces)
            {
                return RedirectToAction("Index", "Home");
            }

            else
            {
                TempData["Message"] = "This Username or Phone Not found or password Is Wrong";
                return View();
            }


        }
        else
        {
            return View();
        }



    }

    #endregion

    #region Login With sms

    public async Task LogInWithSms()
    {
        await _SmsService.SendPublicSms("09336314704", "سلام سوسیس");
        await HttpContext.Response.WriteAsync("Sms Sent");
    }

    //public async Task LookUpSms()
    //{
    //    await _SmsService.SendLookUpSms("09336314704","Password");
    //    await HttpContext.Response.WriteAsync("Sms Sent");
    //}

    #endregion

    #region LogOut

    public async Task<IActionResult> LogOut()
    {

        await HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Home");

    }

    #endregion



    #region AddProductToCart

    public async Task<IActionResult> AddProductToCart(int ProductId, int Weight)
    {
        if (User.Identity.IsAuthenticated)
        {

            int UserId = UserExtensions.GetUserId(User);


            await _IOrderService.AddProductToCart(UserId, ProductId, Weight);


            //_IOrderService.SaveChange();
            return RedirectToAction("Index", "Home");

        }

        else
        {

            //Go to LogIn Page
            return RedirectToAction(nameof(LogIn));

        }

    }

    #endregion



    #region RemoveOrderDetailFromCart

    public async Task<IActionResult> RemoveOrderDetail(int Id)
    {
        await _IOrderService.RemoveOrderDetail(Id);

        return RedirectToAction("Cart", "Home");
    }


    #endregion


    #region SetOrderLocation
    [HttpGet]
    public async Task<IActionResult> SetOrderLocation()
    {
        TempData["Shop"] = await _IShopService.GetShopDetail();

        return View();

    }

    [HttpPost]
    public async Task<IActionResult> SetOrderLocation(LocationDTO locationDTO)
    {

        int UserId = User.GetUserId();
        await _IOrderService.AddOrderLocation(locationDTO, UserId);
        return View();

    }
    #endregion




}
