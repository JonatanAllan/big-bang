using Enterprise.Operations;
using Enterprise.PubSub.Enums;
using Enterprise.PubSub.Interfaces;

namespace Enterprise.Template.Application.Tests.Core.Fakes
{
    public class PublisherServiceFake : IPublisherService
    {
        public readonly List<KeyValuePair<string, object>> Messages = new();
        public PublisherServiceFake()
        {
        }

        public Task<OperationResult> PublishMessageAsync<T>(T obj, PublishType publishType, string destinationName) where T : class
        {
            Messages.Add(new KeyValuePair<string, object>(destinationName, obj));
            return Task.FromResult(OperationResult.Ok());
        }
    }
}
