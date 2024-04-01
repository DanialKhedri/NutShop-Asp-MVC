using Application.Dtos.UserRegisterDTO;
using Microsoft.AspNetCore.Mvc;

namespace NutsShop_Presentation.Areas.SitePanel.Controllers;



[Area("SitePanel")]
public class UserController : Controller
{

    #region Ctor

    

    #endregion






    #region Register


    [HttpGet]
    public async Task<IActionResult> Register()
    {

        return View();

    }

    [HttpPost]
    public Task<IActionResult> Register(UserRegisterDTO userRegisterDTO)
    {


        return View();
    }
    #endregion

}
