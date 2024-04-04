﻿using Application.Dtos.UserLogInDTO;
using Application.Dtos.UserRegisterDTO;
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

    public UserController(IUserService userService,IOrderService orderService)
    {
        _IUserService = userService;
        _IOrderService = orderService;
    }

    #endregion


    #region Register


    [HttpGet]
    public async Task<IActionResult> Register()
    {

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


    #region LogOut

    public async Task<IActionResult> LogOut() 
    {

       await HttpContext.SignOutAsync();

        return RedirectToAction("Index", "Home");

    }

    #endregion


    public async Task<IActionResult> AddProductToCart(int ProductId) 
    {
        if (User.Identity.IsAuthenticated)
        {
            _IOrderService.AddProductToCart(Convert.ToInt32(ClaimTypes.NameIdentifier), ProductId);

            return RedirectToAction("Index", "Home");
        }

        else
        {
            //Go to LogIn Page

            return RedirectToAction(nameof(LogIn));
        }

    }
}
