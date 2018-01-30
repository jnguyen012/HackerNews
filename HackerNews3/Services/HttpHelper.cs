using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace HackerNews3.Services
{
    public static class HttpHelper
    {
        public static async Task<RequestResult> SendRequestAsync(Uri uri)
        {
            var requestResult = new RequestResult();

            using (var request = new HttpRequestMessage(HttpMethod.Get, uri))
            {

                // Get response
                using (var handler = new HttpClientHandler())
                {
                    using (var client = new HttpClient(handler))
                    {
                        using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead))
                        {
                            var content = response.Content == null
                                ? null
                                : await response
                                .Content
                                .ReadAsStringAsync();

                            if (response.IsSuccessStatusCode)
                            {
                                requestResult.StatusCode = response.StatusCode;
                                requestResult.IsSuccess = true;
                                requestResult.Content = content;
                                requestResult.ErrorMessage = string.Empty;
                            }
                            else
                            {
                                requestResult.StatusCode = response.StatusCode;
                                requestResult.IsSuccess = false;
                                requestResult.Content = string.Empty;
                                requestResult.ErrorMessage = content;
                            }
                        }
                    }
                }
            }

            return requestResult;
        }

        
        public static string CreateQueryStringWithParameters(string queryString, IDictionary<string, string> parameters)
        {
            StringBuilder paramString = null;
            foreach (var param in parameters)
            {
                if (paramString == null)
                {
                    paramString = new StringBuilder("?");
                }
                else
                {
                    paramString.Append("&");
                }

                paramString.AppendFormat("{0}={1}", WebUtility.UrlDecode(param.Key), WebUtility.UrlEncode(param.Value));
            }

            return queryString + paramString.ToString();
        }

    }
}