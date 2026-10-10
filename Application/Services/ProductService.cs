using ApiInvoices.Application.DTOs;
using ApiInvoices.Application.Interfaces;

namespace ApiInvoices.Application.Services
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductResponse>> GetCatalogAsync()
        {
            var products = await productRepository.GetAllAsync();
            return products.Select(x => new ProductResponse
            {
                ProductId = x.ProductId,
                Description = x.Description
            });
        }
    }
}
