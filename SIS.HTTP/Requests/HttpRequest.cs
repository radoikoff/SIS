﻿using SIS.HTTP.Common;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Headers.Contracts;
using SIS.HTTP.Requests.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        public HttpRequest(string requestString)
        {
            CoreValidator.ThrowIfNullOrEmpty(requestString, nameof(requestString));

            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();

            this.ParseRequest(requestString);
        }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }



        private bool IsValidRequestLine(string[] stringRequestLineParams)
        {
            if (stringRequestLineParams.Length != 3 || stringRequestLineParams[2] != GlobalConstants.HttpOneProtocolFragment)
            {
                return false;
            }

            return true;
        }

        private IEnumerable<string> ParsePlainRequestHeaders(string[] requestLines)
        {
            for (int i = 1; i < requestLines.Length - 1; i++)
            {
                if (!string.IsNullOrEmpty(requestLines[i]))
                {
                    yield return requestLines[i];
                }
            }
        }

        private void ParseRequestMethod(string[] stringRequestLineParams)
        {
            HttpRequestMethod method;
            bool parseResult = HttpRequestMethod.TryParse(stringRequestLineParams[0], true, out method);

            if (!parseResult)
            {
                throw new BadRequestException(string.Format(GlobalConstants.UnsupportedHttpMethodExceptionMessage, stringRequestLineParams[0]));
            }

            this.RequestMethod = method;
        }

        private void ParseRequestUrl(string[] stringRequestLineParams)
        {
            this.Url = stringRequestLineParams[1];
        }

        private void ParseRequestPath()
        {
            this.Path = this.Url.Split('?')[0];
        }

        private void ParseRequestHeaders(string[] plainHeaders)
        {
            foreach (var plainHeader in plainHeaders)
            {
                string[] headerKvp = plainHeader.Split(": ", StringSplitOptions.RemoveEmptyEntries);
                HttpHeader header = new HttpHeader(headerKvp[0], headerKvp[1]);
                this.Headers.AddHeader(header);
            }
        }


        private void ParseRequestQueryParameters()
        {
            this.Url.Split('?', '#')[1]
                    .Split('&')
                    .Select(plainQueryParam => plainQueryParam.Split('='))
                    .ToList()
                    .ForEach(queryParamKvp => this.QueryData.Add(queryParamKvp[0], queryParamKvp[1]));
        }

        private void ParseFormDataParameters(string requestBody)
        {
            //TODO: Parse multiple parameters by name (id=1&id=5). To be parced to collection

            this.Url.Split('?', '#')[1]
                    .Split('&')
                    .Select(plainQueryParam => plainQueryParam.Split('='))
                    .ToList()
                    .ForEach(queryParamKvp => this.FormData.Add(queryParamKvp[0], queryParamKvp[1]));
        }

        private void ParseRequestParameters(string requestBody)
        {
            this.ParseRequestQueryParameters();
            this.ParseFormDataParameters(requestBody);
        }

        private void ParseRequest(string requestString)
        {
            var splitRequestContent = requestString.Split(GlobalConstants.HttpNewLine, StringSplitOptions.None);

            var stringRequestLineParams = splitRequestContent[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(stringRequestLineParams))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(stringRequestLineParams);
            this.ParseRequestUrl(stringRequestLineParams);
            this.ParseRequestPath();

            this.ParseRequestHeaders(this.ParsePlainRequestHeaders(splitRequestContent).ToArray());

            //this.ParseCookies();
            this.ParseRequestParameters(splitRequestContent[splitRequestContent.Length - 1]);
        }

    }
}