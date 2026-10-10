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

    public async Task AddDetailAsync(InvoiceDetail detail)
    {
        await dbContext.InvoiceDetails.AddAsync(detail);
    }

    public async Task AddHeaderAsync(InvoiceHeader header)
    {
        await dbContext.InvoiceHeaders.AddAsync(header);
    }

    public async Task<InvoiceHeader?> GetByIdAsync(int id)
    {
        return await dbContext.InvoiceHeaders.FirstOrDefaultAsync(h => h.InvoiceHeaderId == id);
    }

    public async Task<IEnumerable<InvoiceListItem>> GetListAsync()
    {
       return await dbContext.InvoiceHeaders
            .OrderBy(x => x.InvoiceDate)
            .Select(h => new InvoiceListItem
            {
                InvoiceDate = h.InvoiceDate,
                InvoiceNumber = h.InvoiceNumber,
                ClientName = dbContext.Clients.Where(c => c.ClientId == h.ClientId)
                .Select(c => c.Name + " " + c.LastName).FirstOrDefault() ?? string.Empty,
                TotalAmount = h.TotalAmount,
                Status = h.Status
            })
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
