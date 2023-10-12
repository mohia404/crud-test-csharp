using ErrorOr;

namespace CustomerManager.Domain.Common.Exceptions;

public abstract class ErrorException : Exception
{
    public Error Error { get; }

    protected ErrorException(Error error)
    {
        Error = error;
    }
}