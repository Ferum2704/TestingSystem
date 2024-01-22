using AutoFixture;

namespace Api.IntegrationTests.AutoFixture
{
    public class TestingSystemCompositeCustomization : CompositeCustomization
    {
        public TestingSystemCompositeCustomization() 
            : base(
                  new SubjectCustomization(),
                  new TopicCustomization(),
                  new QuestionCustomization(),
                  new TestCustomization(),
                  new StudentTestAttemptCustomization())
        {
        }
    }
}
