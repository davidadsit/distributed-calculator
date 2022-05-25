using coordinator.Application;
using coordinator.Models;
using Microsoft.AspNetCore.Mvc;

namespace coordinator.Controllers;

public class StatusController : Controller
{
    private readonly IWorkerRegistry workerRegistry;
    private readonly IJobAssignments jobAssignments;

    public StatusController(IWorkerRegistry workerRegistry, IJobAssignments jobAssignments)
    {
        this.workerRegistry = workerRegistry;
        this.jobAssignments = jobAssignments;
    }

    public ActionResult Index()
    {
        return View(new StatusResult
        {
            ActiveWorkers = workerRegistry.ActiveWorkers,
            InactiveWorkers = workerRegistry.InactiveWorkers,
            RecentJobs = jobAssignments.RecentAssignments
        });
    }
}