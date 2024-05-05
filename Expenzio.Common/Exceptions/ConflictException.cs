using System.Net;
using Expenzio.Common.Extensions;

namespace Expenzio.Common.Exceptions;

/// <summary>
/// Represents a conflict exception.
/// </summary>
/// <remarks>
/// This class is an alternative to the <see cref="System.ConflictException" /> class in order to return a status code of 409 by controllers.
/// </remarks>
public class ConflictException : ApiException
{
    public ConflictException(string message) : base(message, HttpStatusCode.Conflict.ToIntValue())
    {
    }

    public ConflictException() : base("Conflict", HttpStatusCode.Conflict.ToIntValue())
    {
    }
}
