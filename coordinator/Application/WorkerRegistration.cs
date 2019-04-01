using System;

namespace coordinator.Application
{
    public class WorkerRegistration
    {
        public Guid WorkerId { get; set; }
        public string TeamName { get; set; }
        public string CreateJobEndpoint { get; set; }
        public string ErrorCheckEndpoint { get; set; }
        public int CorrectResponses { get; set; } = 0;
        public int IncorrectResponses { get; set; } = 0;
        public int FailedResponses { get; set; }
    }
}