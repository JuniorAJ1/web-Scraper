# Notorious Plug Shoe Scraper

## Overview
This is a C# .NET console application that scrapes the Notorious Plug website to extract available shoe listings. The extracted data includes the product name, price, and link to the product page.

## How to Build and Run the Application

### Prerequisites
Ensure you have the following installed on your system:
- [.NET SDK](https://dotnet.microsoft.com/en-us/download)
- [NuGet Package Manager](https://www.nuget.org/)
- An internet connection (required to fetch data from the website)

### Installation Steps
1. Clone the repository or download the source code.
   ```sh
   git clone https://github.com/your-repo/NotoriousPlugScraper.git
   cd NotoriousPlugScraper
   ```

2. Restore the necessary dependencies.
   ```sh
   dotnet restore
   ```

3. Build the application.
   ```sh
   dotnet build
   ```

4. Run the application.
   ```sh
   dotnet run
   ```

## Dependencies
This application relies on the following NuGet packages:
- `HtmlAgilityPack` (for parsing HTML and extracting data)

To install dependencies manually, run:
```sh
dotnet add package HtmlAgilityPack
```

## Assumptions & Challenges

### Assumptions
- The website structure remains consistent over time.
- The product information (name, price, and link) is always contained within `div.ProductItem` elements.
- The website does not employ heavy anti-scraping mechanisms (e.g., CAPTCHA or JavaScript rendering for products).

### Challenges Faced
- **Duplicate products appearing multiple times** due to different views (e.g., mobile vs. desktop).
  - **Solution:** Implemented a `HashSet<string>` to track and filter out duplicates.
- **Potential website changes:**
  - Shopify sites can update their HTML structure, which could break the scraper.
  - **Solution:** Ensure the scraper selectors are adaptable and update them when needed.
- **Handling network issues:**
  - **Solution:** Added an HTTP User-Agent header to prevent the request from being blocked.

## Example Output
Upon running the application, the output should look like this:
```
Name: NIKE AIR FORCE 1 LOW SP SLAM JAM BLACK
Price: $200.00
Link: https://notorious-plug.com/collections/shoes/products/nike-air-force-1-low-sp-slam-jam-black

Name: ADIDAS YEEZY BOOST 350 V2
Price: $230.00
Link: https://notorious-plug.com/collections/shoes/products/adidas-yeezy-boost-350-v2
```

