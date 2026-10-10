using ApiInvoices.Domain.Entities;

namespace ApiInvoices.Application.Interfaces;

public class InvoiceListItem
{
    public DateTime InvoiceDate { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
}
public interface IInvoiceRepository
{
    Task<InvoiceHeader?> GetByIdAsync(int id);
    Task AddHeaderAsync(InvoiceHeader header);
    Task AddDetailAsync(InvoiceDetail detail);
    Task SaveChangesAsync();
    Task<IEnumerable<InvoiceListItem>> GetListAsync();

}
