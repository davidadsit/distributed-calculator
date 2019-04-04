using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using worker.Models;

namespace worker.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index(RegistrationRequest registrationRequest)
        {
            var client = new RestClient("http://172.16.5.8:6005");
            var request = new RestRequest("register", Method.POST);
            request.AddJsonBody(registrationRequest);

            try
            {
                var response = client.Execute(request);
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
}