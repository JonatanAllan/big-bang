using System.Data.Common;

namespace Application.Tests.Core.Fixture
{
    public interface ITestDatabase
    {
        Task InitialiseAsync();
        Task ResetAsync();
    }
}