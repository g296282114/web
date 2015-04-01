using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using torsion.Models;

namespace torsion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PersonAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PersonAdd(Person_Model model)
        {
            //一行代码判断验证是否通过
            if (ModelState.IsValid)
            {
                return Redirect("/Home/PersonManager");
            }
            return View();
        }
    }
}
