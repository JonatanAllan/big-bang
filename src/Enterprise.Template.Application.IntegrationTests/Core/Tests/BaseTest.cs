using static Enterprise.Template.Application.IntegrationTests.Testing;

namespace Enterprise.Template.Application.IntegrationTests.Core.Tests
{
    [TestFixture]
    public abstract class BaseTest
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }
    }
}
