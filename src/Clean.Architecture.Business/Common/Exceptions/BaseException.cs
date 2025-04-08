using Clean.Architecture.Business.Common.Models;

<<<<<<< HEAD
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
=======
namespace Clean.Architecture.Business.Common.Exceptions;

[Serializable]
public abstract class BaseException : Exception
{
    public ErrorResponse ErrorResponse { get; set; } = null!;

    protected BaseException() : base(string.Empty)
    {
    }

    protected BaseException(ErrorResponse errorResponse)
    {
        ErrorResponse = errorResponse;
>>>>>>> update template
    }
}
