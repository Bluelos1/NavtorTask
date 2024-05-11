using NavtorTask.Model;

namespace NavtorTask.Interface;

public interface IShipService
{ 
    void AddShip(Ship ship); 
    Ship GetShipByIMONumber(string imoNumber);
    void UpdatePosition(string imoNumber, double latitude, double longitude);
    IEnumerable<Ship> GetAllShips();
}