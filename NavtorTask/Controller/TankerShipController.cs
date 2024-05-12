using Microsoft.AspNetCore.Mvc;
using NavtorTask.Interface;
using NavtorTask.Model.Enum;

namespace NavtorTask.Controller;

[Route("api/[controller]")]
[ApiController]
public class TankerShipController : ControllerBase
{
    private readonly ITankerShipService _tankerShipService;

    public TankerShipController(ITankerShipService tankerShipService)
    {
        _tankerShipService = tankerShipService;
    }
    
    [HttpPut("{imoNumber}/tanks/{tankId}/refuel")]
    public IActionResult RefuelTank(string imoNumber, int tankId, [FromQuery]FuelType fuelType, double liters)
    {
        try
        {
            _tankerShipService.RefuelTank(imoNumber, tankId, fuelType, liters);
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
            _tankerShipService.EmptyTank(imoNumber, tankId);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}