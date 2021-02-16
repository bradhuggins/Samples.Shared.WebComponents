using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjectWebApplication
{
    public class Helper
    {
        protected readonly IConfiguration _configuration;

        public Helper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        private string BaseUrl 
        {
            get
            {
                return _configuration.GetValue<string>("WebComponentsBaseUrl");
            }
        }

        private readonly static HttpClientHandler _handler = new HttpClientHandler { };

        private readonly static HttpClient _systemHttpClient = new HttpClient(_handler);

        public string ErrorMessage { get; set; }

        public bool HasError
        {
            get { return !string.IsNullOrEmpty(this.ErrorMessage); }
        }

        public Dictionary<string, string> Headers { get; set; }

        private HttpRequestMessage AddHeaders(HttpRequestMessage request)
        {
            if (this.Headers != null)
            {
                foreach (var item in this.Headers)
                {
                    if (!request.Headers.Contains(item.Key))
                    {
                        request.Headers.Add(item.Key, item.Value);
                    }
                }
            }
            return request;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpMethod httpMethod, string url, HttpContent content = null, bool disableWait = false)
        {
            HttpResponseMessage toReturn = null;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(httpMethod, url);
                request = AddHeaders(request);
                if (content != null)
                {
                    request.Content = content;
                }
                if (disableWait)
                {
                    toReturn = await _systemHttpClient.SendAsync(request).ConfigureAwait(continueOnCapturedContext: false);
                }
                else
                {
                    toReturn = await _systemHttpClient.SendAsync(request);
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
            return toReturn;
        }

        public async Task<string> HeaderMenu()
        {
            string toReturn = "Error loading menu."; 
            var result = await this.SendAsync(HttpMethod.Get, this.BaseUrl + "/Components/HeaderMenu");
            if (result != null && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                toReturn = await result.Content.ReadAsStringAsync();
            }
            return toReturn;
        }

        public async Task<string> Footer()
        {
            string toReturn = "Error loading footer.";
            var result = await this.SendAsync(HttpMethod.Get, this.BaseUrl + "/Components/Footer");
            if (result != null && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                toReturn = await result.Content.ReadAsStringAsync();
            }
            return toReturn;
        }

        public async Task<string> BlueBox()
        {
            string toReturn = "Error loading blue box.";
            var result = await this.SendAsync(HttpMethod.Get, this.BaseUrl + "/Components/BlueBox");
            if (result != null && result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                toReturn = await result.Content.ReadAsStringAsync();
            }
            return toReturn;
        }
    }
}
