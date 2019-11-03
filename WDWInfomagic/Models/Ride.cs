using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDWInfomagic.Models
{
    public class Ride
    {
        public RideMeta meta { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public decimal waitTime { get; set; }
        public bool fastPass { get; set; }
        public DateTime lastUpdate { get; set; }
        public string status { get; set; }
    }

    public class RideMeta
    {
        public string area { get; set; }
        public float longitude { get; set; }
        public float latitude { get; set; }
    }
}