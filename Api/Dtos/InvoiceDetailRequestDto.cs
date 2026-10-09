namespace ApiInvoices.Api.Dtos;

public class InvoiceDetailRequestDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
