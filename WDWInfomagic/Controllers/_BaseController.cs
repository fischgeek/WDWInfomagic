using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WDWInfomagic.Controllers
{
    public class _BaseController : Controller
    {
        public string scriptBase
        {
            get {
                return $@"e:\utils\themeparks\wdw";
            }
        }
        private const string node = @"C:\Program Files\nodejs\node.exe";
        private const string workingDir = @"C:\Program Files\nodejs\";

        public string RunNodeScript(string script)
        {
            if (!System.IO.File.Exists(node)) {
                return "Node not found on server. Looking in C:\\Program Files\\nodejs\\node.exe";
            }
            Process compiler = new Process();
            compiler.StartInfo.FileName = node;
            compiler.StartInfo.Arguments = script;
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.WorkingDirectory = workingDir;
            compiler.Start();
            var json = compiler.StandardOutput.ReadToEnd();
            compiler.WaitForExit();
            return json;
        }
    }
}