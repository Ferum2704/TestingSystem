using AutoFixture;

namespace Api.IntegrationTests.AutoFixture
{
    public class TestingSystemCompositeCustomization : CompositeCustomization
    {
        public TestingSystemCompositeCustomization() 
            : base(
                  new SubjectCustomization())
        {
        }
    }
}
