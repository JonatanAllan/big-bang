using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Enterprise.Template.Application.UseCases.PeriodicUseCase
{
    public class PeriodicUseCase : IRequestHandler<PeriodicUseCaseRequest, Unit>
    {
        public Task<Unit> Handle(PeriodicUseCaseRequest request, CancellationToken cancellationToken)
        {
            // TODO: Implement periodic use case
            return Task.FromResult(Unit.Value);
        }
    }

    public record PeriodicUseCaseRequest : IRequest<Unit>;
}
