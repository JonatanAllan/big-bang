﻿using FluentValidation.Results;

namespace Enterprise.Template.Application.Common.Exceptions
{
    public class ValidationException() : Exception("One or more validation failures have occurred.")
    {
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public ValidationException(string objectName, string message)
            : this()
        {
            Errors = new Dictionary<string, string[]>
            {
                { objectName, new[] { message } }
            };
        }

        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
    }
}
