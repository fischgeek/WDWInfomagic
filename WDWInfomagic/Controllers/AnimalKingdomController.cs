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
        public string scriptPath
        {
            get {
                return $@"{scriptBase}\animal-kingdom-wait-times.js";
            }
        }
        public JsonResult Index() => JsonAllowed(RunNodeScript(scriptPath));
    }
}