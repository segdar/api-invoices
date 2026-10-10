using ApiInvoices.Domain.Entities;

namespace ApiInvoices.Application.Interfaces;
public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
}

