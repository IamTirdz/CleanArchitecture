using Clean.Architecture.Business.Common.Models;

namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class BadRequestException : BaseException
    {
        public BadRequestException()
        {
        }

        public BadRequestException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
    }
}
