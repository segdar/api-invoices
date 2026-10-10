namespace ApiInvoices.Application.DTOs
{
    public class InvoiceCreateResponse
    {
        public int InvoiceHeaderId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
