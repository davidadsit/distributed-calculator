using System;

namespace coordinator.Application
{
    public class ErrorCheckMessage
    {
        public Guid JobId { get; set; }
        public string ErrorMessage { get; set; }
    }
}