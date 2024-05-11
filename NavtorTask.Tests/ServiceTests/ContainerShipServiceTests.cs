using Moq;
using NavtorTask.Interface;
using NavtorTask.Model;
using NavtorTask.Service;

namespace NavtorTask.Tests.ServiceTests;

public class ContainerShipServiceTests
{
    private readonly Mock<IShipService> _mockShipService;
    private readonly ContainerShipService _containerShipService;

    public ContainerShipServiceTests()
    {
        _mockShipService = new Mock<IShipService>();
        _containerShipService = new ContainerShipService(_mockShipService.Object);
    }

    private  readonly Container _container = new Container { ContainerId = 1, Weight = 100, };
    private readonly ContainerShip _containerShip = new ContainerShip { IMONumber = "IMO9014729", Length = 100, Width = 30,MaxContainerCapacity = 1, MaxLoadWeight = 150};


    [Fact]
    public void LoadContainer_ValidShip_InvokesLoadContainer()
    {
        // Arrange
        
        _mockShipService.Setup(s => s.GetShipByIMONumber("IMO9014729")).Returns(_containerShip);

        // Act
        _containerShipService.LoadContainer("IMO9014729", _container);

        // Assert
        Assert.Contains(_container, _containerShip.Containers);
    }

    [Fact]
    public void UnloadContainer_InvalidShip_ThrowsException()
    {
        // Arrange
        _mockShipService.Setup(s => s.GetShipByIMONumber("IMO9014729")).Returns((Ship)null);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => 
            _containerShipService.UnloadContainer("IMO9014729", 1));

        Assert.Equal("Specified ship is not a container ship or does not exist.", exception.Message);
    }
}
