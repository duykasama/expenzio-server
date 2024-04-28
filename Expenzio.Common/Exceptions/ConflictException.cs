namespace Expenzio.Common.Exceptions;

public class ConflictException : ApiException
{
    public ConflictException(string message) : base(message, 409)
    {
    }
}
