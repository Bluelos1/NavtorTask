using System.Text.Json.Serialization;
using NavtorTask.Model.Enum;


namespace NavtorTask.Model;

public class Tank
{
    public int TankId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FuelType? FuelType { get; set; } 
    public double CapacityInLiters { get; set; }  
    public double CurrentLevelInLiters { get; set; } 
    
    private bool CanRefuel(FuelType fuelType)
    {
        if (CurrentLevelInLiters == 0)
        {
            return true;
        }
        return FuelType == fuelType;
    }
    
    public double CalculateFuelWeight(double liters, FuelType fuelType)
    {
        var density = fuelType == Enum.FuelType.Diesel ? 0.838 : 0.90;
        return liters * density;
    }

    public void Refuel(FuelType fuelType, double liters)
    {
        if (!CanRefuel(fuelType))
        {
            throw new InvalidOperationException($"Cannot refuel with {fuelType}. Tank is already filled with {FuelType}.");
        }
        if (CurrentLevelInLiters == 0)
        {
            FuelType = fuelType;
        }
        
        if (CurrentLevelInLiters + liters <= CapacityInLiters)
        {
            CurrentLevelInLiters += liters;
        }
        else
        {
            throw new InvalidOperationException("Cannot refuel: Exceeds tank capacity.");
        }
    }

    public void EmptyTank()
    {
        CurrentLevelInLiters = 0;
    }
}