using Microsoft.AspNetCore.Mvc;

namespace webmvcclientuyg.Controllers
{
    public class StartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
