using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;


namespace torsion.Controllers
{
    public class WeChatController : Controller
    {
        private static readonly string Token = "weixin";
        //
        // GET: /WeChat/
        public static bool WriteFile(string strpath,string str)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(strpath, true, System.Text.Encoding.UTF8);
                sw.Write(str+"\r\n");
                sw.Flush();
            }
            finally
            {
                sw.Close();
            }
            return true;
        }

        
        //
        // GET: /Weixin/
        [HttpGet]
        public ActionResult Index()
        {
            //Server.MapPath("~/log.txt")
           
            string echoStr = Request.QueryString["echoStr"];
            WriteFile(Server.MapPath("~/log.txt"), "Get:" + Request.ToString());
            if (CheckSignature())
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    return Content(echoStr);
                }
            }
            return null;
            
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult PostIndex()
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            //转换成Byte数组
            byte[] b = new byte[s.Length];
            //读取流
            s.Read(b, 0, (int)s.Length);
            //转化成utf8编码
            string postStr = Encoding.UTF8.GetString(b);

            WriteFile(Server.MapPath("~/log.txt"), "Post:" + postStr);
            return null;
            
        }
        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        private bool CheckSignature()
        {
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            string[] ArrTmp = { Token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(tmpStr, "SHA1");
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Valid()
        {
            string echoStr = Request.QueryString["echoStr"];
            if (CheckSignature())
            {
                if (!string.IsNullOrEmpty(echoStr))
                {
                    Response.Write(echoStr);
                    Response.End();
                }
            }
        }

    }
}
