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

        public IEnumerable<RideViewModel> GetWaitTimes(string scriptName)   
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
                    , lastUpdate = r.lastUpdate.AddHours(-4).ToShortTimeString()
                    , color = r.active ? r.waitTime < 30 ? "green" : r.waitTime < 60 ? "orange" : "red" : "black"
                };
            return outRides;
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
                .Replace("â€“", "-")
                .Replace("â€™", "'")
                .Replace("â„¢", "");
        }
    }
}