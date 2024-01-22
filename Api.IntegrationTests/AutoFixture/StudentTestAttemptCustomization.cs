using AutoFixture;
using Domain.Entities;

namespace Api.IntegrationTests.AutoFixture
{
    public class StudentTestAttemptCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<StudentTestAttempt>(composer => composer
                .Without(x => x.Student)
                .Without(x => x.Test)
                .Without(x => x.Results));
        }
    }
}
