using Microsoft.AspNetCore.Mvc;
using bpmn_dotnet_core6.Dtos;
using bpmn_dotnet_core6.Domain;
using MediatR;

namespace bpmn_dotnet_core6.Controllers;

[ApiController]
[Route("[controller]")]
public class SuperpowerController  : ControllerBase
{
    private readonly IMediator bus;

    public SuperpowerController(IMediator bus)
    {
        this.bus = bus;
    }

    [HttpGet]
    public async Task<ICollection<SuperpowerDto>> AllSuperpowers()
    {
        return await bus.Send(new GetAllSuperPowers.Query());
    }
}
