namespace ApiInvoices.Application.DTOs
{
    public class InvoiceCreateRequest
    {
        public int ClientId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public List<InvoiceDetailRequest> Details { get; set; } = new();
    }

    public class InvoiceDetailRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
