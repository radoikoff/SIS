using SIS.HTTP.Common;
using SIS.HTTP.Cookies.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private Dictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void AddCookie(HttpCookie cookie)
        {
            CoreValidator.ThrowIfNull(cookie, nameof(cookie));
            this.cookies.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            return this.cookies.ContainsKey(key);
        }

        public HttpCookie GetCookie(string key)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            if (this.ContainsCookie(key))
            {
                return this.cookies[key];
            }
            return null;
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return this.cookies.Values.GetEnumerator();
        }

        public bool HasCookies()
        {
            return this.cookies.Count != 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var cookie in this.cookies.Values)
            {
                sb.Append($"Set-Cookie: {cookie}").Append(GlobalConstants.HttpNewLine);
            }

            return sb.ToString();
        }
    }
}
