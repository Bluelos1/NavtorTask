using Moq;
using NavtorTask.Model;
using NavtorTask.Service;
using NavtorTask.Validation;

namespace NavtorTask.Tests.ServiceTests;

public class ShipServiceTests
{
    private readonly ShipService _shipService;
    private readonly Mock<ValidationService> _mockValidationService;

    public ShipServiceTests()
    {
        _mockValidationService = new Mock<ValidationService>();
        _shipService = new ShipService(_mockValidationService.Object);
    }

    private readonly Ship _ship = new Ship { IMONumber = "IMO9014729", Length = 100, Width = 30  };


    [Fact]
    public void AddShip_DuplicateIMO_ThrowsException()
    {
        // Arrange
        _shipService.AddShip(_ship); 

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _shipService.AddShip(_ship)); 
    }

    [Fact]
    public void GetShipByIMONumber_ValidIMO_ReturnsShip()
    {
        // Arrange
        _shipService.AddShip(_ship);

        // Act
        var result = _shipService.GetShipByIMONumber("IMO9014729");

        // Assert
        Assert.Equal(_ship, result);
    }
}
