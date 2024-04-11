using Microsoft.AspNetCore.Mvc;

namespace NutsShop_Presentation.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class AdminController : Controller
{

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }















}
