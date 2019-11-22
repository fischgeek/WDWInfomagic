using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDWInfomagic.Models;

namespace WDWInfomagic.Controllers
{
    public class _BaseController : Controller
    {
        private const string dataDir = @"e:\utils\themeparks\wdw\json";
        public string GetCachedWaitTimes(string parkInitials)
        {
            var dataFile = $"{dataDir}\\{parkInitials}.json";
            if (!System.IO.File.Exists(dataFile)) {
                return $"Data file not found for {parkInitials}";
            }
            var json = System.IO.File.ReadAllText(dataFile);
            return json;
        }

        public string GetCachedOpeningTimes(string parkInitials)
        {
            var dataFile = $"{dataDir}\\{parkInitials}_hours.json";
            if (!System.IO.File.Exists(dataFile)) {
                return $"Hours file not found for {parkInitials}";
            }
            var json = System.IO.File.ReadAllText(dataFile);
            return json;
        }

        public WaitTimesViewModel GetWaitTimes(string parkInitials, Park park)
        {
            var rawTimes = GetCachedWaitTimes(parkInitials);
            var rawHours = GetCachedOpeningTimes(parkInitials);
            if (string.IsNullOrEmpty(rawTimes)) {
                return null;
            }
            List<Ride> rides = JsonConvert.DeserializeObject<List<Ride>>(rawTimes);
            List<OpeningTime> times = JsonConvert.DeserializeObject<List<OpeningTime>>(rawHours);
            var outRides =
                from r in rides
                orderby r.waitTime
                orderby r.active descending
                select new RideViewModel {
                    name = ReplaceEntities(r.name)
                    , active = r.active
                    , location = ReplaceEntities(r.meta.area)
                    , status = r.status
                    , waitTime = r.waitTime
                    , lastUpdate = r.lastUpdate.ToLocalTime().AddHours(1).ToShortTimeString()
                    , color = r.active ? r.waitTime < 30 ? "green" : r.waitTime < 60 ? "orange" : "red" : "black"
                };
            var xx = times.Where(x => x.date == DateTime.Now.Date).ToList();
            var time = new OpeningTime();
            if (xx.Count > 0) {
                time = xx.First();
            }

            WaitTimesViewModel waitTimesViewModel = new WaitTimesViewModel() {
                Rides = outRides
                , ParkName = park.ToString()
                , ParkHours = $"{time.openingTime.ToShortTimeString()} - {time.closingTime.ToShortTimeString()}"
                , MKActive = park == Park.MagicKingdom ? "active-park" : ""
                , AKActive = park == Park.AnimalKingdom ? "active-park" : ""
                , EPActive = park == Park.Epcot ? "active-park" : ""
                , HSActive = park == Park.HollywoodStudios ? "active-park" : ""
            };
            return waitTimesViewModel;
        }

        public JsonResult JsonAllowed(object data)
        {
            JsonResult d = this.Json(data, JsonRequestBehavior.AllowGet);
            return d;
        }

        public string ReplaceEntities(string str)
        {
            if (str == null) {
                return "";
            }
            return str
                .Replace("\"it's a small world\"", "It's a Small World")
                .Replace("â€“", "-")
                .Replace("ΓÇô", "-")
                .Replace("Γäó", "")
                .Replace("ΓÇÖ", "'")
                .Replace("â€™", "'")
                .Replace("â„¢", "");
        }
    }

    public enum Park
    {
        MagicKingdom
        , AnimalKingdom
        , Epcot
        , HollywoodStudios
    }
}