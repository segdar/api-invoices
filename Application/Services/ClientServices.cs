using ApiInvoices.Application.DTOs;
using ApiInvoices.Application.Interfaces;
using ApiInvoices.Infrastructure.Repositories;

namespace ApiInvoices.Application.Services
{
    public class ClientServices : IClientServices

    {
        private readonly IClientRepository clientRepository;

        public ClientServices(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        public async Task<IEnumerable<ClientResponse>> GetCatalogAsync()
        {
            var clients = await clientRepository.GetAllAsync();
            return clients.Select(x => new ClientResponse
            {
                ClientId = x.ClientId,
                FullName = x.Name + " " + x.LastName
            });
        }
    }
}
