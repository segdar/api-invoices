namespace ApiInvoices.Api.Dtos;

public class InvoiceCreateRequestDto
{
    public int ClientId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public string Status { get; set; } = "saved";
    public List<InvoiceDetailRequestDto> Details { get; set; } = new();
}
