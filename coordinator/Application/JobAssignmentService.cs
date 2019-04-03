using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace coordinator.Application
{
    public class JobAssignmentService : IHostedService, IDisposable
    {
        private readonly IJobPool jobPool;
        private readonly IJobAssignments jobAssignments;
        private readonly ILogger logger;
        private readonly IWorkerRegistry workerRegistry;
        private Timer timer;

        public JobAssignmentService(ILogger<JobAssignmentService> logger, IWorkerRegistry workerRegistry, IJobPool jobPool, IJobAssignments jobAssignments)
        {
            this.logger = logger;
            this.workerRegistry = workerRegistry;
            this.jobPool = jobPool;
            this.jobAssignments = jobAssignments;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("JobAssignmentService is starting.");

            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("JobAssignmentService is stopping.");

            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            logger.LogInformation($"JobAssignmentService is working. {workerRegistry.ActiveWorkers.Count()} workers are registered");
            foreach (var worker in workerRegistry.ActiveWorkers)
            {
                SendJobToWorker(worker);
            }
        }

        private void SendJobToWorker(WorkerRegistration worker)
        {
            var client = new RestClient(worker.CreateJobEndpoint);
            var request = new RestRequest("", Method.POST);
            var job = jobPool.GetNextJob(worker.CorrectResponses);
            var jobRequest = new JobRequest
            {
                Calculation = job.Problem
            };
            request.AddJsonBody(jobRequest);

            try
            {
                var response = client.Execute(request);
                logger.LogInformation($"Raw response {response.Content}");
                var jobResponse = JsonConvert.DeserializeObject<JobResponse>(response.Content);
                logger.LogInformation($"Sent job {{{jobRequest.JobId}}} ({jobRequest.Calculation}) to {{{worker.WorkerId}}} ({worker.CreateJobEndpoint}) and got {jobResponse.Result} in response. Expected {job.Solution}.");
                jobAssignments.AssignJob(worker, job);

                worker.FailedResponses = 0;
                if (jobResponse.Result == job.Solution)
                {
                    worker.CorrectResponses++;
                }
                else
                {
                    worker.IncorrectResponses++;
                    SendErrorNotice(worker, jobRequest.JobId, "Incorrect solution provided");
                }
            }
            catch (Exception e)
            {
                worker.FailedResponses++;
                logger.LogInformation($"Failed to send job {{{jobRequest.JobId}}} ({jobRequest.Calculation}) to {{{worker.WorkerId}}} ({worker.CreateJobEndpoint}).");
                SendErrorNotice(worker, jobRequest.JobId, e.Message);
            }
        }

        private void SendErrorNotice(WorkerRegistration worker, Guid jobRequestJobId, string message)
        {
            var client = new RestClient(worker.ErrorCheckEndpoint);
            var request = new RestRequest("", Method.POST);
            request.AddJsonBody(new ErrorCheckMessage
            {
                JobId = jobRequestJobId,
                ErrorMessage = message
            });
            try
            {
                client.Execute(request);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Could not send error");
            }
        }
    }
}