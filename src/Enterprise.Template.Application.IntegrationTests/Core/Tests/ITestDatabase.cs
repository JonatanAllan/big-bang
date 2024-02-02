namespace Enterprise.Template.Application.IntegrationTests.Core.Tests
{
    public interface ITestDatabase
    {
        Task InitialiseAsync();
        Task ResetAsync();
    }
}