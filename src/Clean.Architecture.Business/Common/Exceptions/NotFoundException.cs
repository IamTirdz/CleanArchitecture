using Clean.Architecture.Business.Common.Models;

<<<<<<< HEAD
namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class NotFoundException : BaseException
=======
namespace Clean.Architecture.Business.Common.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException()
>>>>>>> update template
    {
        public NotFoundException()
        {
        }

<<<<<<< HEAD
        public NotFoundException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
=======
    public NotFoundException(ErrorResponse errorResponse) : base(errorResponse)
    {
>>>>>>> update template
    }
}
