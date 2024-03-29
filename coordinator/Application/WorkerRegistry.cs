﻿using System.Collections.Concurrent;
using System.Reactive.Linq;

namespace coordinator.Application;

public interface IWorkerRegistry
{
    WorkerRegistrationResult RegisterWorker(WorkerRegistration workerRegistration);
    void DeregisterWorker(Guid workerId);
    IEnumerable<WorkerRegistration> ActiveWorkers { get; }
    IEnumerable<WorkerRegistration> InactiveWorkers { get; }
    IObservable<IEnumerable<WorkerRegistration>> GetActiveWorkers();
    IObservable<IEnumerable<WorkerRegistration>> GetAInactiveWorkers();
}

public class WorkerRegistry : IWorkerRegistry
{
    private static readonly ConcurrentDictionary<Guid, WorkerRegistration> workers = new ConcurrentDictionary<Guid, WorkerRegistration>();

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

        workers.TryAdd(workerRegistration.WorkerId, workerRegistration);
        return new WorkerRegistrationResult
        {
            Result =
                $"'{workerRegistration.WorkerId}' developed by '{workerRegistration.TeamName}' is ready to accept jobs on '{workerRegistration.CreateJobEndpoint}' with error checking reported on '{workerRegistration.ErrorCheckEndpoint}'"
        };
    }

    public void DeregisterWorker(Guid workerId)
    {
        WorkerRegistration w;
        if (workers.ContainsKey(workerId)) workers.TryRemove(workerId, out w);
    }

    public IEnumerable<WorkerRegistration> ActiveWorkers => workers.Values.Where(x => x.FailedResponses < 4);
    public IEnumerable<WorkerRegistration> InactiveWorkers => workers.Values.Where(x => x.FailedResponses > 3);

    public IObservable<IEnumerable<WorkerRegistration>> GetActiveWorkers() => Observable.Return(ActiveWorkers);
    public IObservable<IEnumerable<WorkerRegistration>> GetAInactiveWorkers() => Observable.Return(InactiveWorkers);
}