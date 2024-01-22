using AutoFixture.Xunit2;

namespace Api.IntegrationTests.AutoFixture
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values) : base(new AutoMoqDataAttribute(), values)
        {
        }
    }
}
