using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WDWInfomagic.Controllers
{
    public class HollywoodStudiosController : _BaseController
    {
        private const string scriptName = "hollywood-studios-wait-times.js";

        public JsonResult Index() => JsonAllowed(RunNodeScript(scriptName));

        [Route("hs/waittimes")]
        public ActionResult WaitTimes() => View(GetWaitTimes(scriptName, Park.HollywoodStudios));

        [Route("hs/waittimes/json")]
        public JsonResult json() => JsonAllowed(RunNodeScript(scriptName));
    }
}