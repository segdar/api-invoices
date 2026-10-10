using ApiInvoices.Application.Interfaces;
using ApiInvoices.Domain.Entities;
using ApiInvoices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiInvoices.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly BillingDbContext dbContext;

        public ProductRepository(BillingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await dbContext.Products.OrderBy(p => p.Description).ToListAsync();
        }
    }
}
