using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using RESTFulSense.Clients;
namespace DailyRecipes.Tests.Integration.Brokers
{
    public partial class DailyRecipesApiBroker
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient baseClient;
        private readonly IRESTFulApiFactoryClient apiFactoryClient;

        public DailyRecipesApiBroker()
        {
            webApplicationFactory = new WebApplicationFactory<Startup>();
            baseClient = webApplicationFactory.CreateClient();
            apiFactoryClient = new RESTFulApiFactoryClient(baseClient);
        }
    }
}
