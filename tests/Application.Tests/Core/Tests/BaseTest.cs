using static Application.Tests.Testing;

namespace Application.Tests.Core.Fixture
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
