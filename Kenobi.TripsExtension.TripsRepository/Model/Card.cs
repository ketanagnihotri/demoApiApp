using System;
using System.Globalization;
using Kenobi.TripsExtension.TripsRepository.Util;
using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class Card
    {
        private string _expirydate;
        private string _nameOnCard;

        private Card()
        {
            if (string.IsNullOrEmpty(_expirydate))
                _expirydate = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            if (string.IsNullOrEmpty(_nameOnCard))
                _nameOnCard = Config.CreditcardNameonCard;
        }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("issuedBy")]
        public string IssuedBy { get; set; }

        [JsonProperty("expiryDate")]
        public string ExpiryDate
        {

            get
            {

                return _expirydate;
            }
            set
            {

                _expirydate = value;
            }

        }

        [JsonProperty("nameOnCard")]
        public string NameOnCard
        {

            get
            {
                return _nameOnCard;

            }
            set
            {
                _nameOnCard = value;
            }
        }


    }
}
