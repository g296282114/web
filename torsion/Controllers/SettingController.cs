using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace torsion.Controllers
{
    public class SettingController : Controller
    {
        //
        // GET: /Setting/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExtendField()
        {
            torsion.BLL.UserInfo dal = new BLL.UserInfo();

            return View(dal.ListExtend());
        }
    }
}
