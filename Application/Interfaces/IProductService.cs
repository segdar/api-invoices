using ApiInvoices.Application.DTOs;

namespace ApiInvoices.Application.Interfaces;
public interface IProductService
{
        Task<IEnumerable<ProductResponse>> GetCatalogAsync();
}

