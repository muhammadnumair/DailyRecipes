using System;
using System.Collections.Generic;
using System.Text;

namespace DailyRecipes.Tests.Integration.Brokers
{
    [Xunit.CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection : Xunit.ICollectionFixture<DailyRecipesApiBroker>
    {
    }
}
