using System.Transactions;
using bpmn_dotnet_core6.DAL;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace bpmn_dotnet_core6.Domain;

public class CreateInvoice
{
    public class Command : IRequest<InvoiceId>
    {
        public OrderId OrderId { get; set; }
    }

    public class Handler : IRequestHandler<Command, InvoiceId>
    {
        private readonly HeroesDbContext db;

        public Handler(HeroesDbContext db)
        {
            this.db = db;
        }

        public async Task<InvoiceId> Handle(Command request, CancellationToken cancellationToken)
        {
            using var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            
            var order = await db.Orders.FirstAsync(o => o.Id == request.OrderId, cancellationToken);

            var invoice = Invoice.ForOrder(order);

            db.Invoices.Add(invoice);

            await db.SaveChangesAsync(cancellationToken);

            tx.Complete();
            
            return invoice.Id;
        }
    }
}