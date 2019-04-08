using System;

namespace worker.Models
{
    public class RegistrationRequest
    {
        public const string YourLocalIP = "144.17.24.145";
        public Guid WorkerId { get; set; }
        public string TeamName { get; set; }
        public string CreateJobEndpoint { get; set; } = $"{YourLocalIP}/CreateJob";
        public string ErrorCheckEndpoint { get; set; } = $"{YourLocalIP}/ErrorCheck";
    }
}