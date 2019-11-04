using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WDWInfomagic.Models
{
    public class WaitTimesViewModel
    {
        public IEnumerable<RideViewModel> Rides { get; set; }
        public string MKActive { get; set; }
        public string AKActive { get; set; }
        public string EPActive { get; set; }
        public string HSActive { get; set; }
    }
}