#region Copyright

// MIT License
// 
// Copyright (c) 2020 Ivan Bondy
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion

namespace ApexLoader
{
    #region using

    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ApexConfig;
    using ApexLog;
    using ApexStatus;
    using ApexTLog;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    #endregion

    /// <summary>
    ///     Communicates with Apex via RESTApi
    /// </summary>
    public class ApexService
    {
        private readonly IConfiguration _configuration;
        private readonly CookieDelegateHandler _cookieDelegateHandler;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApexService> _logger;
        private Config _apexConfig;

        public ApexService(HttpClient httpClient, IConfiguration configuration, CookieDelegateHandler cookieDelegateHandler, ILogger<ApexService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cookieDelegateHandler = cookieDelegateHandler;
            _logger = logger;
        }

        /// <summary>
        ///     Returns conguration from Apex
        /// </summary>
        /// <returns> </returns>
        public async Task<ConfigRoot> GetConfig()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/config");

            var result = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, null, "Getting config data from Apex");
                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.Log(LogLevel.Information, null, "Received config data from Apex");
                return JsonSerializer.Deserialize<ConfigRoot>(content);
            }

            return null;
        }

        /// <summary>
        ///     Returns log data from Apex
        /// </summary>
        /// <returns> </returns>
        public async Task<LogRoot> GetLog()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            ///TODO-Implement specific days /rest/ilog?days=1&sdate=200825&_=1598501258383 HTTP/1.1
            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/ilog");

            var result = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, null, "Getting log data from Apex");
                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.Log(LogLevel.Information, null, "Received log data from Apex");
                return JsonSerializer.Deserialize<LogRoot>(content);
            }

            return null;
        }

        /// <summary>
        ///     Returns Tlog data from Apex
        /// </summary>
        /// <returns> </returns>
        public async Task<Root> GetTLog()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            ///TODO-Implement specific days /rest/ilog?days=1&sdate=200825&_=1598501258383 HTTP/1.1
            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/tlog");

            var result = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, null, "Getting Tlog data from Apex");
                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.Log(LogLevel.Information, null, "Received Tlog data from Apex");
                return JsonSerializer.Deserialize<Root>(content);
            }

            return null;
        }

        /// <summary>
        ///     Returns Olog data from Apex
        /// </summary>
        /// <returns> </returns>
        public async Task<ApexOlog.Root> GetOLog()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            ///TODO-Implement specific days /rest/ilog?days=1&sdate=200825&_=1598501258383 HTTP/1.1
            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/olog");

            var result = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, null, "Getting Olog data from Apex");
                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.Log(LogLevel.Information, null, "Received Olog data from Apex");
                return JsonSerializer.Deserialize<ApexOlog.Root>(content);
            }

            return null;
        }

        /// <summary>
        ///     Returns Dlog data from Apex
        /// </summary>
        /// <returns> </returns>
        public async Task<ApexDLog.Root> GetDLog()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            ///TODO-Implement specific days /rest/ilog?days=1&sdate=200825&_=1598501258383 HTTP/1.1
            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/dlog");

            var result = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, null, "Getting Dlog data from Apex");
                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.Log(LogLevel.Information, null, "Received Dlog data from Apex");
                return JsonSerializer.Deserialize<ApexDLog.Root>(content);
            }

            return null;
        }

        /// <summary>
        ///     Gets login cookie
        /// </summary>
        /// <returns> </returns>
        public async Task GetLogin()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/login");
            var login = new ApexLoginContext {Password = _apexConfig.Password, User = _apexConfig.User};
            var payload = JsonSerializer.Serialize(login);

            _logger.Log(LogLevel.Information, null, "Login to Apex");
            var result = await _httpClient.PostAsync(uri, new StringContent(payload)).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, null, "Login into Apex successful");
                if (result.Headers.Contains("Set-Cookie"))
                {
                    _cookieDelegateHandler.AccessCookie = result.Headers.First(p => p.Key == "Set-Cookie").Value.First();
                }
            }
        }

        /// <summary>
        ///     Returns status from Apex
        /// </summary>
        /// <returns> </returns>
        public async Task<StatusRoot> GetStatus()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/status");

            var result = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information, null, "Getting status data from Apex");
                var content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.Log(LogLevel.Information, null, "Received status data from Apex");
                return JsonSerializer.Deserialize<StatusRoot>(content);
            }

            return null;
        }

        /// <summary>
        ///     Sets Apex connection context
        /// </summary>
        /// <param name="apexConfig"> </param>
        public void SetContext(Config apexConfig)
        {
            _apexConfig = apexConfig;
        }
    }
}