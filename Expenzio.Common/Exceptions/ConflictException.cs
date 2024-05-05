using System.Net;
using Expenzio.Common.Extensions;

namespace Expenzio.Common.Exceptions;

public class ConflictException : ApiException
{
    public ConflictException(string message) : base(message, HttpStatusCode.Conflict.ToIntValue())
    {
    }
}
