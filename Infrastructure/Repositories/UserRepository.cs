using ApiInvoices.Application.Interfaces;
using ApiInvoices.Domain.Entities;
using ApiInvoices.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiInvoices.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BillingDbContext dbContext;

    public UserRepository(BillingDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task<User?> GetByIdAsync(int id)
    {
        return dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        return dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);
    }
}
