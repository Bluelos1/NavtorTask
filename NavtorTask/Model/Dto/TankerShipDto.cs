using NavtorTask.Model;

namespace NavtorTask.Controller;

public class TankerShipDto
{
    public string Name { get; set; }
    public string IMONumber { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double MaxLoadWeight { get; set; } 
    public List<Tank> Tanks { get; set; } = new List<Tank>();
}