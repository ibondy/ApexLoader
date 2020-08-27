using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApexLoader
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;

    public class CookieDelegateHandler  :DelegatingHandler
    {
        //private readonly IHttpContextAccessor httpContextAccessor;
        //private ISession Session;

        public CookieDelegateHandler()
        { }

        public string AccessCookie { get; set; }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(AccessCookie))
            {
               request.Headers.Add("Cookie", AccessCookie);
            }  

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }


}
