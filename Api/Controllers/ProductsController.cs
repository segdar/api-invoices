using ApiInvoices.Application.DTOs;
using ApiInvoices.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiInvoices.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await this.productService.GetCatalogAsync();
            return Ok(ApiResponse<IEnumerable<ProductResponse>>.Ok(list));
        }
    }
}
