using System.Net;
using Expenzio.Common.Extensions;

namespace Expenzio.Common.Exceptions;

/// <summary>
/// Represents a bad request exception.
/// </summary>
/// <remarks>
/// This class is an alternative to the <see cref="System.BadRequestException" /> class in order to return a status code of 400 by controllers.
/// </remarks>
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
