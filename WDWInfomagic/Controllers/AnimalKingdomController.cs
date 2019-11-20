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
        private const string parkInitials = "ak";

        public JsonResult Index() => JsonAllowed(GetCachedWaitTimes(parkInitials));

        [Route("ak/waittimes")]
        public ActionResult WaitTimes() => View(GetWaitTimes(parkInitials, Park.AnimalKingdom));

        [Route("ak/waittimes/json")]
        public JsonResult json() => JsonAllowed(GetCachedWaitTimes(parkInitials));
    }
}