using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Text;
using torsion.Model;
using System.Web.Script.Serialization;

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

        public string gxl(int bb)
        {
            string str = "test"+bb.ToString();
            return str;
        }
        [HttpPost]
        public ActionResult gxx()
        {
        ActionResult rs = RedirectToAction("test","home", new { bb="gxx" });
        return Content("kkk");
           // return Content("ok");
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
        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetPost(string str)
        {
            WeChat.JSEQdata json = new JavaScriptSerializer().Deserialize<WeChat.JSEQdata>(GlobalController.Get_Post_String(Request));
           //WeChat.reJSON rejson = new WeChat.reJSON();
           //rejson.errcode = 0;
           //rejson.errmsg = "Ok";
           //wc.SendStr(json.Checktime);
           return RedirectToAction("SendStr", "WeChat",json); 
           
         //  return new JavaScriptSerializer().Serialize(rejson);
        }

        [HttpPost]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ConEq()
        {
            string postStr = "";
            

            Stream s = Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            postStr = Encoding.UTF8.GetString(b);
            if (!string.IsNullOrEmpty(postStr))
            {

            }  
            
            //WeChat.reJSON rejson = new WeChat.reJSON();
            //rejson.errcode = 0;
            //rejson.errmsg = "Ok";
            //wc.SendStr(json.Checktime);
            return RedirectToAction("SendStr", "WeChat", json);

            //  return new JavaScriptSerializer().Serialize(rejson);
        }


        [HttpPost]
        [AllowAnonymous]
        public string InStaffClasses()
        {

            AttendanceSet.StaffClasses json = new JavaScriptSerializer().Deserialize<AttendanceSet.StaffClasses>(GlobalController.Get_Post_String(Request));
            torsion.BLL.AttendanceSet webll = new torsion.BLL.AttendanceSet();
            WeChat.reJSON rejson = new WeChat.reJSON();
            rejson.errcode = webll.insert_StaffClasses(json);
            return new JavaScriptSerializer().Serialize(rejson);
        }

        [HttpPost]
        [AllowAnonymous]
        public string InAttendanceSetInfo()
        {
            
            AttendanceSet.ClassesInfo json = new JavaScriptSerializer().Deserialize<AttendanceSet.ClassesInfo>(GlobalController.Get_Post_String(Request));
            torsion.BLL.AttendanceSet webll = new torsion.BLL.AttendanceSet();
            WeChat.reJSON rejson = new WeChat.reJSON();
            rejson.errcode = webll.insert_ClassesSetInfo(json);
              return new JavaScriptSerializer().Serialize(rejson);
        }

        [HttpPost]
        [AllowAnonymous]
        public string UpAttendanceSetInfo()
        {
            AttendanceSet.ClassesInfo json = new JavaScriptSerializer().Deserialize<AttendanceSet.ClassesInfo>(GlobalController.Get_Post_String(Request));
            torsion.BLL.AttendanceSet webll = new torsion.BLL.AttendanceSet();
            webll.update_ClassesSetInfo(json);
            WeChat.reJSON rejson = new WeChat.reJSON();
            rejson.errcode = webll.insert_ClassesSetInfo(json);
            return new JavaScriptSerializer().Serialize(rejson);

            //  return new JavaScriptSerializer().Serialize(rejson);
        }

        [HttpPost]
        [AllowAnonymous]
        public string DeAttendanceSetInfo()
        {
            int id = int.Parse(GlobalController.Get_Post_String(Request));
            torsion.BLL.AttendanceSet webll = new torsion.BLL.AttendanceSet();
            WeChat.reJSON rejson = new WeChat.reJSON();
            rejson.errcode = webll.delete_ClassesSetInfo(id);
            return new JavaScriptSerializer().Serialize(rejson);

            //  return new JavaScriptSerializer().Serialize(rejson);
        }

        [HttpPost]
        [AllowAnonymous]
        public string GeAttendanceSetInfo()
        {
            int id = int.Parse(GlobalController.Get_Post_String(Request));
            torsion.Model.AttendanceSet.ClassesInfo ac = new AttendanceSet.ClassesInfo();
            ac.id = id;
            torsion.BLL.AttendanceSet webll = new torsion.BLL.AttendanceSet();
            webll.get_ClassesSetInfo(ac);
            return new JavaScriptSerializer().Serialize(ac);

            //  return new JavaScriptSerializer().Serialize(rejson);
        }

        public string Get_Post_String()
        {
            string postStr="";
            if (Request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);
                if (!string.IsNullOrEmpty(postStr))
                {
                    WeChatController.WriteFile(Server.MapPath("~/log.txt"), "poststr:" + postStr);
                }                 //WriteLog("postStr:" + postStr);
            }
            return "";
        }

        public string test()
        {
            GlobalController.Write_Err("bbbb");
            return "ok";
        }
    }
}
