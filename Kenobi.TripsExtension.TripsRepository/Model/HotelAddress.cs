namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class HotelAddress
    {
        public string line1 { get; set; }
        public string line2 { get; set; }
        public City city { get; set; }
        public State state { get; set; }
        public string countryCode { get; set; }
        public string zipCode { get; set; }
    }
}
