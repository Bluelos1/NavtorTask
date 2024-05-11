using NavtorTask.Model;

namespace NavtorTask.Interface;

public interface IContainerShipService
{
    void LoadContainer(string imoNumber, Container container);
    void UnloadContainer(string imoNumber, int containerId);
}