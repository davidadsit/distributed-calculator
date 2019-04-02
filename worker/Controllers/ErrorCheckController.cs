using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using worker.Models;

namespace worker.Controllers
{
    public class ErrorCheckController : Controller
    {
        private readonly ILogger<ErrorCheckController> logger;

        public ErrorCheckController(ILogger<ErrorCheckController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Index([FromBody] ErrorCheckRequest errorCheckRequest)
        {
            logger.LogDebug($"FAILED TO SOLVE: {JsonConvert.SerializeObject(errorCheckRequest)}");
            return Ok();
        }
    }
}