using ApiInvoices.Application.DTOs;
using ApiInvoices.Application.Interfaces;
using ApiInvoices.Application.Services;
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
    public async Task<IActionResult> Create([FromBody] InvoiceCreateRequest request)
    {
        var result = await invoiceService.CreateAsync(request);
        if (result == null)
            return BadRequest(ApiResponse<object>.Error("invoice could not be saved"));

        return Ok(ApiResponse<InvoiceCreateResponse>.Ok(result, "invoice saved"));
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var cancelled = await invoiceService.CancelAsync(id);
        if (!cancelled)
            return BadRequest(ApiResponse<object>.Error("invoice not found or already cancelled"));

        return Ok(ApiResponse<object>.Ok(null!, "invoice cancelled"));
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var list = await invoiceService.GetListAsync();
        return Ok(ApiResponse<IEnumerable<InvoiceListResponse>>.Ok(list));
    }
}
