﻿namespace Enterprise.Template.Application.Tests.Core.Tests
{
    public interface ITestDatabase
    {
        Task InitialiseAsync();
        Task ResetAsync();
    }
}