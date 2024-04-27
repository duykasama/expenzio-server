namespace Expenzio.Common.Exceptions;

public class UnauthorizedException : ApiException
{
    public UnauthorizedException(string message) : base(message, 401)
    {
    }

    public UnauthorizedException() : base("Unauthorized", 401)
    {
    }
}
