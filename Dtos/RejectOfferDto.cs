using FluentValidation;

namespace bpmn_dotnet_core6.Dtos;

public class RejectOfferDto
{
    public string TaskId { get; set; }
        
    public Guid OrderId { get; set; }
}
    
public class RejectOfferDtoValidator : AbstractValidator<RejectOfferDto>
{
    public RejectOfferDtoValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty();
        RuleFor(x => x.OrderId).NotEmpty();
    }
}