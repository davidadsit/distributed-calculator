using Microsoft.AspNetCore.Mvc;
using worker.Models;

namespace worker.Controllers
{
    public class ErrorCheckController : Controller
    {
        [HttpPost]
        public IActionResult Index(ErrorCheckRequest createJobRequest)
        {
            return Ok();
        }
    }
}