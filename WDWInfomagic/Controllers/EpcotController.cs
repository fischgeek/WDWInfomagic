using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WDWInfomagic.Controllers
{
    public class EpcotController : _BaseController
    {
        private const string scriptName = "epcot-wait-times.js";

        public JsonResult Index() => JsonAllowed(RunNodeScript(scriptName));

        [Route("ep/waittimes")]
        public ActionResult WaitTimes() => View(GetWaitTimes(scriptName));

        [Route("ep/waittimes/json")]
        public JsonResult json() => JsonAllowed(RunNodeScript(scriptName));
    }
}