using Newtonsoft.Json;

namespace Kenobi.TripsExtension.Entities.Contracts
{
    public class  WebhooksRequest
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("timeStamp")]
        public string TimeStamp { get; set; }

    }
}