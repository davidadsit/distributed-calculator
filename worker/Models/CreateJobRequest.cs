namespace worker.Models;

public class CreateJobRequest
{
    public Guid JobId { get; set; }
    public string Calculation { get; set; }
}