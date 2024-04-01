using Clean.Architecture.Business.Common.Models;

namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class ForbiddenException : BaseException
    {
        public ForbiddenException()
        {
        }

        public ForbiddenException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
    }
}
