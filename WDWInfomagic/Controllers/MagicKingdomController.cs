using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WDWInfomagic.Models;
using Newtonsoft.Json;

namespace WDWInfomagic.Controllers
{
    public class MagicKingdomController : _BaseController
    {
        private const string scriptName = "magic-kingdom-wait-times.js";

        public JsonResult Index() => JsonAllowed(RunNodeScript(scriptName));

        [Route("mk/waittimes")]
        public ActionResult WaitTimes() => View(GetWaitTimes(scriptName, Park.MagicKingdom));

        [Route("mk/waittimes/json")]
        public JsonResult json() => JsonAllowed(RunNodeScript(scriptName));

    }
}