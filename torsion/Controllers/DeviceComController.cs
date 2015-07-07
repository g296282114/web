
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
        [AllowAnonymous]
        public string ShowDevice()
        {
            return torsion.Model.GlfGloVar.TEST_STRING;
        }

        [AllowAnonymous]
        public string TestDevice()
        {
            BLL.SoftInfo.gl_si[0].sendStr = "serversend";
            BLL.SoftInfo.gl_si[0].cmd = 0x90;
            
            return "ok";
        }

        [HttpPost]
        [AllowAnonymous]
        public string ConDevice()
        {         
            Model.SoftInfo tsi = new Model.SoftInfo();

            tsi.secretKey = GlobalController.Get_Post_String(Request);
            webll.con_SoftInfo(tsi);
            if (tsi.assess_token == null) return Model.GlfGloVar.ERRSTR_UNREGISTERED;
            //WeChat.JSEQdata json = new JavaScriptSerializer().Deserialize<WeChat.JSEQdata>(GlobalController.Get_Post_String(Request));
            return tsi.assess_token;
        }


        [HttpPost]
        [AllowAnonymous]
        public string ComDevice()
        {

            Model.JsonModel.RecData rjmrd = new Model.JsonModel.RecData();
            try
            {
                Model.JsonModel.RecData jmrd = new JavaScriptSerializer().Deserialize<Model.JsonModel.RecData>(GlobalController.Get_Post_String(Request));
                try
                {
                    webll.heartbeat(Request.QueryString["access_token"], jmrd, rjmrd);
                }
                catch(Exception e)
                {
                    rjmrd.cmd = torsion.Model.GlfGloVar.CMD_ERRCODE_OTHER;
                    rjmrd.cdata = e.Message;
                }
                
            }
            catch
            {
                rjmrd.cmd = torsion.Model.GlfGloVar.CMD_ERRCODE_JSONFORMAT;
            }
            torsion.Model.GlfGloVar.TEST_STRING += rjmrd.cmd.ToString() + "    " + rjmrd.cdata + "<br/>";
            
  
            return new JavaScriptSerializer().Serialize(rjmrd);
        }



    }
}
