using System;

namespace coordinator.Models
{
    public class RegistrationRequest
    {
        public Guid WorkerId { get; set; }
        public string TeamName { get; set; }
        public string CreateJobEndpoint { get; set; }
        public string ErrorCheckEndpoint { get; set; }
    }
}