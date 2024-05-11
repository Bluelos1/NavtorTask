using NavtorTask.Model.Enum;

namespace NavtorTask.Model;

public class TankerShip : Ship
{
    public double MaxLoadWeight { get; set; } 
    public List<Tank> Tanks { get; set; } = new List<Tank>();
    
    
    public void RefuelTank(int tankId, FuelType fuelType, double liters)
    {
        var tank = Tanks.FirstOrDefault(t => t.TankId == tankId);
        if (tank == null)
        {
            throw new KeyNotFoundException("Tank not found");
        }
        var addedFuelWeight = tank.CalculateFuelWeight(liters, fuelType);

        var currentLoad = Tanks.Sum(t => t.CurrentLevelInLiters * t.CalculateFuelWeight(1, t.FuelType.Value));

        if (currentLoad + addedFuelWeight <= MaxLoadWeight)
        {
            tank.Refuel(fuelType, liters);
        }
        else
        {
            throw new InvalidOperationException("Cannot refuel: Overloaded tanker.");
        }
    }

    public void EmptyTank(int tankId)
    {
        var tank = Tanks.FirstOrDefault(t => t.TankId == tankId);
        if (tank != null)
        {
            tank.EmptyTank();
        }
        else
        {
            throw new KeyNotFoundException("Tank not found.");
        }
    }
}