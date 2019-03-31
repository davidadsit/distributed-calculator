using System;

namespace worker.Models
{
    public class JobResult
    {
        public Guid JobId { get; set; }
        public string Result { get; set; }
    }
}