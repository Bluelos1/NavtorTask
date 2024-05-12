using Microsoft.AspNetCore.Mvc;
using Moq;
using NavtorTask.Controller;
using NavtorTask.Interface;
using NavtorTask.Model;
using NavtorTask.Model.Enum;

namespace NavtorTask.Tests.ControllerTests;

public class ShipControllerTests
{
    private readonly ShipController _controller;
    private readonly Mock<IShipService> _mockShipService;

    public ShipControllerTests()
    {
        _mockShipService = new Mock<IShipService>();
        _controller = new ShipController(_mockShipService.Object);
    }
    
    [Fact]
    public void GetAllShips_ReturnsAllShips()
    {
        // Arrange
        var mockShips = new List<Ship> { new Ship { IMONumber = "IMO9014729" }, new Ship { IMONumber = "IMO9015729" } };
        _mockShipService.Setup(service => service.GetAllShips()).Returns(mockShips);

        // Act
        var result = _controller.GetAllShips() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var ships = result.Value as List<Ship>;
        Assert.Equal(2, ships.Count);
        Assert.Equal("IMO9014729", ships[0].IMONumber);
    }
    
    [Fact]
    public void GetShipByImoNumber_ValidImoNumber_ReturnsShip()
    {
        // Arrange
        var ship = new Ship { IMONumber = "IMO9014729" };
        _mockShipService.Setup(service => service.GetShipByIMONumber("IMO9014729")).Returns(ship);

        // Act
        var result = _controller.GetShipByImoNumber("IMO9014729") as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        var returnedShip = result.Value as Ship;
        Assert.Equal("IMO9014729", returnedShip.IMONumber);
    }
    
    [Fact]
    public void AddContainerShip_ValidData_ReturnsCreatedAtAction()
    {
        // Arrange
        var containerShipDto = new ContainerShipDto { IMONumber = "IMO9014729", Name = "New Ship" };
        _mockShipService.Setup(service => service.AddShip(It.IsAny<ContainerShip>()));

        // Act
        var result = _controller.AddContainerShip(containerShipDto) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal("GetShipByImoNumber", result.ActionName);
        Assert.Equal(containerShipDto.IMONumber, result.RouteValues["imoNumber"]);
    }
    
    [Fact]
    public void AddTankerShip_DuplicateTankId_ReturnsBadRequest()
    {
        // Arrange
        var tankerShipDto = new TankerShipDto
        {
            IMONumber = "IMO9014729",
            Tanks = new List<Tank>
            {
                new Tank { TankId = 1, FuelType = FuelType.Diesel },
                new Tank { TankId = 1, FuelType = FuelType.HeavyFuel }
            }
        };
        _mockShipService.Setup(service => service.AddShip(It.IsAny<TankerShip>()));

        // Act
        var result = _controller.AddTankerShip(tankerShipDto) as BadRequestObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Contains("Duplicate tank ID found", result.Value.ToString());
    }
}