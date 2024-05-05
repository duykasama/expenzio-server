using System.Net;
using Expenzio.Common.Extensions;

namespace Expenzio.Common.Exceptions;

/// <summary>
/// Represents an unauthorized exception.
/// </summary>
/// <remarks>
/// This class is an alternative to the <see cref="System.UnauthorizedAccessException" /> class in order to return a status code of 401 by controllers.
/// </remarks>
public class UnauthorizedException : ApiException
{
    public UnauthorizedException(string message) : base(message, HttpStatusCode.Unauthorized.ToIntValue())
    {
    }

    public UnauthorizedException() : base("Unauthorized", HttpStatusCode.Unauthorized.ToIntValue())
    {
    }

    /// <summary>
    /// Throws an <see cref="UnauthorizedException" /> if the object is null.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <param name="message">The message to display.</param>
    /// <exception cref="UnauthorizedException">Thrown if the object is null.</exception>
    public static void ThrowIfNullOrEmpty(object? obj, string message)
    {
        if (obj is null) throw new UnauthorizedException(message);
        if (obj is string str && string.IsNullOrEmpty(str)) throw new UnauthorizedException(message);
    }
}
