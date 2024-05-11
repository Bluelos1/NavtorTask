using Microsoft.AspNetCore.Mvc;
using NavtorTask.Interface;
using NavtorTask.Model;
using System.Linq;

namespace NavtorTask.Controller;

[Route("api/[controller]")]
[ApiController]
public class ShipController : ControllerBase
{
    private readonly IShipService _ship;

    public ShipController(IShipService shipService)
    {
        _ship = shipService;
    }
    
    [HttpGet]
    public IActionResult GetAllShips()
    {
        var ships = _ship.GetAllShips();
        return Ok(ships);
    }

    [HttpGet("{imoNumber}")]
    public IActionResult GetShipByImoNumber(string imoNumber)
    {
        try
        {
            var ship = _ship.GetShipByIMONumber(imoNumber);
            return Ok(ship);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("containers")]
    public IActionResult AddContainerShip([FromBody] ContainerShipDto containerShipDto)
    {
        try
        {
            var newContainerShip = new ContainerShip
            {
                Name = containerShipDto.Name,
                IMONumber = containerShipDto.IMONumber,
                Length = containerShipDto.Length,
                Width = containerShipDto.Width,
                MaxContainerCapacity = containerShipDto.MaxContainerCapacity,
                MaxLoadWeight = containerShipDto.MaxLoadWeight,
                Containers = new List<Container>()
            };
            _ship.AddShip(newContainerShip);
            return CreatedAtAction(nameof(GetShipByImoNumber), new { imoNumber = containerShipDto.IMONumber }, containerShipDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("tankers")]
    public IActionResult AddTankerShip([FromBody] TankerShipDto tankerShipDto)
    {
        try
        {
            var newTankerShip = new TankerShip
            {
                Name = tankerShipDto.Name,
                IMONumber = tankerShipDto.IMONumber,
                Length = tankerShipDto.Length,
                Width = tankerShipDto.Width,
                MaxLoadWeight = tankerShipDto.MaxLoadWeight,
                Tanks = tankerShipDto.Tanks
            };
            var tankIds = new HashSet<int>();

            foreach (var tank in newTankerShip.Tanks.Where(tank => !tankIds.Add(tank.TankId)))
            {
                return BadRequest($"Duplicate tank ID found: {tank.TankId}. Each tank must have a unique ID.");
            }
            foreach (var tank in newTankerShip.Tanks.Where(tank => tank.CurrentLevelInLiters > tank.CapacityInLiters))
            {
                return BadRequest($"Tank with ID {tank.TankId} has more fuel ({tank.CurrentLevelInLiters} liters) than its capacity ({tank.CapacityInLiters} liters).");
            }
            var totalFuelWeight = newTankerShip.Tanks.Sum(tank => tank.CalculateFuelWeight(tank.CurrentLevelInLiters, tank.FuelType.Value));
            if (totalFuelWeight > newTankerShip.MaxLoadWeight)
            {
                return BadRequest("Total fuel weight exceeds the ship's maximum load weight.");
            }
            _ship.AddShip(newTankerShip);
            return CreatedAtAction(nameof(GetShipByImoNumber), new { imoNumber = tankerShipDto.IMONumber }, tankerShipDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{imoNumber}/position")]
    public IActionResult UpdatePosition(string imoNumber, double latitude, double longitude)
    {
        try
        {
            _ship.UpdatePosition(imoNumber, latitude, longitude);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
}