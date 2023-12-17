﻿using FluentValidation.Results;

namespace Clean.Architecture.Business.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
           .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
           .ToDictionary(g => g.Key, g => g.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; private set; }
}
