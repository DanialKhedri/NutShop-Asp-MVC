using Microsoft.AspNetCore.Mvc;

namespace NutsShop_Presentation.Areas.SitePanel.Controllers
{
    [Area("SitePanel")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       

    }
}
