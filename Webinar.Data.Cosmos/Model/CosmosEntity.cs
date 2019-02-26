using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Webinar.Data.Cosmos.Model
{
    public abstract class CosmosEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? DeletedOn { get; set; }
    }
}
