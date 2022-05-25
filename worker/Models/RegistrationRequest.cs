namespace worker.Models;

public class RegistrationRequest
{
    public const string YourLocalIP = "144.17.24.145";
    public Guid WorkerId { get; set; } = Guid.NewGuid();
    public string TeamName { get; set; } = "{your name here}";
    public string CreateJobEndpoint { get; set; } = $"http://{YourLocalIP}:6010/CreateJob";
    public string ErrorCheckEndpoint { get; set; } = $"http://{YourLocalIP}:6010/ErrorCheck";
}