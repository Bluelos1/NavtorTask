using NavtorTask.Model.Enum;

namespace NavtorTask.Interface;

public interface ITankerShipService
{
    void RefuelTank(string imoNumber, int tankId, FuelType fuelType, double liters);
    void EmptyTank(string imoNumber, int tankId);
}