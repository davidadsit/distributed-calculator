using System;

namespace coordinator.Application
{
    public class JobRequest
    {
        public Guid JobId { get; } = Guid.NewGuid();
        public string Calculation { get; set; }
    }
}