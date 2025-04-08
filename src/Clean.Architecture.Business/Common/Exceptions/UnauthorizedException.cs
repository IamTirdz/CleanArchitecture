using Clean.Architecture.Business.Common.Models;

<<<<<<< HEAD
namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class UnauthorizedException : BaseException
=======
namespace Clean.Architecture.Business.Common.Exceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException()
    {
    }

    public UnauthorizedException(ErrorResponse errorResponse) : base(errorResponse)
>>>>>>> update template
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
    }
}
