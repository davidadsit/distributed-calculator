using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using worker.Models;

namespace worker.Controllers
{
    public class CreateJobController : Controller
    {
        private readonly ILogger<CreateJobController> logger;

        public CreateJobController(ILogger<CreateJobController> logger)
        {
            this.logger = logger;
        }

        [HttpPost]
        public JobResult Index([FromBody] CreateJobRequest createJobRequest)
        {
            logger.LogDebug($"PROBLEM:  {JsonConvert.SerializeObject(createJobRequest)}");
            var jobResult = new JobResult
            {
                JobId = createJobRequest.JobId,
                Result = "7"
            };
            logger.LogDebug($"SOLUTION: {JsonConvert.SerializeObject(jobResult)}");
            return jobResult;
        }
    }
}