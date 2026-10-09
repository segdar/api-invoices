namespace ApiInvoices.Domain.Entities;

public class InvoiceDetail
{
    public int InvoiceDetailId { get; set; }
    public int InvoiceHeaderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Subtotal { get; set; }
    public InvoiceHeader? InvoiceHeader { get; set; }
    
}
