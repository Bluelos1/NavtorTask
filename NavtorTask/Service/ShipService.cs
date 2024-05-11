using NavtorTask.Interface;
using NavtorTask.Model;
using NavtorTask.Validation;

namespace NavtorTask.Service;

public class ShipService : IShipService
{
    private readonly List<Ship> _ships = new List<Ship>();
    private readonly ValidationService _validationService;
    public ShipService(ValidationService validationService)
    {
        _validationService = validationService;
    }

    public IEnumerable<Ship> GetAllShips()
    {
        Console.WriteLine($"Number of ships: {_ships.Count}");
        return _ships;
    }

    public void AddShip(Ship ship)
    {
        if (_ships.Any(s => s.IMONumber == ship.IMONumber))
        {
            throw new InvalidOperationException("A ship with this IMO number already exists in the fleet.");
        }

        _validationService.ValidateShip(ship);
        _ships.Add(ship);
    }

    public Ship GetShipByIMONumber(string imoNumber)
    {
        var ship = _ships.FirstOrDefault(s => s.IMONumber == imoNumber);
        
        if (ship == null)
        {
            throw new KeyNotFoundException($"No ship found with IMO number {imoNumber}.");
        }

        return ship;
    }

    public void UpdatePosition(string imoNumber, double latitude, double longitude)
    {
        var ship = GetShipByIMONumber(imoNumber);
        if (ship == null)
        {
            throw new KeyNotFoundException($"No ship found with IMO number {imoNumber}.");
        }

        _validationService.ValidateCoordinates(latitude, longitude);
        ship.UpdatePosition(latitude, longitude);
    }
}

