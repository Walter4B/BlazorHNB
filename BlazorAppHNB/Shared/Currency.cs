namespace BlazorAppHNB.Shared
{
    public class Currency
    {
        public int broj_tecajnice { get; set; }
        public DateTime datum_primjene { get; set; }
        public string? drzava { get; set; }
        public string? drzava_iso { get; set; }
        public int sifra_valute { get; set; }
        public string? valuta { get; set; }
        public decimal kupovni_tecaj { get; set; }
        public decimal srednji_tecaj { get; set; }
        public decimal prodajni_tecaj { get; set; }
    }
}
