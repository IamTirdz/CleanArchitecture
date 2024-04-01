using Clean.Architecture.Business.Common.Exceptions;
using Clean.Architecture.Business.Common.Models;
using FluentAssertions;
using NUnit.Framework;

namespace Clean.Architecture.Business.Tests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Test]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        var failures = new ErrorResponseDto
        {
            Errors = new()
        };

        var actual = new ValidationException(failures).ErrorResponse;

        actual.Errors.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Test]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var failures = new ErrorResponseDto
        {
            Errors = new Dictionary<string, string[]>
            {
                { "Age", new string[] { "must be over 18" } }
            }
        };

        var actual = new ValidationException(failures).ErrorResponse;

        actual.Errors.Keys.Should().BeEquivalentTo(new string[] { "Age" });
        actual.Errors["Age"].Should().BeEquivalentTo(new string[] { "must be over 18" });
    }

    [Test]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        var failures = new ErrorResponseDto
        {
            Errors = new Dictionary<string, string[]>
            {
                { "Age", new string[] 
                    {
                        "must be 18 or older",
                        "must be 25 or younger"
                    } 
                },
                { "Password", new string[] 
                    {
                        "must contain at least 8 characters",
                        "must contain a number",
                        "must contain upper case letter",
                        "must contain lower case letter"
                    } 
                }
            }
        };

        var actual = new ValidationException(failures).ErrorResponse;

        actual.Errors.Keys.Should().BeEquivalentTo(new string[] { "Password", "Age" });

        actual.Errors["Age"].Should().BeEquivalentTo(new string[]
        {
                "must be 25 or younger",
                "must be 18 or older",
        });

        actual.Errors["Password"].Should().BeEquivalentTo(new string[]
        {
                "must contain lower case letter",
                "must contain upper case letter",
                "must contain at least 8 characters",
                "must contain a number",
        });
    }
}
