namespace NavtorTask.Controller;

public class ContainerShipDto
{
    public string Name { get; set; }
    public string IMONumber { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public int MaxContainerCapacity { get; set; }
    public double MaxLoadWeight { get; set; }
}