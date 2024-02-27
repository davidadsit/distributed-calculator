using coordinator.Application;
using coordinator.Models;
using Microsoft.AspNetCore.Mvc;

namespace coordinator.Controllers;

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
        if (registrationRequest == null) return new RegistrationResult { Result = "Invalid registration request. The worker ID may not be a valid GUID." };
        try
        {
            new Uri(registrationRequest.CreateJobEndpoint);
            new Uri(registrationRequest.ErrorCheckEndpoint);
        }
        catch (Exception)
        {
            return new RegistrationResult { Result = "You must provide valid URIs for the CreateJob and ErrorCheck endpoints" };
        }
        var workerRegistrationResult = workerRegistry.RegisterWorker(new WorkerRegistration
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