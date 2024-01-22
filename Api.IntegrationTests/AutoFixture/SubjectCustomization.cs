using AutoFixture;
using Domain.Entities;

namespace Api.IntegrationTests.AutoFixture
{
    public class SubjectCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Subject>(composer => composer
                .Without(x => x.Topics)
                .Without(x => x.Teacher)
                .Without(x => x.TeacherId));
        }
    }
}
