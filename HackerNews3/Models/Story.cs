using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HackerNews3.Models
{
    public class Story
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("by")]
        public string Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}