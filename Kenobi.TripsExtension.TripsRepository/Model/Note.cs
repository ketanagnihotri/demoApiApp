using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public class Note
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("creatorId")]
        public string CreatorId { get; set; }

        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; }
    }
}
