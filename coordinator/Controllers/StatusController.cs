using Microsoft.AspNetCore.Mvc;

namespace coordinator.Controllers
{
    public class StatusController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}