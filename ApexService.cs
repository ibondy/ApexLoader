namespace ApexLoader
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ApexConfig;
    using Microsoft.Extensions.Configuration;
    using ApexLog;
    using ApexStatus;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using JsonSerializer = System.Text.Json.JsonSerializer;

    public class ApexService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly CookieDelegateHandler _cookieDelegateHandler;
        private readonly ILogger<ApexService> _logger;
        private Config _apexConfig;

        public ApexService(HttpClient httpClient, IConfiguration configuration, CookieDelegateHandler cookieDelegateHandler, ILogger<ApexService> logger )
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _cookieDelegateHandler = cookieDelegateHandler;
            _logger = logger;
        }

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
           

            _logger.Log(LogLevel.Information,null,"Login to Apex");
            var result = await _httpClient.PostAsync(uri, new StringContent(payload)).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information,null,"Login into Apex successful");
                if (result.Headers.Contains("Set-Cookie"))
                {
                    _cookieDelegateHandler.AccessCookie = result.Headers.First(p => p.Key == "Set-Cookie").Value.First();
                }
            }
        }

        public void SetContext(Config apexConfig)
        {
            _apexConfig = apexConfig;
        }

        public async Task<LogRoot> GetLog()
        {
            if (_apexConfig == null)
            {
                throw new ArgumentNullException("Context is not set");
            }

            var baseUri = string.Format($" {_apexConfig.Url}:{_apexConfig.Port}");
            var uri = string.Format($"{baseUri}/rest/ilog");

            var result = await _httpClient.GetAsync(uri).ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                _logger.Log(LogLevel.Information,null,"Getting log data from Apex");
               var c = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
               _logger.Log(LogLevel.Information,null,"Received log data from Apex");
               return JsonConvert.DeserializeObject<LogRoot>(c);
            }

            return null;
        }

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
                _logger.Log(LogLevel.Information,null,"Getting status data from Apex");
                var c = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.Log(LogLevel.Information,null,"Received status data from Apex");
                return JsonConvert.DeserializeObject<StatusRoot>(c);
            }

            return null;
        }

        public async Task<ConfigRoot>  GetConfig()
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
                _logger.Log(LogLevel.Information,null,"Getting config data from Apex");
                var c = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                _logger.Log(LogLevel.Information,null,"Received config data from Apex");
                return JsonConvert.DeserializeObject<ConfigRoot>(c);
            }

            return null;
        }
    }
}