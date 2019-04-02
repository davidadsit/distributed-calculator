using coordinator.Application;
using coordinator.Models;
using Microsoft.AspNetCore.Mvc;

namespace coordinator.Controllers
{
    public class StatusController : Controller
    {
        private readonly IWorkerRegistry workerRegistry;

        public StatusController(IWorkerRegistry workerRegistry)
        {
            this.workerRegistry = workerRegistry;
        }

        public ActionResult Index()
        {
            return View(new StatusResult
            {
                ActiveWorkers = workerRegistry.ActiveWorkers,
                InactiveWorkers = workerRegistry.InactiveWorkers
            });
        }
    }
}