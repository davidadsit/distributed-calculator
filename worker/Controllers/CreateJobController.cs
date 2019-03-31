using Microsoft.AspNetCore.Mvc;
using worker.Models;

namespace worker.Controllers
{
    public class CreateJobController : Controller
    {
        [HttpPost]
        public JobResult Index(CreateJobRequest createJobRequest)
        {
            return new JobResult
            {
                JobId = createJobRequest.JobId,
                Result = createJobRequest.Calculation
            };
        }
    }
}