using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace torsion.Controllers
{
    public class GlfController : Controller
    {

        //model = bll.GetModel(id);
        //
        // GET: /Glf/

        public string Index()
        {
            torsion.BLL.Admin bll = new torsion.BLL.Admin();
            torsion.Model.Admin model = new torsion.Model.Admin();
            DataSet ds = bll.GetList("1=1");
            model = bll.GetModel(1);
            return model.UserName;
        }
        public string Attendance()
        {
            torsion.BLL.Attendance bll = new torsion.BLL.Attendance();
            // torsion.Model.Attendance model = new torsion.Model.Attendance();
            DataSet ds = bll.GetList("1=1");
            string ts = "test<br/>";
            foreach (DataColumn dc in ds.Tables[0].Columns)
                ts += " "+dc.ColumnName;
            ts += "<br/>";
            foreach (DataRow dr in ds.Tables[0].Rows)   ///遍历所有的行
            {
                ts += "<br/>";
                foreach (DataColumn dc in ds.Tables[0].Columns)   //遍历所有的列
                    ts += " " + dr[dc];
            }
           
            return ts;

        }

    }
}
