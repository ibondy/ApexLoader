using System.Threading.Tasks;

namespace ApexLoader
{
    using System.Net.Http;
    using System.Threading;

    public class CookieDelegateHandler : DelegatingHandler
    {
        //private readonly IHttpContextAccessor httpContextAccessor;
        //private ISession Session;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(AccessCookie))
            {
                request.Headers.Add("Cookie", AccessCookie);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        public CookieDelegateHandler()
        { }

        public string AccessCookie { get; set; }
    }
}