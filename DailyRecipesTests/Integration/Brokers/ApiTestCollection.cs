namespace DailyRecipes.Tests.Integration.Brokers
{
    [Xunit.CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection : Xunit.ICollectionFixture<DailyRecipesApiBroker>
    {
    }
}
