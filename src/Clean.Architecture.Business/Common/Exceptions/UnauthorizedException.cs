using Clean.Architecture.Business.Common.Models;

namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
    }
}
