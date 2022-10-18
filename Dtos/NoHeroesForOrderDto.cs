using FluentValidation;

namespace bpmn_dotnet_core6.Dtos;

public class NoHeroesForOrderDto
{
    public string TaskId { get; set; }
    public Guid OrderId { get; set; }
}
    
public class NoHeroesForOrderDtoValidator : AbstractValidator<NoHeroesForOrderDto>
{
    public NoHeroesForOrderDtoValidator()
    {
        RuleFor(x => x.TaskId).NotEmpty();
        RuleFor(x => x.OrderId).NotEmpty();
    }
}