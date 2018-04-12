// This code was generated by a cloud-generators-net tool 
// (see https://github.com/Kentico/cloud-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using KenticoCloud.ContentManagement.Models.Items;
using KenticoCloud.ContentManagement.Models.Assets;

namespace KenticoCloudImport.Models
{
    public partial class Dish
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("dish_colour")]
        public IEnumerable<TaxonomyTermIdentifier> DishColour { get; set; }
        [JsonProperty("flags")]
        public IEnumerable<TaxonomyTermIdentifier> Flags { get; set; }
        [JsonProperty("dietary_requirement")]
        public IEnumerable<TaxonomyTermIdentifier> DietaryRequirement { get; set; }
        [JsonProperty("calories")]
        public decimal? Calories { get; set; }
        [JsonProperty("ingredients")]
        public string Ingredients { get; set; }
        [JsonProperty("allergens")]
        public string Allergens { get; set; }
    }
}