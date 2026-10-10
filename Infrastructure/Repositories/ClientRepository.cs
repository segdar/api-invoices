using ApiInvoices.Application.Interfaces;
using ApiInvoices.Domain.Entities;
using ApiInvoices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiInvoices.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly BillingDbContext dbContext;

        public ClientRepository(BillingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await dbContext.Clients.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
