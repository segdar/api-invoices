using ApiInvoices.Application.DTOs;
using ApiInvoices.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiInvoices.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly IClientServices clientService;

        public ClientsController(IClientServices clientService)
        {
            this.clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await this.clientService.GetCatalogAsync();
            return Ok(ApiResponse<IEnumerable<ClientResponse>>.Ok(list));
        }
    }
}
