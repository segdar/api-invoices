using ApiInvoices.Domain.Entities;

namespace ApiInvoices.Application.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();

}

