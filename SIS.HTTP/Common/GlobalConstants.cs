using System;

namespace SIS.HTTP.Common
{
    public static class GlobalConstants
    {
        public static string HttpOneProtocolFragment = "HTTP/1.1";

        public static string HostHeaderKey = "Host";

        public static string HttpNewLine = "\r\n";

        public static string UnsupportedHttpMethodExceptionMessage = "The HTTP method - {0} is not supported";
    }
}
