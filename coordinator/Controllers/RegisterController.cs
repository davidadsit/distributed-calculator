using coordinator.Application;
using coordinator.Models;
using Microsoft.AspNetCore.Mvc;

namespace coordinator.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IWorkerRegistry workerRegistry;

        public RegisterController(IWorkerRegistry workerRegistry)
        {
            this.workerRegistry = workerRegistry;
        }

        [HttpPost]
        public RegistrationResult Index([FromBody] RegistrationRequest registrationRequest)
        {
            var workerRegistrationResult = workerRegistry.RegisterWorker(new WorkerRegistration()
            {
                WorkerId = registrationRequest.WorkerId,
                TeamName = registrationRequest.TeamName,
                CreateJobEndpoint = registrationRequest.CreateJobEndpoint,
                ErrorCheckEndpoint = registrationRequest.ErrorCheckEndpoint
            });
            return new RegistrationResult
            {
                 Result = workerRegistrationResult.Result
            };
        }
    }
}