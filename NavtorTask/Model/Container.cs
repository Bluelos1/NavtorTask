using System.ComponentModel.DataAnnotations;

namespace NavtorTask.Model;

public class Container
{
    [Key]
    public int ContainerId { get; set; }
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string CargoDescription { get; set; }
    public double Weight { get; set; }
    
    
}