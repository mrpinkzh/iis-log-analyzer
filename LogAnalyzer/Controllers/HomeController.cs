using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogAnalyzer.Analysis;
using LogAnalyzer.Models;

namespace LogAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReadLog reader;
        private readonly IParseLog parser;
        private readonly IResolveNames resolver;

        public HomeController()
        {
            reader = new SimpleLogReader();
            parser = new W3cExtendedLogFileFormatParser();
            resolver = new SimpleNameResolver();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            AnalysisResult result;
            try
            {
                var lines = reader.ReadLog(file.InputStream);
                var logItems = parser.Parse(lines);
                var clientResults = logItems.GroupBy(item => item.Ip)
                    .Select(group => new ClientResult(@group.Key, @group.Count()))
                    .ToList();
                result = new AnalysisResult(clientResults, "Successfully analyzed the log file.");
            }
            catch (InvalidOperationException exception)
            {
                result = new AnalysisResult(new List<ClientResult>(), exception.Message);
            }

            ViewResult view = View("Result", result);
            return view;
        }

        public ActionResult Resolve(string ip)
        {
            string name = "";
            try
            {
                return Json(new { type = "content", name = resolver.ResolveName(ip) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { type = "error", name =  "name resolve failed" }, JsonRequestBehavior.AllowGet);
            }            
        }
    }
}