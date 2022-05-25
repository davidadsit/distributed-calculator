namespace coordinator.Application;

public interface IJobAssignments
{
    void AssignJob(WorkerRegistration workerRegistration, Job job);
    IEnumerable<Assignment> RecentAssignments { get; }
}

public class JobAssignments : IJobAssignments
{
    private static readonly List<Assignment> assignments = new List<Assignment>();

    public void AssignJob(WorkerRegistration workerRegistration, Job job)
    {
        assignments.Add(new Assignment
        {
            WorkerId = workerRegistration.WorkerId,
            TeamName = workerRegistration.TeamName,
            Problem = job.Problem,
            AssignedAt = DateTimeOffset.Now
        });
    }

    public IEnumerable<Assignment> RecentAssignments => assignments.OrderByDescending(x => x.AssignedAt).Take(35).ToArray();
}

public class Assignment
{
    public string Problem { get; set; }
    public Guid WorkerId { get; set; }
    public string TeamName { get; set; }
    public DateTimeOffset AssignedAt { get; set; }
}