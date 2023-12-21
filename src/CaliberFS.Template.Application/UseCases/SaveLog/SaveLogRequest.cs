using FluentValidation;
using MediatR;

namespace CaliberFS.Template.Application.UseCases.SaveLog
{
    public class SaveLogRequest : IRequest<Unit>
    {
        public SaveLogRequest(Guid correlationId, string message)
        {
            CorrelationId = correlationId;
            Message = message;
        }
        public SaveLogRequest() { }
        public Guid CorrelationId { get; set; }
        public string? Message { get; set; }
    }

    public class SaveLogRequestValidator : AbstractValidator<SaveLogRequest>
    {
        public SaveLogRequestValidator()
        {
            RuleFor(x => x.CorrelationId).NotEmpty();
            RuleFor(x => x.Message).NotEmpty();
        }
    }
}
