using NavtorTask.Model;
using NavtorTask.Validation;

namespace NavtorTask.Tests.ValidatorTests;

public class ValidationServiceTests
{
    private readonly ValidationService _validationService;

    public ValidationServiceTests()
    {
        _validationService = new ValidationService();
    }

    [Fact]
    public void ValidateShip_WithPositiveDimensions_DoesNotThrow()
    {
        // Arrange
        var ship = new Ship { Length = 10, Width = 5, IMONumber = "IMO9014729" };

        // Act & Assert
        var exception = Record.Exception(() => _validationService.ValidateShip(ship));
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateShip_WithNonPositiveDimensions_ThrowsArgumentException()
    {
        // Arrange
        var ship = new Ship { Length = 0, Width = -5, IMONumber = "IMO9014729" };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _validationService.ValidateShip(ship));
    }
    [Fact]
    public void IsValidImoNumber_CorrectCheckDigit_ReturnsTrue()
    {
        // Arrange
        var imoNumber = "IMO9014729";  

        // Act
        var result = _validationService.IsValidImoNumber(imoNumber);

        // Assert
        Assert.True(result);
    }
}