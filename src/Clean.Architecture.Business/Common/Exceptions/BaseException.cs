using Clean.Architecture.Business.Common.Models;

namespace Clean.Architecture.Business.Common.Exceptions
{
    [Serializable]
    public abstract class BaseException : Exception
    {
        public ErrorResponseDto ErrorResponse { get; set; }

        protected BaseException() : base(string.Empty)
        {            
        }

        public BaseException(ErrorResponseDto errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }
}
