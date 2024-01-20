using AutoFixture.AutoMoq;
using AutoFixture;
using AutoFixture.Xunit2;

namespace Api.IntegrationTests
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute(bool configureMockMembers = false)
            : base(() => CreateFixture(configureMockMembers))
        {
        }

        private static IFixture CreateFixture(bool configureMockMembers)
            => new Fixture()
                .Customize(new AutoMoqCustomization { ConfigureMembers = configureMockMembers });
    }
}
