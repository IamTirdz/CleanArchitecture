using Clean.Architecture.Business.Common.Models;
<<<<<<< HEAD

namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class ValidationException : BaseException
    {
        public ValidationException()
        {
        }

        public ValidationException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
=======
using FluentValidation.Results;

namespace Clean.Architecture.Business.Common.Exceptions;

public class ValidationException : BaseException
{
    public ValidationException()
    {
    }

    public ValidationException(ErrorResponse errorResponse) : base(errorResponse)
    {
>>>>>>> update template
    }
}

