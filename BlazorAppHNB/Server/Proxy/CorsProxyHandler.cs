namespace BlazorAppHNB.Server.Proxy
{
    public class CorsProxyHandler : DelegatingHandler
    {
        private const string ProxyUrl = "https://cors-anywhere.herokuapp.com/";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");

            request.RequestUri = new Uri(ProxyUrl + request.RequestUri);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
