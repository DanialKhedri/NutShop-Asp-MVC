using Application.Dtos.LocationDTO;
using Application.Dtos.OrderDTO;
using Application.Dtos.UserLogInDTO;
using Application.Dtos.UserRegisterDTO;
using Application.Dtos.VerifyOtpDTO;
using Application.Extensions;
using Application.Extensions.Generators;
using Application.Extensions.OtpSharp;
using Application.Services.implements;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using ZarinpalSandbox;

namespace NutsShop_Presentation.Areas.SitePanel.Controllers;



[Area("SitePanel")]
public class UserController : Controller
{

    #region Ctor

    private readonly IUserService _IUserService;
    private readonly IOrderService _IOrderService;
    private readonly IShopService _IShopService;
    private readonly ISmsService _SmsService;
    private readonly IPaymentService _IPaymentService;
    private readonly ICategoryService _ICategoryService;

    public UserController(IUserService userService,
        IOrderService orderService,
        IShopService ShopService,
        ISmsService smsService,
        IPaymentService iPaymentService,
        ICategoryService iCategoryService)
    {
        _IUserService = userService;
        _IOrderService = orderService;
        _IShopService = ShopService;
        _SmsService = smsService;
        _IPaymentService = iPaymentService;
        _ICategoryService = iCategoryService;
    }

    #endregion



    #region Register


    [HttpGet]
    public async Task<IActionResult> Register()
    {
        await SetTempData();
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
        await SetTempData();

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

    [HttpGet]
    public async Task<IActionResult> LogInWithSms()
    {
        await SetTempData();
        return View();

    }


    public async Task<IActionResult> LookUpSms(string PhoneNumber)
    {

        if (ModelState.IsValid)
        {
            if (PhoneNumber != null)
            {

                string SecretKey = OtpManger.GenerateOtp();

                VerifyOtpDTO verifyOtpDTO = new VerifyOtpDTO()
                {
                    PhoneNumber = PhoneNumber,

                };

                //await _SmsService.SendLookUpSms(PhoneNumber, "Passwordtest", SecretKey);
                //return RedirectToAction("GetVerifyOtp", verifyOtpDTO);


            }
            else
            {
                return RedirectToAction(nameof(LogInWithSms));
            }



        }

        return RedirectToAction(nameof(LogInWithSms));


    }

    [HttpGet]
    public async Task<IActionResult> GetVerifyOtp(VerifyOtpDTO verifyOtpDTO)
    {
        await SetTempData();
        return View(verifyOtpDTO);

    }

    [HttpPost]
    public async Task<IActionResult> VerifyOtp(VerifyOtpDTO verifyOtpDTO)
    {

        //SecretKey Is Correct 
        bool isverify = OtpManger.VerifyOtp(verifyOtpDTO.SecretKey);

        if (isverify)
        {
            if (verifyOtpDTO.PhoneNumber != null)
            {
                var islog = await _IUserService.LogInWithSms(verifyOtpDTO.PhoneNumber);
                return RedirectToAction("Index", "Home");
            }

        }

        return RedirectToAction(nameof(GetVerifyOtp));


    }


    //public async Task SendPublicSms()
    //{
    //    await _SmsService.SendPublicSms("09336314704", "سلام سوسیس");
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

        await SetTempData();

        return View();

    }

    [HttpPost]
    public async Task<IActionResult> SetOrderLocation(LocationDTO locationDTO)
    {
        if (ModelState.IsValid)
        {
            int UserId = User.GetUserId();
            await _IOrderService.AddOrderLocation(locationDTO, UserId);
            return RedirectToAction(nameof(PaymentAction));
        }
        else 
        {
            return View();
        }

    }
    #endregion



    #region Payment
    public async Task<IActionResult> PaymentAction()
    {
        var UserId = User.GetUserId();

        OrderDTO? Order = await _IOrderService.GetUnFinaledOrderByUserId(UserId);
        UserAdminPanelDTO? user = await _IUserService.GetUserById(UserId);

        if (Order != null)
        {
            var Payment = new Payment(Order.Sum);

            var Request = Payment
                .PaymentRequest($"پرداخت فاکتور شماره{Order.Id}", "https://localhost:7077/User/OnlinePayment/" + Order.Id, "", user.UserName);

            if (Request.Result.Status == 100)
            {

                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + Request.Result.Authority);

            }

            else
            {
                return BadRequest();
            }


        }
        else
        {
            return BadRequest();
        }



    }

    public async Task<IActionResult> OnlinePayment(int Id)
    {

        await SetTempData();

        if (HttpContext.Request.Query["Status"] != ""
            && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok"
            && HttpContext.Request.Query["Authority"] != "")
        {

            string? Authority =  HttpContext.Request.Query["Authority"];

            OrderDTO? orderDTO = await _IOrderService.GetUnFinaledOrderByOrderId(Id);

            var payment = new Payment(orderDTO.Sum);

            var result = payment.Verification(Authority).Result;

            if (result.Status == 100)
            {
                await _IOrderService.FinalizeOrder(orderDTO.Id);

              
                return View();
            }
            else
            {
                BadRequest();
            }



        }

        else
        {
            return BadRequest();
        }

        return BadRequest();

    }


    #endregion





    #region SetTempData
    private async Task SetTempData()
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

    }

    #endregion
}
