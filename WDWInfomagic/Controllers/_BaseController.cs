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
        private const string scriptBase = @"e:\utils\themeparks\wdw";
        private const string node = @"C:\Program Files\nodejs\node.exe";
        private const string workingDir = @"C:\Program Files\nodejs\";

        public string RunNodeScript(string script)
        {
            if (!System.IO.File.Exists(node)) {
                return "Node not found on server. Looking in C:\\Program Files\\nodejs\\node.exe";
            }
            Process compiler = new Process();
            compiler.StartInfo.FileName = node;
            compiler.StartInfo.Arguments = $@"{scriptBase}\{script}";
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.WorkingDirectory = workingDir;
            compiler.Start();
            var json = compiler.StandardOutput.ReadToEnd();
            compiler.WaitForExit();
            return json;
        }

        public WaitTimesViewModel GetWaitTimes(string scriptName, Park park)
        {
            var rawTimes = RunNodeScript(scriptName);
            if (string.IsNullOrEmpty(rawTimes)) {
                return null;
            }
            List<Ride> rides = JsonConvert.DeserializeObject<List<Ride>>(rawTimes);
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
            WaitTimesViewModel waitTimesViewModel = new WaitTimesViewModel() {
                Rides = outRides
                , ParkName = park.ToString()
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