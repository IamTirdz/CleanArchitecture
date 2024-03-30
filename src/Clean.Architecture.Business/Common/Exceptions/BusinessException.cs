using Clean.Architecture.Business.Common.Models;

namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class BusinessException : BaseException
    {
        public BusinessException()
        {
        }

        public BusinessException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
    }
}
