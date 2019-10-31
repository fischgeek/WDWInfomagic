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
        public string scriptPath
        {
            get {
                return $@"{scriptBase}\hollywood-studios-wait-times.js";
            }
        }
        public ActionResult Index() => JsonAllowed(RunNodeScript(scriptPath));
    }
}