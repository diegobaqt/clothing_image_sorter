using Microsoft.AspNetCore.Mvc;

namespace SorterServices.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
