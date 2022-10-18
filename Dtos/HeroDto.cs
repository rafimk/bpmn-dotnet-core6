using bpmn_dotnet_core6.Domain;

namespace bpmn_dotnet_core6.Dtos;

public class HeroDto
{
    public Guid HeroId { get; set; }
        
    public string Name { get; set; }

    public static HeroDto FromEntity(Hero hero)
    {
        return new HeroDto
        {
            HeroId = hero.Id.Value,
            Name = hero.Name
        };
    }
}