using System;

namespace coordinator.Application
{
    public class JobResponse
    {
        public Guid JobId { get; set; }
        public string Result { get; set; }
    }
}