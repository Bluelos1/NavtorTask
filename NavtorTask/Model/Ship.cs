namespace NavtorTask.Model;

public class Ship
{
    public string Name { get; set; }
    public string IMONumber { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public List<PositionHistory> PositionHistory { get; set; } = new List<PositionHistory>();
    
    public void UpdatePosition(double latitude, double longitude)
    {
        var position = new PositionHistory(latitude, longitude);
        PositionHistory.Add(position);
    }
}