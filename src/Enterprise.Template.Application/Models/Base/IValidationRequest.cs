using FluentValidation.Results;

namespace Enterprise.Template.Application.Models.Base
{
    public interface IValidationRequest
    {
        Task<ValidationResult> Validate();
    }
}
