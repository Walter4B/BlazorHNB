using static System.Net.WebRequestMethods;
using System.Text.Json.Serialization;
using System.Text.Json;
using BlazorAppHNB.Shared;
using Newtonsoft.Json;
using System.Net.Http;
using System.Globalization;

namespace BlazorAppHNB.Client.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Currency>> FatchData()
        {
            List<Currency>? currency = new List<Currency>();

            HttpResponseMessage response = await _httpClient.GetAsync("api/currency");

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                currency = JsonConvert.DeserializeObject<List<Currency>>(responseContent,
                    new JsonSerializerSettings
                    {
                        Culture = new System.Globalization.CultureInfo("fr-FR") //Used for culture pack as it provides required functionality
                    });
                return currency;
            }
            else
            {
                throw new Exception($"The request failed with status code {response.StatusCode}");
            }
        }


    }
}
