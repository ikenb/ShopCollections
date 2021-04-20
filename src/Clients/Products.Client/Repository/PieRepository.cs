using Product.API.Models;
using Products.Client.Interfaces.IRepository;
using System.Net.Http;

namespace Products.Client.Repository
{
    public class PieRepository : Repository<Pie>, IPieRepository
    {
        private readonly IHttpClientFactory _clientFactory;

        public PieRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
