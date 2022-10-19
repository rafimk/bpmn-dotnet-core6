using Microsoft.AspNetCore.Mvc;
using bpmn_dotnet_core6.Dtos;
using bpmn_dotnet_core6.Domain;
using MediatR;

namespace bpmn_dotnet_core6.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator bus;

    public OrderController(IMediator bus)
    {
        this.bus = bus;
    }

    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto request)
    {
        await bus.Send(new PlaceOrder.Command
        {
            CustomerCode = User.CompanyCode(),
            SuperpowerCode = request.SuperpowerCode,
            OrderFrom = request.OrderFrom,
            OrderTo = request.OrderTo
        });
        
        return Ok();
    }

    [HttpGet("{orderId}")]
    public async Task<OrderDto> GetOrderDetails([FromRoute] Guid orderId)
    {
        return await bus.Send(new GetOrderDetails.Query
        {
            OrderId = new OrderId(orderId)
        });
    }

    [HttpPost("CreateOffer")]
    public async Task<IActionResult> CreateOffer([FromBody] CreateOfferDto request)
    {
        await bus.Send(new CreateOffer.Command
        {
            TaskId = request.TaskId,
            OrderId = new OrderId(request.OrderId),
            SelectedHero = new HeroId(request.SelectedHero)
        });
        
        return Ok();
    }
    
    [HttpPost("NoHeroesAvailable")]
    public async Task<IActionResult> NoHeroesAvailable([FromBody] NoHeroesForOrderDto request)
    {
        await bus.Send(new RejectForNoHerosAvailableForOrder.Command
        {
            TaskId = request.TaskId,
            OrderId = new OrderId(request.OrderId)
        });
        
        return Ok();
    }
    
    [HttpPost("AcceptOffer")]
    public async Task<IActionResult> AcceptOffer([FromBody] AcceptOfferDto request)
    {
        await bus.Send(new AcceptOffer.Command
        {
            TaskId = request.TaskId,
            OrderId = new OrderId(request.OrderId)
        });
        
        return Ok();
    }
    
    [HttpPost("RejectOffer")]
    public async Task<IActionResult> RejectOffer([FromBody] RejectOfferDto request)
    {
        await bus.Send(new RejectOffer.Command
        {
            TaskId = request.TaskId,
            OrderId = new OrderId(request.OrderId)
        });
        
        return Ok();
    }

    [HttpPost("MarkInvoicePaid")]
    public async Task<IActionResult> MarkInvoicePaid([FromBody] MarkInvoicePaidDto request)
    {
        await bus.Send(new MarkOrderPaid.Command
        {
            InvoiceId = new InvoiceId(request.InvoiceId)
        });
        
        return Ok();
    }
}