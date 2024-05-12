using Microsoft.AspNetCore.Mvc;
using Moq;
using NavtorTask.Controller;
using NavtorTask.Interface;
using NavtorTask.Model;

namespace NavtorTask.Tests.ControllerTests;

public class ContainerShipControllerTests
{
    private readonly ContainerShipController _controller;
    private readonly Mock<IContainerShipService> _mockService = new Mock<IContainerShipService>();

    public ContainerShipControllerTests()
    {
        _controller = new ContainerShipController(_mockService.Object);
    }

    [Fact]
    public void LoadContainer_ValidData_ReturnsNoContent()
    {
        // Arrange
        var container = new Container { ContainerId = 1, CargoDescription = "Test Container" };
        var imoNumber = "IMO9014729";

        // Act
        var result = _controller.LoadContainer(imoNumber, container);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockService.Verify(s => s.LoadContainer(imoNumber, container), Times.Once);
    }

    [Fact]
    public void LoadContainer_ServiceThrowsException_ReturnsBadRequest()
    {
        // Arrange
        var container = new Container { ContainerId = 1, CargoDescription = "Test Container" };
        var imoNumber = "IMO9014729";
        _mockService.Setup(s => s.LoadContainer(imoNumber, container)).Throws(new InvalidOperationException("Test exception"));

        // Act
        var result = _controller.LoadContainer(imoNumber, container);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Test exception", badRequestResult.Value);
    }
    
    [Fact]
    public void UnloadContainer_ValidData_ReturnsNoContent()
    {
        // Arrange
        var imoNumber = "IMO9014729";
        var containerId = 1;

        // Act
        var result = _controller.UnloadContainer(imoNumber, containerId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockService.Verify(s => s.UnloadContainer(imoNumber, containerId), Times.Once);
    }

    [Fact]
    public void UnloadContainer_ServiceThrowsException_ReturnsBadRequest()
    {
        // Arrange
        var imoNumber = "IMO9014729";
        var containerId = 1;
        _mockService.Setup(s => s.UnloadContainer(imoNumber, containerId)).Throws(new Exception("Error unloading container"));

        // Act
        var result = _controller.UnloadContainer(imoNumber, containerId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error unloading container", badRequestResult.Value);
    }
}