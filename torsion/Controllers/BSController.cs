using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace torsion.Controllers
{
    public class BSController : Controller
    {
        //
        // GET: /BS/


        public ActionResult Home()
        {
      //  http://xue.youdao.com/w?method=tinyEngData&date=2015-07-26
            string ret = string.Empty;

           ret =  GlobalController.Get_Get_String("http://xue.youdao.com/w?method=tinyEngData&date=2015-07-26");
            return Content(ret);
        }
        public ActionResult BIndex()
        {
            BLL.Menus webll = new BLL.Menus();
            torsion.Model.Menus mm = new Model.Menus();
            webll.get_Menus(mm);

            //int ti = 0;
            //string[] tsr = new string[] { "home", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test" };

            //int mn = 4;
            //Models.Menus mm = new Models.Menus();
            //mm.hm = new Models.Menus.HomeMenu[4];
            //mm.hm[0] = new Models.Menus.HomeMenu();
            //mm.hm[0].id = i;
            //mm.hm[0].name = "Menu" + i;
            //mm.hm[0].url = tsr[ti++];
            //mm.hm[0].ico = "icon-user";
            //mm.hm[0].shm = new Models.Menus.SubHomeMenu[i];

            //for (int i = 0; i < mn; i++)
            //{
            //    mm.hm[i] = new Models.Menus.HomeMenu();
            //    mm.hm[i].id = i;
            //    mm.hm[i].name = "Menu" + i;
            //    mm.hm[i].url = tsr[ti++];
            //    switch(i)
            //    {
            //        case 1:
            //            mm.hm[i].ico = "icon-user";
            //            break;
            //        case 2:
            //            mm.hm[i].ico = "icon-download-alt";
            //            break;
            //        case 3:
            //            mm.hm[i].ico = "icon-signal";
            //            break;
            //        default:
            //            mm.hm[i].ico = "icon-camera";
            //            break;
            //    }
            //    mm.hm[i].shm = new Models.Menus.SubHomeMenu[i];
            //    for (int j = 0; j < i; j++)
            //    {
            //        mm.hm[i].shm[j] = new Models.Menus.SubHomeMenu();
            //        mm.hm[i].shm[j].id = i*100+j;
            //        mm.hm[i].shm[j].name = "Sub" + mm.hm[i].shm[j].id;
            //        mm.hm[i].shm[j].url = tsr[ti++];

            //    }
            //}


            return View(mm);
        }
        public ActionResult Index()
        {
            BLL.Menus webll = new BLL.Menus();
            torsion.Model.Menus mm = new Model.Menus();
            webll.get_Menus(mm);

            //int ti = 0;
            //string[] tsr = new string[] { "home", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test", "home1", "attendance/test", "attendance/test", "home/test", "home/test", "home/test" };

            //int mn = 4;
            //Models.Menus mm = new Models.Menus();
            //mm.hm = new Models.Menus.HomeMenu[4];
            //mm.hm[0] = new Models.Menus.HomeMenu();
            //mm.hm[0].id = i;
            //mm.hm[0].name = "Menu" + i;
            //mm.hm[0].url = tsr[ti++];
            //mm.hm[0].ico = "icon-user";
            //mm.hm[0].shm = new Models.Menus.SubHomeMenu[i];

            //for (int i = 0; i < mn; i++)
            //{
            //    mm.hm[i] = new Models.Menus.HomeMenu();
            //    mm.hm[i].id = i;
            //    mm.hm[i].name = "Menu" + i;
            //    mm.hm[i].url = tsr[ti++];
            //    switch(i)
            //    {
            //        case 1:
            //            mm.hm[i].ico = "icon-user";
            //            break;
            //        case 2:
            //            mm.hm[i].ico = "icon-download-alt";
            //            break;
            //        case 3:
            //            mm.hm[i].ico = "icon-signal";
            //            break;
            //        default:
            //            mm.hm[i].ico = "icon-camera";
            //            break;
            //    }
            //    mm.hm[i].shm = new Models.Menus.SubHomeMenu[i];
            //    for (int j = 0; j < i; j++)
            //    {
            //        mm.hm[i].shm[j] = new Models.Menus.SubHomeMenu();
            //        mm.hm[i].shm[j].id = i*100+j;
            //        mm.hm[i].shm[j].name = "Sub" + mm.hm[i].shm[j].id;
            //        mm.hm[i].shm[j].url = tsr[ti++];

            //    }
            //}


            return View(mm);
        }
        public ActionResult Loading()
        {
            return View();
        }
        public ActionResult Test()
        {
            return Content("test");
        }

        public ActionResult UserInfo()
        {
            return View();
        }

        public string UserInfoJson()
        {
            BLL.UserInfo webll = new BLL.UserInfo();
            string search="";
            int deptid=0;
            if (Request.QueryString["search"] != null)
                search = Request.QueryString["search"];
            if (Request.QueryString["deptid"] != null)
                deptid = int.Parse(Request.QueryString["deptid"]);

            DataSet ds = webll.get_UserInfo(search,deptid);
            torsion.Model.UserInfo.BaseInfo[] uibi = new Model.UserInfo.BaseInfo[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                uibi[i] = new Model.UserInfo.BaseInfo();
                torsion.Model.GlfGloFun.get_DataRow(ds.Tables[0], i, uibi[i]);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(uibi);  
        }

        public ActionResult Attendance()
        {
           
            return View();     

        }
        public string AttendanceInfoDS()
        {
            DataTable dc = getAttendance().Tables["DRAttendanceInfo"];
            torsion.Models.EasyUi.DataGridJson.AttendanceInfo tai = new Models.EasyUi.DataGridJson.AttendanceInfo();
            tai.total = dc.Rows.Count;
            tai.rows = new Model.AttendanceSet.DatasetStat.DRAttendanceInfo[dc.Rows.Count];
            for (int i = 0; i < dc.Rows.Count; i++)
            {
                tai.rows[i] = new Model.AttendanceSet.DatasetStat.DRAttendanceInfo();
                torsion.Model.GlfGloFun.get_DataRow(dc, i, tai.rows[i]);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(tai);  
        }
        public string ClassesInfoDS()
        {
            DataTable dc = getAttendance().Tables["DRClassesInfo"];
            torsion.Models.EasyUi.DataGridJson.ClassesInfo tci = new Models.EasyUi.DataGridJson.ClassesInfo();
            tci.total = dc.Rows.Count;
            tci.rows = new Model.AttendanceSet.DatasetStat.DRClassesInfo[dc.Rows.Count];
            for (int i = 0; i < dc.Rows.Count; i++)
            {
                tci.rows[i] = new Model.AttendanceSet.DatasetStat.DRClassesInfo();
                torsion.Model.GlfGloFun.get_DataRow(dc, i, tci.rows[i]);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(tci); 
        }
        public string ResultInfoDS()
        {
            DataTable dc = getAttendance().Tables["DRResultInfo"];
            torsion.Models.EasyUi.DataGridJson.ResultInfo tri = new Models.EasyUi.DataGridJson.ResultInfo();
            tri.total = dc.Rows.Count;
            tri.rows = new Model.AttendanceSet.DatasetStat.DRResultInfo[dc.Rows.Count];
            for (int i = 0; i < dc.Rows.Count; i++)
            {
                tri.rows[i] = new Model.AttendanceSet.DatasetStat.DRResultInfo();
                torsion.Model.GlfGloFun.get_DataRow(dc, i, tri.rows[i]);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(tri); 
        }

        private DataSet getAttendance(Boolean init = false)
        {
            if (Session["AttendanceDS"] == null || init == true)
            {
                torsion.BLL.AttendanceSet webll = new torsion.BLL.AttendanceSet();
                DataSet ds = new DataSet();
                int[] sid = new int[1];
                sid[0] = 0;
                Models.AttendanceDataSet ads = new Models.AttendanceDataSet();

                webll.get_StatDataSet(ds, sid, DateTime.Now.AddMonths(-4), DateTime.Now);
                Session["AttendanceDS"] = ds;
            }
            return Session["AttendanceDS"] as DataSet;
        }
    }
}
