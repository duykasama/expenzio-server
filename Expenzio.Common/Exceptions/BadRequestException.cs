using System.Net;
using Expenzio.Common.Extensions;

namespace Expenzio.Common.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest.ToIntValue())
    {
    }

    public BadRequestException() : base("Request is invalid", HttpStatusCode.BadRequest.ToIntValue())
    {
    }
    
    /// <summary>
    /// Throws a <see cref="BadRequestException" /> if the <paramref name="condition" /> is true.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The message to display.</param>
    /// <exception cref="NotFoundException">Thrown if the condition is true.</exception>
    public static void ThrowIfTrue(bool condition, string message)
    {
        if (condition) throw new BadRequestException(message);
    }
}
