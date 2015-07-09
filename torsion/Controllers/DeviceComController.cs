
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
            BLL.SoftInfo.gl_si[0].conStat = 2;
            BLL.SoftInfo.gl_si[0].cmd = torsion.Model.GlfGloVar.CMD_DEVICELIST;
            //while (BLL.SoftInfo.gl_si[0].conStat < 4)
            //{
            //    System.Threading.Thread.Sleep(500);
            //}

            return BLL.SoftInfo.gl_si[0].recStr;
        }
        [AllowAnonymous]
        public string DeviceList()
        {
           return webll.DeviceList(BLL.SoftInfo.gl_si[0].assess_token).ToString();
            //while (BLL.SoftInfo.gl_si[0].conStat < 4)
            //{
            //    System.Threading.Thread.Sleep(500);
            //}


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
        public string RecDevice()
        {
            try
            {
                Model.JsonModel.RecData jmrd = new Model.JsonModel.RecData();
                jmrd = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.JsonModel.RecData>(GlobalController.Get_Post_String(Request));
                return Newtonsoft.Json.JsonConvert.SerializeObject(webll.RecDevice(Request.QueryString["access_token"],jmrd));

            }
            catch
            {
                return "";
            }
           
        }
        [HttpPost]
        [AllowAnonymous]
        public string ComDevice()
        {
           return Newtonsoft.Json.JsonConvert.SerializeObject(webll.ComDevice(GlobalController.Get_Post_String(Request)));
        }
        [HttpPost]
        [AllowAnonymous]
        public string CommDevice()
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
