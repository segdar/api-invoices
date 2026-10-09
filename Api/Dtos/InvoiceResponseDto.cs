namespace ApiInvoices.Api.Dtos;

public class InvoiceResponseDto
{
    public int InvoiceHeaderId { get; set; }
    public int ClientId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public List<InvoiceDetailResponseDto> Details { get; set; } = new();
}
