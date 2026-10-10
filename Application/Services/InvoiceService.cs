using ApiInvoices.Application.DTOs;
using ApiInvoices.Application.Interfaces;
using ApiInvoices.Domain.Entities;
using ApiInvoices.Infrastructure.Data;



namespace ApiInvoices.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository invoiceRepository;
    private readonly BillingDbContext context;

    public InvoiceService(IInvoiceRepository invoiceRepository, BillingDbContext context)
    {
        this.invoiceRepository = invoiceRepository;
        this.context = context;
    }

    public async Task<bool> CancelAsync(int id)
    {
        var header = await invoiceRepository.GetByIdAsync(id);
        if (header == null) return false;
        if (header.Status == "cancelled") return false; 

        header.Status = "cancelled";
        await invoiceRepository.SaveChangesAsync();
        return true;
    }

    

    public async Task<InvoiceCreateResponse?> CreateAsync(InvoiceCreateRequest request)
    {
        if (request.Details == null || request.Details.Count == 0)
            return null;

        
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var header = new InvoiceHeader
            {
                ClientId = request.ClientId,
                InvoiceNumber = request.InvoiceNumber.Trim(),
                InvoiceDate = request.InvoiceDate,
                Status = "saved",
                TotalAmount = 0
            };
            await invoiceRepository.AddHeaderAsync(header);
            await invoiceRepository.SaveChangesAsync();

            decimal total = 0;
            foreach (var item in request.Details)
            {
                if (item.Quantity <= 0 || item.Price < 0)
                    throw new InvalidOperationException("invalid detail");

                var detail = new InvoiceDetail
                {
                    InvoiceHeaderId = header.InvoiceHeaderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Subtotal = item.Quantity * item.Price
                };
                total += detail.Subtotal;
                await invoiceRepository.AddDetailAsync(detail);
            }

            header.TotalAmount = total;
            await invoiceRepository.SaveChangesAsync();
            await transaction.CommitAsync();

            return new InvoiceCreateResponse
            {
                InvoiceHeaderId = header.InvoiceHeaderId,
                TotalAmount = total
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            return null;
        }
    }

    public async Task<IEnumerable<InvoiceListResponse>> GetListAsync()
    {
        var items = await invoiceRepository.GetListAsync();
        return items.Select(x => new InvoiceListResponse
        {
            InvoiceDate = x.InvoiceDate,
            InvoiceNumber = x.InvoiceNumber,
            ClientName = x.ClientName,
            TotalAmount = x.TotalAmount,
            Status = x.Status
        });
    }

    
}
