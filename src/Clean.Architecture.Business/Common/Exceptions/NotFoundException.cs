using Clean.Architecture.Business.Common.Models;

namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public class NotFoundException : BaseException
    {
        public NotFoundException()
        {
        }

        public NotFoundException(ErrorResponseDto errorResponse) : base(errorResponse)
        {
        }
    }
}
