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
        private const string parkInitials = "hs";

        public JsonResult Index() => JsonAllowed(GetCachedWaitTimes(parkInitials));

        [Route("hs/waittimes")]
        public ActionResult WaitTimes() => View(GetWaitTimes(parkInitials, Park.HollywoodStudios));

        [Route("hs/waittimes/json")]
        public JsonResult json() => JsonAllowed(GetCachedWaitTimes(parkInitials));
    }
}