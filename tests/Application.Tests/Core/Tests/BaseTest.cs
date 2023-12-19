using static CaliberFS.Template.Application.Tests.Testing;

namespace CaliberFS.Template.Application.Tests.Core.Tests
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
