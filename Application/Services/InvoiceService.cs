using ApiInvoices.Api.Dtos;
using ApiInvoices.Application.Interfaces;
using ApiInvoices.Application.Validations;
using ApiInvoices.Domain.Entities;

namespace ApiInvoices.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository invoiceRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository)
    {
        this.invoiceRepository = invoiceRepository;
    }

    public async Task<InvoiceResponseDto> CreateAsync(InvoiceCreateRequestDto request)
    {
        await ValidateAsync(request);

        var invoiceHeader = new InvoiceHeader
        {
            ClientId = request.ClientId,
            InvoiceNumber = request.InvoiceNumber.Trim(),
            InvoiceDate = request.InvoiceDate,
            Status = request.Status.Trim().ToLowerInvariant(),
            Details = request.Details.Select(detail => new InvoiceDetail
            {
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                Price = detail.Price,
                Subtotal = detail.Quantity * detail.Price
            }).ToList()
        };

        invoiceHeader.TotalAmount = invoiceHeader.Details.Sum(detail => detail.Subtotal);
        await invoiceRepository.AddAsync(invoiceHeader);

        return new InvoiceResponseDto
        {
            InvoiceHeaderId = invoiceHeader.InvoiceHeaderId,
            ClientId = invoiceHeader.ClientId,
            InvoiceNumber = invoiceHeader.InvoiceNumber,
            InvoiceDate = invoiceHeader.InvoiceDate,
            Status = invoiceHeader.Status,
            TotalAmount = invoiceHeader.TotalAmount,
            Details = invoiceHeader.Details.Select(detail => new InvoiceDetailResponseDto
            {
                InvoiceDetailId = detail.InvoiceDetailId,
                ProductId = detail.ProductId,
                Quantity = detail.Quantity,
                Price = detail.Price,
                Subtotal = detail.Subtotal
            }).ToList()
        };
    }

    private async Task ValidateAsync(InvoiceCreateRequestDto request)
    {
        if (request.ClientId <= 0)
        {
            throw new ValidationException("ClientId is required.");
        }

        if (string.IsNullOrWhiteSpace(request.InvoiceNumber))
        {
            throw new ValidationException("InvoiceNumber is required.");
        }

        if (!string.Equals(request.Status.Trim(), "saved", StringComparison.OrdinalIgnoreCase) &&
            !string.Equals(request.Status.Trim(), "cancelled", StringComparison.OrdinalIgnoreCase))
        {
            throw new ValidationException("Status must be saved or cancelled.");
        }

        if (request.Details.Count == 0)
        {
            throw new ValidationException("At least one invoice detail is required.");
        }

        if (!await invoiceRepository.ClientExistsAsync(request.ClientId))
        {
            throw new ValidationException("Client does not exist.");
        }

        if (await invoiceRepository.InvoiceNumberExistsAsync(request.InvoiceNumber.Trim()))
        {
            throw new ValidationException("InvoiceNumber already exists.");
        }

        foreach (var detail in request.Details)
        {
            if (detail.ProductId <= 0 || detail.Quantity <= 0 || detail.Price <= 0)
            {
                throw new ValidationException("Each detail must include a valid product, quantity and price.");
            }

            if (!await invoiceRepository.ProductExistsAsync(detail.ProductId))
            {
                throw new ValidationException($"Product {detail.ProductId} does not exist.");
            }
        }
    }
}
