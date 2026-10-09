using ApiInvoices.Api.Dtos;

namespace ApiInvoices.Application.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceResponseDto> CreateAsync(InvoiceCreateRequestDto request);
}
