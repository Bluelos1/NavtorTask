namespace NavtorTask.Model;

public class PositionHistory
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime Timestamp { get; set; }
    
    public PositionHistory(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        Timestamp = DateTime.UtcNow;
    }
}