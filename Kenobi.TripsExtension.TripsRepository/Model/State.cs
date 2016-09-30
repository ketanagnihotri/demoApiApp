﻿using Newtonsoft.Json;

namespace Kenobi.TripsExtension.TripsRepository.Model
{
    public class State
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
