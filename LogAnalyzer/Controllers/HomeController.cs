using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogAnalyzer.Analysis;
using LogAnalyzer = LogAnalyzer.Analysis.LogAnalyzer;

namespace LogAnalyzer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReadLog reader;

        public HomeController()
        {
            this.reader = new SimpleLogReader();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            var lines = this.reader.ReadLog(file.InputStream);
            ViewBag.FirstLine = $"FirstLine: {lines.First()}";
            ViewBag.LineCount = $".... {lines.Count} lines .....";
            ViewBag.LastLine  = $"LastLine:  {lines.Last()}";
            return View("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}