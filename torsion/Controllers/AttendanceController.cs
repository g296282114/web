using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using torsion.Models;

namespace torsion.Controllers
{
    public class AttendanceController : Controller
    {
        //
        // GET: /Attendance/
        torsion.BLL.Attendance webll = new BLL.Attendance();

        public ActionResult Index()
        {
            return View();
        }
        public string GetData()
        {
            DataSet ds = webll.GetList("");
            AttdanceDataGrid adg = new AttdanceDataGrid();
            adg.total = ds.Tables[0].Rows.Count;
            adg.rows = new AttdanceJson[adg.total];
            int tlsensorid, tlusercode;
            tlsensorid = 0;
            tlusercode = 0;
            for (int i = 0; i < adg.total; i++)
            {
                adg.rows[i] = new AttdanceJson();
                adg.rows[i].name = ds.Tables[0].Rows[i]["name"].ToString();
                adg.rows[i].sensorid = int.Parse(ds.Tables[0].Rows[i]["sensorid"].ToString());
                adg.rows[i].usercode = int.Parse(ds.Tables[0].Rows[i]["usercode"].ToString());
                adg.rows[i].checktime = ds.Tables[0].Rows[i]["checktime"].ToString();
                tlsensorid += adg.rows[i].sensorid;
                tlusercode += adg.rows[i].usercode;

            }

           

            adg.footer = new AttdanceFooter[2];
            for (int i = 0; i < 2; i++)
            {
                adg.footer[i] = new AttdanceFooter();
                adg.footer[i].name = tlsensorid;
                adg.footer[i].sensorid = tlusercode;
                adg.footer[i].usercode = "total";
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(adg);
        }

    }
}
