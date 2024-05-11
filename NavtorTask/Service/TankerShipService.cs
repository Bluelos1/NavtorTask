using NavtorTask.Interface;
using NavtorTask.Model;
using NavtorTask.Model.Enum;

namespace NavtorTask.Service;

public class TankerShipService : ITankerShipService
{
    private readonly IShipService _shipService;

    public TankerShipService(IShipService shipService)
    {
        _shipService = shipService;
    }    
    public void RefuelTank(string imoNumber, int tankId, FuelType fuelType, double liters)
    {
        if (_shipService.GetShipByIMONumber(imoNumber) is not TankerShip tankerShip)
        {
            throw new InvalidOperationException("Specified ship is not a tanker ship or does not exist.");
        }

        tankerShip.RefuelTank(tankId, fuelType, liters);
    }

    public void EmptyTank(string imoNumber, int tankId)
    {
        if (_shipService.GetShipByIMONumber(imoNumber) is not TankerShip tankerShip)
        {
            throw new InvalidOperationException("Specified ship is not a tanker ship or does not exist.");
        }

        tankerShip.EmptyTank(tankId);
    }
    
    
    
}