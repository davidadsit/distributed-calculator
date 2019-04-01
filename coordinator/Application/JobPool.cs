namespace coordinator.Application
{
    public interface IJobPool
    {
        Job GetNextJob(int previousCorrectResponses);
    }

    public class JobPool : IJobPool
    {
        public Job GetNextJob(int previousCorrectResponses)
        {
            return new Job
            {
                Problem = "CALCULATE: 3 + 4",
                Solution = "7"
            };
        }
    }

    public class Job
    {
        public string Problem { get; set; }
        public string Solution { get; set; }
    }
}