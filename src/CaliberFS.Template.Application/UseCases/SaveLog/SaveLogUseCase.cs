using MediatR;

namespace CaliberFS.Template.Application.UseCases.SaveLog
{
    public class SaveLogUseCase : IRequestHandler<SaveLogRequest, Unit>
    {
        public Task<Unit> Handle(SaveLogRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
