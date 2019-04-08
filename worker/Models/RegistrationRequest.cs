using System;

namespace worker.Models
{
    public class RegistrationRequest
    {
        public Guid WorkerId { get; set; }
        public string TeamName { get; set; }
        public string CreateJobEndpoint { get; set; } = "http://144.17.10.32:6010/CreateJob";
        public string ErrorCheckEndpoint { get; set; } = "http://144.17.10.32:6010/ErrorCheck";
    }
}