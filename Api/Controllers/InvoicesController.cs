using ApiInvoices.Api.Dtos;
using ApiInvoices.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiInvoices.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        this.invoiceService = invoiceService;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<InvoiceResponseDto>>> Create(InvoiceCreateRequestDto request)
    {
        var invoice = await invoiceService.CreateAsync(request);
        return Ok(ApiResponse<InvoiceResponseDto>.Ok("Invoice saved successfully.", invoice));
    }
}
