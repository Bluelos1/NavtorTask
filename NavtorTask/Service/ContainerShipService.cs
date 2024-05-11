using NavtorTask.Interface;
using NavtorTask.Model;

namespace NavtorTask.Service;

public class ContainerShipService : IContainerShipService
{
    private readonly IShipService _shipService;

    public ContainerShipService(IShipService shipService)
    {
        _shipService = shipService;
    }
    
    public void LoadContainer(string imoNumber, Container container)
    {
        if (_shipService.GetShipByIMONumber(imoNumber) is not ContainerShip containerShip)
        {
            throw new InvalidOperationException("Specified ship is not a container ship or does not exist.");
        }

        containerShip.LoadContainer(container);
    }

    public void UnloadContainer(string imoNumber, int containerId)
    {
        if (_shipService.GetShipByIMONumber(imoNumber) is not ContainerShip containerShip)
        {
            throw new InvalidOperationException("Specified ship is not a container ship or does not exist.");
        }

        containerShip.UnloadContainer(containerId);
    }
}