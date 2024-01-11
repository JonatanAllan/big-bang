using Enterprise.Template.Application.Interfaces;

namespace Enterprise.Template.Application.Application
{
    public class SamplePeriodicApplication : ISamplePeriodicApplication
    {
        public SamplePeriodicApplication() { }

        public Task DoSomething()
        {
            // Do something
            return Task.CompletedTask;
        }
    }
}
