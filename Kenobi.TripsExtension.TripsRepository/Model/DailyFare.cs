using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
   public  class DailyFare
    {

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }
    }
}
