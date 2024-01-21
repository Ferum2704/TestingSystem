using AutoFixture;
using Domain.Entities;

namespace Api.IntegrationTests.AutoFixture
{
    public class QuestionCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Question>(composer => composer
                .Without(x => x.Tests)
                .Without(x => x.Results)
                .Without(x => x.Topic));
        }
    }
}
