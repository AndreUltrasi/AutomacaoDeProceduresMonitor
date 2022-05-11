using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AutomacaoDeProceduresMonitor.Models;
using X.PagedList;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AutomacaoDeProceduresMonitor.Controllers {
    public class HomeController : Controller {
        public IConfiguration Configuration { get; set; }

        public HomeController() {
            Configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json").Build();
        }

        [HttpGet]
        [Route("index")]
        [Route("")]
        public IActionResult Index(ListProcedureClass model, int? page) {
            ViewBag.Title = "AutomacaoDeProceduresMonitor";
            _ProcedureClassView(model, page);
            return View();
        }

        [HttpPost]
        public PartialViewResult _ProcedureClassView(ListProcedureClass model, int? page, string searchString = null) {
            int pageNumber = page ?? 1;
            model.ProcedureClass = ProcedureClass.GetProcedureClass(Configuration);
            if (model.ProcedureClass != null) {
                var listProcsGuid = model.ProcedureClass.Select(proc => proc.Guid);
                var onePageOfSQLs = listProcsGuid.ToPagedList(pageNumber, 25);
                ViewBag.ListItems = onePageOfSQLs.ToList();
                ViewBag.OnePageOfSQLs = onePageOfSQLs;
            }
            model.Page = pageNumber;
            return PartialView(model);
        }

        public IActionResult About() {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
