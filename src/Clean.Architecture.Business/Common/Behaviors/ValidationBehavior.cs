using Clean.Architecture.Business.Common.Models;
using FluentValidation;
using MediatR;
using System.Diagnostics;

namespace Clean.Architecture.Business.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators ?? throw new ArgumentNullException(nameof(validators));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators
            .Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(e => e.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        var errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(g => g.Key, g => g.ToArray());

        if (failures.Any())
            throw new Exceptions.ValidationException(new ErrorResponseDto
            {
                Message = "One or more validation failures have occurred.",
                Errors = errors,
                ReferenceKey = Activity.Current!.RootId!
            });

        return await next();
    }
}

