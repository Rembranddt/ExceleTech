using ExceleTech.Application.Responses;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace ExceleTech.Application.Behaviors;

internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : BaseResponse
    where TRequest : class
    {
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationFailure[] ValidationFailures = Validate(request);
        if (ValidationFailures.Length == 0)
        {
            return await next();
        }

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(BaseResponse<>))
        {
            Type resultType = typeof(TResponse).GetGenericArguments()[0];
            Type ElementType = typeof(BaseResponse<>).MakeGenericType(resultType);
            TResponse response = (TResponse)Activator.CreateInstance(ElementType);

            AddValidationFailures(response, ValidationFailures);
            return response;
        }
        else
        {
            TResponse response = (TResponse)Activator.CreateInstance(typeof(BaseResponse));
            AddValidationFailures(response, ValidationFailures);

            return response;
        }
    }

    public ValidationFailure[] Validate(TRequest request)
    {
        var context = new ValidationContext<TRequest>(request);

        ValidationResult[] ValidationResults = _validators
            .Select(val => val.Validate(context))
            .ToArray();

        ValidationFailure[] ValidationFailures = ValidationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();

        return ValidationFailures;
    }

    public void AddValidationFailures(BaseResponse response, ValidationFailure[] validationFailures)
    {
        foreach (var failure in validationFailures)
        {
            response.AddError(failure.PropertyName + " " + failure.ErrorMessage);
        }
        
        response.StatusCode = HttpStatusCode.BadRequest;
    }
}

