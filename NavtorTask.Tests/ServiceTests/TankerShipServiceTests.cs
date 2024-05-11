using Moq;
using NavtorTask.Interface;
using NavtorTask.Model;
using NavtorTask.Model.Enum;
using NavtorTask.Service;

namespace NavtorTask.Tests.ServiceTests;

public class TankerShipServiceTests
{
    private readonly Mock<IShipService> _mockShipService;
    private readonly TankerShipService _tankerShipService;

    public TankerShipServiceTests()
    {
        _mockShipService = new Mock<IShipService>();
        _tankerShipService = new TankerShipService(_mockShipService.Object);
    }
    private readonly TankerShip _tankerShip = new TankerShip { IMONumber = "IMO9014729", Length = 100, Width = 30,MaxLoadWeight = 100};

    [Fact]
    public void RefuelTank_ValidShip_RefuelsTank()
    {
        // Arrange
        _tankerShip.Tanks.Add(new Tank { TankId = 1, CapacityInLiters = 100 ,CurrentLevelInLiters = 20,FuelType = FuelType.Diesel});
        _mockShipService.Setup(s => s.GetShipByIMONumber(It.IsAny<string>())).Returns(_tankerShip);

        // Act
        _tankerShipService.RefuelTank("IMO9014729", 1, FuelType.Diesel, 50);

        // Assert
        var tank = _tankerShip.Tanks.First();
        Assert.Equal(70, tank.CurrentLevelInLiters);
    }

    [Fact]
    public void EmptyTank_NonExistentShip_ThrowsException()
    {
        // Arrange
        
        _mockShipService.Setup(s => s.GetShipByIMONumber("IMO9014729")).Returns((Ship)null);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _tankerShipService.EmptyTank("IMO9014729", 1));
    }
}
