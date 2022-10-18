using bpmn_dotnet_core6.Domain;

namespace bpmn_dotnet_core6.Dtos;

public class InvoiceDto
{
    public Guid InvoiceId { get; set; }
    public Guid OrderId { get; set; }
    public string CustomerCode { get; set; }
    public string CustomerName { get; set; }
    public string Title { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }

    public static InvoiceDto FromEntity(Invoice invoice)
    {
        return new InvoiceDto
        {
            InvoiceId = invoice.Id.Value,
            OrderId = invoice.Order.Id.Value,
            CustomerCode = invoice.Customer.Code,
            CustomerName = invoice.Customer.Name,
            Title = invoice.Title,
            Total = invoice.Amount,
            Status = invoice.Status.ToString()
        };
    }
}