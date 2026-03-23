namespace Shared;

public class Ticket
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; }
    public string Description { get; set; }
    public string CustomerEmail { get; set; }
    public string Priority { get; set; } = "Low";
}
