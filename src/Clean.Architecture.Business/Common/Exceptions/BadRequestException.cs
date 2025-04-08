using Clean.Architecture.Business.Common.Models;

<<<<<<< HEAD
namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class BadRequestException : BaseException
=======
namespace Clean.Architecture.Business.Common.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException()
    {
    }

    public BadRequestException(ErrorResponse errorResponse) : base(errorResponse)
>>>>>>> update template
    {
        public BadRequestException()
        {
        }

        public BadRequestException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
    }
}
