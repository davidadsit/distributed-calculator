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
            decimal result = 0;
            var parts = createJobRequest.Calculation.Split(' ');
            switch (parts[2])
            {
                case "+":
                    result = decimal.Parse(parts[1]) + decimal.Parse(parts[3]);
                    break;
                case "-":
                    result = decimal.Parse(parts[1]) - decimal.Parse(parts[3]);
                    break;
                case "/":
                    result = decimal.Parse(parts[1]) / decimal.Parse(parts[3]);
                    break;
                case "*":
                    result = decimal.Parse(parts[1]) * decimal.Parse(parts[3]);
                    break;
            }

            var jobResult = new JobResult
            {
                JobId = createJobRequest.JobId,
                Result = $"{result:0.###}"
            };
            logger.LogDebug($"SOLUTION: {JsonConvert.SerializeObject(jobResult)}");
            return jobResult;
        }
    }
}