using Microsoft.AspNetCore.Mvc;
using NavtorTask.Interface;
using NavtorTask.Model;

namespace NavtorTask.Controller;

[Route("api/[controller]")]
[ApiController]
public class ContainerShipController : ControllerBase
{
    private readonly IContainerShipService _containerShip;

    public ContainerShipController(IContainerShipService containerShip)
    {
        _containerShip = containerShip;
    }
    
    [HttpPost("{imoNumber}/containers")]
    public IActionResult LoadContainer(string imoNumber, [FromBody] Container container)
    {
        try
        {
            _containerShip.LoadContainer(imoNumber, container);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{imoNumber}/containers/{containerId}")]
    public IActionResult UnloadContainer(string imoNumber, int containerId)
    {
        try
        {
            _containerShip.UnloadContainer(imoNumber, containerId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

