using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Cookies.Contracts;
using SIS.HTTP.Enums;
using SIS.HTTP.Extensions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Responses.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Responses
{
    public class HttpResponse : IHttpResponse
    {

        public HttpResponse()
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
            this.Content = new byte[0];
        }

        public HttpResponse(HttpResponseStatusCode statusCode)
            : this()
        {
            CoreValidator.ThrowIfNull(statusCode, nameof(statusCode));
            this.StatusCode = statusCode;
        }

        public HttpResponseStatusCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; }

        public byte[] Content { get; set; }

        public IHttpCookieCollection Cookies { get; }

        public void AddCookie(HttpCookie cookie)
        {
            this.Cookies.AddCookie(cookie);
        }

        public void AddHeader(HttpHeader header)
        {
            this.Headers.AddHeader(header);
        }

        public byte[] GetBytes()
        {
            byte[] httpRespoeBytesWithoutBody = Encoding.UTF8.GetBytes(this.ToString());
            byte[] httpRespoeBytesWithBody = new byte[httpRespoeBytesWithoutBody.Length + this.Content.Length];

            for (int i = 0; i < httpRespoeBytesWithoutBody.Length; i++)
            {
                httpRespoeBytesWithBody[i] = httpRespoeBytesWithoutBody[i];
            }

            for (int i = 0; i < httpRespoeBytesWithBody.Length - httpRespoeBytesWithoutBody.Length; i++)
            {
                httpRespoeBytesWithBody[i + httpRespoeBytesWithoutBody.Length] = this.Content[i];
            }

            return httpRespoeBytesWithBody;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append($"{GlobalConstants.HttpOneProtocolFragment} {this.StatusCode.GetStatusLine()}")
                  .Append(GlobalConstants.HttpNewLine)
                  .Append($"{this.Headers}")
                  .Append(GlobalConstants.HttpNewLine);

            if (this.Cookies.HasCookies())
            {
                result.Append($"{this.Cookies}").Append(GlobalConstants.HttpNewLine);
            }

            result.Append(GlobalConstants.HttpNewLine);

            return result.ToString();
        }
    }
}
