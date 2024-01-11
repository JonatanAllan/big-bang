using static Enterprise.Template.Application.Tests.Testing;

namespace Enterprise.Template.Application.Tests.Core.Tests
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
