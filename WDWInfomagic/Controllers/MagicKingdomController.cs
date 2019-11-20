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
        private const string parkInitials = "mk";

        public JsonResult Index() => JsonAllowed(GetCachedWaitTimes(parkInitials));

        [Route("mk/waittimes")]
        public ActionResult WaitTimes() => View(GetWaitTimes(parkInitials, Park.MagicKingdom));

        [Route("mk/waittimes/json")]
        public JsonResult json() => JsonAllowed(GetCachedWaitTimes(parkInitials));

    }
}