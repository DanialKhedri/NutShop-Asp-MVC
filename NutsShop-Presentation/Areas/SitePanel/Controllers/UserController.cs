using Application.Dtos.UserRegisterDTO;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NutsShop_Presentation.Areas.SitePanel.Controllers;



[Area("SitePanel")]
public class UserController : Controller
{

    #region Ctor

    private readonly IUserService _IUserService;

    public UserController(IUserService userService) 
    {
        _IUserService = userService;
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
                    return RedirectToAction("Index","Home");

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



}
