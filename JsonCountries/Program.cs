using Newtonsoft.Json.Linq;

var requestUri = new Uri("https://restcountries.com/v3.1/region/americas");

Console.WriteLine("Request by address: " + requestUri.AbsoluteUri);

using var httpClient = new HttpClient();

try
{
    var responseMessage = (await httpClient.GetAsync(requestUri)).EnsureSuccessStatusCode();

    var countriesText = await responseMessage.Content.ReadAsStringAsync();

    var jCountries = JArray.Parse(countriesText);

    var countries = jCountries.Select(country => new Country(
        Name: (string?)country["name"]?["official"],
        Currencies: country["currencies"]?
            .ToObject<JObject>()?
            .Properties()
            .Select(c => c.Name)
            .ToArray(),
        Population: (int?)country["population"])).ToList();

    Console.WriteLine();
    Console.WriteLine($"Total population in the region: {countries.Sum(p => p.Population)}");

    var currencies = jCountries.SelectTokens("$[*].currencies..name")
        .Select(c => (string?)c)
        .Distinct()
        .OrderBy(c => c)
        .ToArray();

    Console.WriteLine();
    Console.WriteLine("Currencies in the region: ");
    Console.WriteLine(string.Join(Environment.NewLine, currencies));
}
catch (HttpRequestException e)
{
    Console.WriteLine("Failed to request:");
    Console.WriteLine(e.Message);
}

internal record Country(string? Name, string[]? Currencies, int? Population);