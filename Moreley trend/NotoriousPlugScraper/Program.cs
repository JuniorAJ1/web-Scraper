using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using HtmlAgilityPack;

class Program
{
    public static async Task Main(string[] args)
    {
        string url = "https://notorious-plug.com/collections/shoes";

        using HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)"); // Prevents bot detection

        var response = await client.GetStringAsync(url);

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(response);

        // Shopify's product structure
        var shoeNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'ProductItem')]");

        if (shoeNodes == null || shoeNodes.Count == 0)
        {
            Console.WriteLine("No shoes found.");
            return;
        }

        // HashSet to store unique product names
        HashSet<string> seenProducts = new HashSet<string>();

        foreach (var shoe in shoeNodes)
        {
            // Get product name
            var nameNode = shoe.SelectSingleNode(".//h2[@class='ProductItem__Title Heading']/a");
            string name = nameNode != null ? nameNode.InnerText.Trim() : "Unknown";

            // Avoid duplicates
            if (seenProducts.Contains(name)) continue;
            seenProducts.Add(name);

            // Get product price
            var priceNode = shoe.SelectSingleNode(".//span[contains(@class, 'ProductItem__Price')]");
            string price = priceNode != null ? priceNode.InnerText.Trim() : "Price not found";

            // Get product URL
            string link = nameNode != null ? "https://notorious-plug.com" + nameNode.GetAttributeValue("href", "#") : "#";

            Console.WriteLine($"Name: {name}\nPrice: {price}\nLink: {link}\n");
        }
    }
}
