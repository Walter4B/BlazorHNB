using BlazorAppHNB.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BlazorAppHNB.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Currency> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Currency
            {
                broj_tecajnice = Random.Shared.Next(-20, 55),
                datum_primjene = DateTime.Now.AddDays(index),
                drzava = "Poljska",
                drzava_iso = "POL",
                sifra_valute = Random.Shared.Next(-20, 55),
                valuta = "Name",
                kupovni_tecaj = Random.Shared.Next(-20, 55),
                srednji_tecaj = Random.Shared.Next(-20, 55),
                prodajni_tecaj = Random.Shared.Next(-20, 55),
            })
            .ToArray();
        }
    }
}
