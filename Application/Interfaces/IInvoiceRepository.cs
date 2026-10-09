using ApiInvoices.Domain.Entities;

namespace ApiInvoices.Application.Interfaces;

public interface IInvoiceRepository
{
    Task AddAsync(InvoiceHeader invoiceHeader);
    Task<bool> InvoiceNumberExistsAsync(string invoiceNumber);
    Task<bool> ClientExistsAsync(int clientId);
    Task<bool> ProductExistsAsync(int productId);
}
