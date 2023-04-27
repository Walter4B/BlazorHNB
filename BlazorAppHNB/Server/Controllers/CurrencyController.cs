using BlazorAppHNB.Client.Authentication;
using BlazorAppHNB.Server.Proxy;
using BlazorAppHNB.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;

namespace BlazorAppHNB.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private List<Currency>? currency = new List<Currency>();

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
        }

        [HttpOptions]
        public IActionResult PreflightRoute()
        {
            return NoContent();
        }

        [HttpGet]
        public async Task<string> Get()
        {
            try
            {
                var httpClient = new CorsProxyHandler()
                {
                    InnerHandler = new HttpClientHandler()
                };
                var client = new HttpClient(httpClient);
                HttpResponseMessage response = await client.GetAsync("https://api.hnb.hr/tecajn-eur/v3");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    throw new Exception($"The request failed with status code {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
