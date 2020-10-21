using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api
{
    public class Context
    {
        [JsonProperty("@version")]
        public string Version { get; set; }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<List<List<List<double>>>> coordinates { get; set; }
    }

    public class Period
    {
        public int number { get; set; }
        public string name { get; set; }
        public string detailedForecast { get; set; }
    }

    public class Properties
    {
        public string zone { get; set; }
        public DateTime updated { get; set; }
        public List<Period> periods { get; set; }
    }

    public class NwsForecast
    {
        [JsonProperty("@context")]
        public Context Context { get; set; }
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }
    }
}