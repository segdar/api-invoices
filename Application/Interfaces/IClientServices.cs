using ApiInvoices.Application.DTOs;

namespace ApiInvoices.Application.Interfaces;

public interface IClientServices
{
        Task<IEnumerable<ClientResponse>> GetCatalogAsync();
}

