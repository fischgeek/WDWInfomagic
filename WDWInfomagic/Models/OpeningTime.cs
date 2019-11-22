using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDWInfomagic.Models
{
    public class OpeningTime
    {
        public DateTime date { get; set; }
        public DateTime openingTime { get; set; }
        public DateTime closingTime { get; set; }
        public string type { get; set; }
        public Special[] special { get; set; }
    }

    public class Special
    {
        public DateTime openingTime { get; set; }
        public DateTime closingTime { get; set; }
        public string type { get; set; }
    }
}