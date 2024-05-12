namespace NavtorTask.Model;

public class PositionHistory
{
    private double Latitude { get; set; }
    private double Longitude { get; set; }
    private DateTime Timestamp { get; set; }
    
    public PositionHistory(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        Timestamp = DateTime.UtcNow;
    }
}