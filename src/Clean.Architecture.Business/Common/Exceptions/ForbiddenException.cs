using Clean.Architecture.Business.Common.Models;

<<<<<<< HEAD
namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class ForbiddenException : BaseException
=======
namespace Clean.Architecture.Business.Common.Exceptions;

public class ForbiddenException : BaseException
{
    public ForbiddenException()
    {
    }

    public ForbiddenException(ErrorResponse errorResponse) : base(errorResponse)
>>>>>>> update template
    {
        public ForbiddenException()
        {
        }

        public ForbiddenException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
    }
}
