using Clean.Architecture.Business.Common.Models;
using FluentValidation;
using MediatR;
using System.Diagnostics;
<<<<<<< HEAD
=======
using ValidationException = Clean.Architecture.Business.Common.Exceptions.ValidationException;
>>>>>>> update template

namespace Clean.Architecture.Business.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) 
    : IPipelineBehavior<TRequest, TResponse> where TRequest 
    : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
<<<<<<< HEAD
        if (!_validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(_validators
            .Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(e => e.Errors.Any())
=======
        if (!validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(validators
            .Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(e => e.Errors.Count != 0)
>>>>>>> update template
            .SelectMany(r => r.Errors)
            .ToList();

        var errors = failures
<<<<<<< HEAD
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(g => g.Key, g => g.ToArray());

        if (failures.Any())
            throw new Exceptions.ValidationException(new ErrorResponseDto
            {
                Message = "One or more validation failures have occurred.",
                Errors = errors,
                ReferenceKey = Activity.Current!.RootId!
            });
=======
            .GroupBy(p => p.PropertyName, e => e.ErrorMessage)
            .ToDictionary(f => f.Key, f => f.ToArray());

        if (failures.Count != 0)
            throw new ValidationException(
                new ErrorResponse
                {
                    ReferenceKey = Activity.Current!.RootId!,
                    Message = "One or more validation errors occurred.",
                    Errors = errors
                });
>>>>>>> update template

        return await next();
    }
}
