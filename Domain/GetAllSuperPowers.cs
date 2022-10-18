using bpmn_dotnet_core6.DAL;
using bpmn_dotnet_core6.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bpmn_dotnet_core6.Domain;

public class GetAllSuperPowers
{
    public class Query : IRequest<ICollection<SuperpowerDto>>
    {
            
    }
        
    public class Handler : IRequestHandler<Query,ICollection<SuperpowerDto>>
    {
        private readonly HeroesDbContext db;

        public Handler(HeroesDbContext db)
        {
            this.db = db;
        }


        public async Task<ICollection<SuperpowerDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var powers = await db.Superpowers
                .ToListAsync(cancellationToken: cancellationToken);

            return powers.Select(SuperpowerDto.FromEntity).ToList();
        }
    }
}