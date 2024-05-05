using System.Net;

namespace Expenzio.Common.Extensions;

public static class HttpStatusCodeExtensions
{
    public static int ToIntValue(this HttpStatusCode statusCode)
    {
        return (int)statusCode;
    }
}
