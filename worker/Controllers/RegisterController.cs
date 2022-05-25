using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using worker.Models;

namespace worker.Controllers;

public class RegisterController : Controller
{
    public IActionResult Index(RegistrationRequest registrationRequest)
    {
        var client = new RestClient("http://144.17.10.32:5000");
        var request = new RestRequest("register", Method.Post);
        request.AddJsonBody(registrationRequest);

        try
        {
            var response = client.ExecuteAsync(request).Result;
            var registrationResult = JsonConvert.DeserializeObject<RegistrationResult>(response.Content);

            return View(new RegisterResult
            {
                Message = response.Content //registrationResult.Result
            });
        }
        catch (Exception e)
        {
            return View(new RegisterResult
            {
                Message = $"Registration failed with this exception: {e.Message}"
            });
        }
    }
}