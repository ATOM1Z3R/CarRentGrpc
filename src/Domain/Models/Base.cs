namespace Domain.Models;

public abstract class Base
{
    public int Id { get; set; }

    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime Deleted { get; set; }
}
