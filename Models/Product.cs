using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Swag.IO.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Maker { get; set; }
        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Rating { get; set; }

        //mapping my Product model so that i can be converted back to Json
                                    // hence "Serialize""
         public override string ToString() => JsonSerializer.Serialize<Product>(this);


        //public override string ToString()

        //{
        //    JsonSerializer.Serialize<Product>(this);
        //}
    }
}
