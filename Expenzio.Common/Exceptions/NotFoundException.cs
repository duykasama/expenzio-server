using System.Net;
using Expenzio.Common.Extensions;

namespace Expenzio.Common.Exceptions;

/// <summary>
/// Represents a not found exception.
/// </summary>
/// <remarks>
/// This class is an alternative to the <see cref="System.NotFoundException" /> class in order to return a status code of 404 by controllers.
/// </remarks>
public class NotFoundException : ApiException
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound.ToIntValue())
    {
    }

    public NotFoundException() : base("Not found", HttpStatusCode.NotFound.ToIntValue())
    {
    }

    /// <summary>
    /// Throws a <see cref="NotFoundException" /> if the <paramref name="condition" /> is true.
    /// </summary>
    /// <param name="condition">The condition to check.</param>
    /// <param name="message">The message to display.</param>
    /// <exception cref="NotFoundException">Thrown if the condition is true.</exception>
    public static void ThrowIfTrue(bool condition, string message)
    {
        if (condition) throw new NotFoundException(message);
    }
}
