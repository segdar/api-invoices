namespace ApiInvoices.Application.DTOs
{
    public class InvoiceListResponse
    {
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
