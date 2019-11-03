using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDWInfomagic.Models
{
    public class RideViewModel
    {
        public string name { get; set; }
        public string location { get; set; }
        public string status { get; set; }
        public bool active { get; set; }
        public decimal waitTime { get; set; }
        public string lastUpdate { get; set; }
    }
}