using AutoFixture.AutoMoq;
using AutoFixture;
using AutoFixture.Xunit2;

namespace Api.IntegrationTests.AutoFixture
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute(bool configureMockMembers = false)
            : base(() => CreateFixture(configureMockMembers))
        {
        }

        private static IFixture CreateFixture(bool configureMockMembers)
            => new Fixture()
                .Customize(new RecursiveObjectCustomization())
                .Customize(new AutoMoqCustomization { ConfigureMembers = configureMockMembers });
    }
}
