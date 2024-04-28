namespace Expenzio.Common.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(string message) : base(message, 400)
    {
    }

    public BadRequestException() : base("Request is invalid", 400)
    {
    }
}
