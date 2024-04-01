using Microsoft.AspNetCore.Mvc;

namespace NutsShop_Presentation.Areas.SitePanel.Controllers;



[Area("SitePanel")]
public class UserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }



    #region Register


    [HttpGet]
    public async Task<IActionResult> Register()
    {

        return View();

    }

    //[HttpPost]
    //public Task<IActionResult> Register() 
    //{

    //}
    #endregion

}
