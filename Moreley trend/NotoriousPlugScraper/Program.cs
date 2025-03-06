using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using HtmlAgilityPack;

class Program
{
    public static async Task Main(string[] args)
    {
        string url = "https://notorious-plug.com/collections/shoes";
        List<Shoe> shoesList = new List<Shoe>();
        HashSet<string> seenShoes = new HashSet<string>(); // Prevent duplicates

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

        var response = await client.GetStringAsync(url);
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(response);

        var shoeNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'ProductItem')]");

        if (shoeNodes == null || shoeNodes.Count == 0)
        {
            Console.WriteLine("No shoes found.");
            return;
        }

        foreach (var shoe in shoeNodes)
        {
            var nameNode = shoe.SelectSingleNode(".//h2[@class='ProductItem__Title Heading']/a");
            var priceNode = shoe.SelectSingleNode(".//span[contains(@class, 'ProductItem__Price')]");

            string name = nameNode != null ? nameNode.InnerText.Trim() : "Unknown";
            string price = priceNode != null ? priceNode.InnerText.Trim() : "Price not found";
            string link = nameNode != null ? "https://notorious-plug.com" + nameNode.GetAttributeValue("href", "#") : "#";

            // Avoid duplicate entries
            if (!seenShoes.Contains(name))
            {
                seenShoes.Add(name); // Mark as seen
                shoesList.Add(new Shoe { Name = name, Price = price, Link = link });

                Console.WriteLine($"Name: {name}\nPrice: {price}\nLink: {link}\n");
            }
        }

        // Convert list to JSON with proper encoding
        string json = JsonSerializer.Serialize(shoesList, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Fixes Unicode issue
        });

        await File.WriteAllTextAsync("shoes.json", json);

        Console.WriteLine("Data successfully saved to shoes.json!");
    }
}

// Shoe model class
class Shoe
{
    public string Name { get; set; }
    public string Price { get; set; }
    public string Link { get; set; }
}
