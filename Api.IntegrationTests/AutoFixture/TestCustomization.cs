using AutoFixture;
using Domain.Entities;

namespace Api.IntegrationTests.AutoFixture
{
    public class TestCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Test>(composer => composer
                .Without(x => x.Topic)
                .Without(x => x.Students)
                .Without(x => x.StudentsAttempts)
                .Without(x => x.TestQuestions)
                .Without(x => x.Questions));
        }
    }
}
