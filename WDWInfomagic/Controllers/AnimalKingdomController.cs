using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WDWInfomagic.Controllers
{
    public class AnimalKingdomController : _BaseController
    {
        private const string scriptName = "animal-kingdom-wait-times.js";

        public JsonResult Index() => JsonAllowed(RunNodeScript(scriptName));

        [Route("ak/waittimes")]
        public ActionResult WaitTimes() => View(GetWaitTimes(scriptName));

        [Route("ak/waittimes/json")]
        public JsonResult json() => JsonAllowed(RunNodeScript(scriptName));
    }
}