using NavtorTask.Model;

namespace NavtorTask.Validation;

public class ValidationService
{
    public void ValidateShip(Ship ship)
    {
        if (ship.Length <= 0 || ship.Width <= 0)
        {
            throw new ArgumentException("Ship length and width must be positive values.");
        }

        if (!IsValidImoNumber(ship.IMONumber))
        {
            throw new ArgumentException("Invalid IMO number.");
        }
    }

    public void ValidateCoordinates(double latitude, double longitude)
    {
        if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
        {
            throw new ArgumentOutOfRangeException("Invalid coordinates provided.");
        }
    }

    public bool IsValidImoNumber(string imoNumber)
    {
        if (!imoNumber.StartsWith("IMO") || imoNumber.Length != 10 || !imoNumber.Skip(3).All(char.IsDigit))
        {
            return false;
        }

        int sum = 0;
        int[] weights = { 7, 6, 5, 4, 3, 2 };

        for (int i = 0; i < 6; i++)
        {
            int digit = imoNumber[i + 3] - '0';
            sum += digit * weights[i];
        }

        int checkDigit = sum % 10;
        int actualCheckDigit = imoNumber[imoNumber.Length - 1] - '0';

        return checkDigit == actualCheckDigit;
    }
}