using AutoFixture;
using Domain.Entities;

namespace Api.IntegrationTests.AutoFixture
{
    public class TopicCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Topic>(composer => composer
                .Without(x => x.Tests)
                .Without(x => x.Questions)
                .Without(x => x.Subject));
        }
    }
}
