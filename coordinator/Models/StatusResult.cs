﻿using coordinator.Application;

namespace coordinator.Models;

public class StatusResult
{
    public IEnumerable<WorkerRegistration> ActiveWorkers { get; set; }
    public IEnumerable<WorkerRegistration> InactiveWorkers { get; set; }
    public IEnumerable<Assignment> RecentJobs { get; set; }
}