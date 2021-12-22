using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Swag.IO.Services;
using Microsoft.AspNetCore.Hosting;
using Swag.IO.Models;

namespace Swag.IO.Services
{
    public class JsonFileProductService
    {
        public JsonFileProductService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public IEnumerable<Product> GetProducts()
        {
            using var jsonFileReader = File.OpenText(JsonFileName);
            return JsonSerializer.Deserialize<Product[]>(jsonFileReader.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        //adding a rating feature
        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();

            //LINQ = **
            var query = products.First(x => x.Id == productId);

            if (query.Ratings == null)
            {
                query.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = query.Ratings.ToList();
                ratings.Add(rating);
                query.Ratings = ratings.ToArray();
            }

            //writng to the JsonFile/DB
            using var outputStream = File.OpenWrite(JsonFileName);
            JsonSerializer.Serialize<IEnumerable<Product>>(
               new Utf8JsonWriter(outputStream, new JsonWriterOptions
               {
                   SkipValidation = true,
                   Indented = true
               }),
               products
               );

        }
    }
}
