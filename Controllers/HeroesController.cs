using Microsoft.AspNetCore.Mvc;
using bpmn_dotnet_core6.Dtos;
using bpmn_dotnet_core6.Domain;
using MediatR;

namespace bpmn_dotnet_core6.Controllers;

[ApiController]
[Route("[controller]")]
public class HeroesController : ControllerBase
{
    private readonly IMediator bus;

    public HeroesController(IMediator bus)
    {
        this.bus = bus;
    }

    [HttpGet("AvailableForOrder/{orderId}")]
    public async Task<ICollection<HeroDto>> FindAvailableHeroes([FromRoute] Guid orderId)
    {
        return await bus.Send(new FindHeroesForOrder.Query
        {
            OrderId = new OrderId(orderId)
        });
    }
}