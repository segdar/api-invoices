using ApiInvoices.Application.DTOs;

namespace ApiInvoices.Application.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceCreateResponse?> CreateAsync(InvoiceCreateRequest request);
    Task<bool> CancelAsync(int id);
    Task<IEnumerable<InvoiceListResponse>> GetListAsync();
}
