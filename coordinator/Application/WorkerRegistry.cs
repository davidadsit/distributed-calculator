using System;
using System.Collections.Generic;

namespace coordinator.Application
{
    public interface IWorkerRegistry
    {
        IEnumerable<WorkerRegistration> ActiveWorkers { get; }
        WorkerRegistrationResult RegisterWorker(WorkerRegistration workerRegistration);
        void DeregisterWorker(Guid workerId);
    }

    public class WorkerRegistry : IWorkerRegistry
    {
        private static readonly Dictionary<Guid, WorkerRegistration> workers = new Dictionary<Guid, WorkerRegistration>();

        public WorkerRegistrationResult RegisterWorker(WorkerRegistration workerRegistration)
        {
            if (workers.ContainsKey(workerRegistration.WorkerId))
            {
                workers[workerRegistration.WorkerId] = workerRegistration;
                return new WorkerRegistrationResult
                {
                    Result =
                        $"'{workerRegistration.WorkerId}' developed by '{workerRegistration.TeamName}' was re-registered and is ready to accept jobs on '{workerRegistration.CreateJobEndpoint}' with error checking reported on '{workerRegistration.ErrorCheckEndpoint}'"
                };
            }

            workers.Add(workerRegistration.WorkerId, workerRegistration);
            return new WorkerRegistrationResult
            {
                Result =
                    $"'{workerRegistration.WorkerId}' developed by '{workerRegistration.TeamName}' is ready to accept jobs on '{workerRegistration.CreateJobEndpoint}' with error checking reported on '{workerRegistration.ErrorCheckEndpoint}'"
            };
        }

        public void DeregisterWorker(Guid workerId)
        {
            if (workers.ContainsKey(workerId)) workers.Remove(workerId);
        }

        public IEnumerable<WorkerRegistration> ActiveWorkers => workers.Values;
    }
}