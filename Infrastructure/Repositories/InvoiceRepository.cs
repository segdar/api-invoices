using ApiInvoices.Application.Interfaces;
using ApiInvoices.Domain.Entities;
using ApiInvoices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiInvoices.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly BillingDbContext dbContext;

    public InvoiceRepository(BillingDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(InvoiceHeader invoiceHeader)
    {
        dbContext.InvoiceHeaders.Add(invoiceHeader);
        await dbContext.SaveChangesAsync();
    }

    public Task<bool> InvoiceNumberExistsAsync(string invoiceNumber)
    {
        return dbContext.InvoiceHeaders.AnyAsync(invoice => invoice.InvoiceNumber == invoiceNumber);
    }

    public Task<bool> ClientExistsAsync(int clientId)
    {
        return dbContext.Clients.AnyAsync(client => client.ClientId == clientId);
    }

    public Task<bool> ProductExistsAsync(int productId)
    {
        return dbContext.Products.AnyAsync(product => product.ProductId == productId);
    }
}
