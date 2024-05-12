using Microsoft.AspNetCore.Mvc;
using Moq;
using NavtorTask.Controller;
using NavtorTask.Interface;
using NavtorTask.Model.Enum;

namespace NavtorTask.Tests.ControllerTests;

public class TankerShipControllerTests
{
    private readonly TankerShipController _controller;
    private readonly Mock<ITankerShipService> _mockService;

    public TankerShipControllerTests()
    {
        _mockService = new Mock<ITankerShipService>();
        _controller = new TankerShipController(_mockService.Object);
    }

    [Fact]
    public void RefuelTank_ValidRequest_ReturnsNoContent()
    {
        // Arrange
        var imoNumber = "IMO9014729";
        var tankId = 1;
        var liters = 100;
        _mockService.Setup(s => s.RefuelTank(imoNumber, tankId, FuelType.Diesel, liters)).Verifiable();

        // Act
        var result = _controller.RefuelTank(imoNumber, tankId, FuelType.Diesel, liters);

        // Assert
        var noContentResult = Assert.IsType<NoContentResult>(result);
        _mockService.Verify();
    }

    [Fact]
    public void RefuelTank_ThrowsException_ReturnsBadRequest()
    {
        // Arrange
        var imoNumber = "IMO9014729";
        var tankId = 1;
        var liters = 100;
        _mockService.Setup(s => s.RefuelTank(imoNumber, tankId, FuelType.Diesel, liters))
                    .Throws(new InvalidOperationException("Error message"));

        // Act
        var result = _controller.RefuelTank(imoNumber, tankId, FuelType.Diesel, liters);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error message", badRequestResult.Value);
    }

    [Fact]
    public void EmptyTank_ValidRequest_ReturnsNoContent()
    {
        // Arrange
        var imoNumber = "IMO9014729";
        var tankId = 1;
        _mockService.Setup(s => s.EmptyTank(imoNumber, tankId)).Verifiable();

        // Act
        var result = _controller.EmptyTank(imoNumber, tankId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockService.Verify();
    }

    [Fact]
    public void EmptyTank_ThrowsException_ReturnsBadRequest()
    {
        // Arrange
        var imoNumber = "IMO9014729";
        var tankId = 1;
        _mockService.Setup(s => s.EmptyTank(imoNumber, tankId))
                    .Throws(new InvalidOperationException("Error message"));

        // Act
        var result = _controller.EmptyTank(imoNumber, tankId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error message", badRequestResult.Value);
    }
}