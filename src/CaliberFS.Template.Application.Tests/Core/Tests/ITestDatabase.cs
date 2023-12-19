namespace CaliberFS.Template.Application.Tests.Core.Tests
{
    public interface ITestDatabase
    {
        Task InitialiseAsync(IServiceScopeFactory serviceScopeFactory);

        Task ResetAsync(IServiceScopeFactory serviceScopeFactory);
    }
}