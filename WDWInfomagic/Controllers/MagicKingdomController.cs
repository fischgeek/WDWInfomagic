using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WDWInfomagic.Controllers
{
    public class MagicKingdomController : _BaseController
    {
        public string scriptPath
        {
            get {
                return $"{scriptBase}/mk-wait-times.js";
            }
        }
        public ActionResult Index() => new ContentResult { Content = RunNodeScript(scriptPath) };
    }
}