namespace worker.Models;

public class ErrorCheckRequest
{
    public Guid JobId { get; set; }
    public string ErrorMessage { get; set; }
}