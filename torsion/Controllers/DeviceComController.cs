
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace torsion.Controllers
{
    public class DeviceComController : Controller
    {
        //
        // GET: /DeviceCom/
        BLL.SoftInfo webll = new BLL.SoftInfo();
        public ActionResult Index()
        {
            return null;
        }
        
        [HttpPost]
        [AllowAnonymous]
        public string ConDevice()
        {
            
            Model.SoftInfo tsi = new Model.SoftInfo();
           
            tsi.secretKey = "secretKey";
            webll.con_SoftInfo(tsi);
            //WeChat.JSEQdata json = new JavaScriptSerializer().Deserialize<WeChat.JSEQdata>(GlobalController.Get_Post_String(Request));
            return tsi.assess_token;
        }

        
        [HttpPost]
        [AllowAnonymous]
        public string ComDevice()
        {
            Model.SoftInfo si = webll.get_SoftInfo("access_token");
            Model.JsonModel.RecData json = new JavaScriptSerializer().Deserialize<Model.JsonModel.RecData>(GlobalController.Get_Post_String(Request)); ;
            
            //if (si != null)
            //{
            //    si.conStat = 1;
            //    json = new JavaScriptSerializer().Deserialize<Model.JsonModel.RecData>(GlobalController.Get_Post_String(Request));
            //    json.errcode = 1;
            //    si.conStat = 2;
            //    webll.heartbeat(si,json);
            //}
            //else
            //{
            //}
            //if (json == null)
            //{
            //    json = new Model.JsonModel.RecData();
            //    json.errcode = 3;
            //    json.cdata = "json data error";
            //}
            return new JavaScriptSerializer().Serialize(webll.heartbeat(si, json));
        }



    }
}
