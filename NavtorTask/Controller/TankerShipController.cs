using Microsoft.AspNetCore.Mvc;
using NavtorTask.Interface;
using NavtorTask.Model.Enum;

namespace NavtorTask.Controller;

[Route("api/[controller]")]
[ApiController]
public class TankerShipController : ControllerBase
{
    private readonly ITankerShipService _tankerShip;

    public TankerShipController(ITankerShipService tankerShip)
    {
        _tankerShip = tankerShip;
    }
    
    [HttpPut("{imoNumber}/tanks/{tankId}/refuel")]
    public IActionResult RefuelTank(string imoNumber, int tankId, [FromQuery]FuelType fuelType, double liters)
    {
        try
        {
            _tankerShip.RefuelTank(imoNumber, tankId, fuelType, liters);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{imoNumber}/tanks/{tankId}/empty")]
    public IActionResult EmptyTank(string imoNumber, int tankId)
    {
        try
        {
            _tankerShip.EmptyTank(imoNumber, tankId);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}