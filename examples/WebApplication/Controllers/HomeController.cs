using Budgerigar.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public async Task<ActionResult> Over() {
            await Task.Delay(500);
            return View("index");
        }

        public ActionResult Under() {
            return View("index");
        }
    }
}