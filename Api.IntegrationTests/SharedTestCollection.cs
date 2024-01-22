using Xunit;

namespace Api.IntegrationTests
{
    [CollectionDefinition("SharedWebFactory")]
    public class SharedTestCollection : ICollectionFixture<WebApplicationFactoryFixture>
    {
    }
}
