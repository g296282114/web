using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace torsion.Controllers
{
    public class BSController : Controller
    {
        //
        // GET: /BS/

        public ActionResult Index()
        {
            int ti = 0;
            string[] tsr = new string[] { "home", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test" };
            int mn = 4;
            Models.Menus mm = new Models.Menus();
            mm.hm = new Models.Menus.HomeMenu[mn];
            for (int i = 0; i < mn; i++)
            {
                mm.hm[i] = new Models.Menus.HomeMenu();
                mm.hm[i].id = i;
                mm.hm[i].name = "Menu" + i;
                mm.hm[i].url = tsr[ti++];
                switch(i)
                {
                    case 1:
                        mm.hm[i].ico = "icon-user";
                        break;
                    case 2:
                        mm.hm[i].ico = "icon-download-alt";
                        break;
                    case 3:
                        mm.hm[i].ico = "icon-signal";
                        break;
                    default:
                        mm.hm[i].ico = "icon-camera";
                        break;
                }
                mm.hm[i].shm = new Models.Menus.SubHomeMenu[i];
                for (int j = 0; j < i; j++)
                {
                    mm.hm[i].shm[j] = new Models.Menus.SubHomeMenu();
                    mm.hm[i].shm[j].id = i*100+j;
                    mm.hm[i].shm[j].name = "Sub" + mm.hm[i].shm[j].id;
                    mm.hm[i].shm[j].url = tsr[ti++];

                }
            }


            return View(mm);
        }
        public ActionResult Test()
        {
            return View();
        }
    }
}
