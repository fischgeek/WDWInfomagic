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
        private const string parkInitials = "ep";

        public JsonResult Index() => JsonAllowed(GetCachedWaitTimes(parkInitials));

        [Route("ep/waittimes")]
        public ActionResult WaitTimes() => View(GetWaitTimes(parkInitials, Park.Epcot));

        [Route("ep/waittimes/json")]
        public JsonResult json() => JsonAllowed(GetCachedWaitTimes(parkInitials));
    }
}