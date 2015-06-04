using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Xml;
using torsion.Models;
using System.Net;
using System.Web.Script.Serialization;


namespace torsion.Controllers
{
    public class WeChatController : Controller
    {
        private static readonly string Token = "weixin";
        private string finalstr;
        private static readonly string acToken = "aizW37YFylAkQeLglFrZ-cF6wGvlW433hwFB0S2sg0kwsj3FjZN-PygRoJ-kFFM57jlURMFKEvZjuH5OIlwOpairLGCGCNoZlwwgZcu3mzs";
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
        public void PostIndex()
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            //转换成Byte数组
            byte[] b = new byte[s.Length];
            //读取流
            s.Read(b, 0, (int)s.Length);
            //转化成utf8编码
            string postStr = Encoding.UTF8.GetString(b);

            WriteFile(Server.MapPath("~/log.txt"), "Post:" + postStr);
            Handle(postStr);
            
        }


        public ActionResult SendGlf()
        {
            string strcon = "{\"touser\":\"oCOhut6m5Tt-4Z_yZ5hMhpjsS5IM\",\"msgtype\":\"text\",\"text\":{\"content\":\"Hello World\"}}";
            System.Net.HttpWebRequest httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token=" + acToken);
            httpWebRequest.Method = "POST";
            byte[] postBytes = Encoding.UTF8.GetBytes(strcon);
            //httpWebRequest.ContentType = "text/xml";
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            // httpWebRequest.ContentLength = Encoding.UTF8.GetByteCount(data);
            //strJson为json字符串 
            Stream stream = httpWebRequest.GetRequestStream();
            stream.Write(postBytes, 0, postBytes.Length);
            stream.Close();
            //发送完毕，接受返回值 
            var response = httpWebRequest.GetResponse();
            Stream streamResponse = response.GetResponseStream();
            StreamReader streamRead = new StreamReader(streamResponse);
            String responseString = streamRead.ReadToEnd();
            WriteFile(Server.MapPath("~/log.txt"), "restr:" + responseString);
            return Content(responseString); 
        }

        private string PostData(string url, string postData)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);

            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();

            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }

   

        private string GetData(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
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

        public void Handle(string postStr)
        {
            //封装请求类  
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(postStr);
            XmlElement rootElement = doc.DocumentElement;
            //MsgType  
            XmlNode MsgType = rootElement.SelectSingleNode("MsgType");
            //接收的值--->接收消息类(也称为消息推送)  
            WC_RequestXml requestXML = new WC_RequestXml();
            requestXML.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
            requestXML.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;
            requestXML.CreateTime = rootElement.SelectSingleNode("CreateTime").InnerText;
            requestXML.MsgType = MsgType.InnerText;

            //根据不同的类型进行不同的处理  
            switch (requestXML.MsgType)
            {
                case "text": //文本消息  
                    requestXML.Content = rootElement.SelectSingleNode("Content").InnerText;
                    break;
                case "image": //图片  
                    requestXML.PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText;
                    break;
                case "location": //位置  
                    requestXML.Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                    requestXML.Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                    requestXML.Scale = rootElement.SelectSingleNode("Scale").InnerText;
                    requestXML.Label = rootElement.SelectSingleNode("Label").InnerText;
                    break;
                case "link": //链接  
                    break;
                case "event": //事件推送 支持V4.5+  
                    break;
            }

            //消息回复  
            ResponseMsg(requestXML);
        }

        private void ResponseMsg(WC_RequestXml requestXML)
        {
            try
            {
                string resxml = "";
                //主要是调用数据库进行关键词匹配自动回复内容，可以根据自己的业务情况编写。  
                //1.通常有，没有匹配任何指令时，返回帮助信息  
                // AutoResponse mi = new AutoResponse(requestXML.Content, requestXML.FromUserName);

                switch (requestXML.MsgType)
                {
                    case "text":
                        //在这里执行一系列操作，从而实现自动回复内容.   
                        // WriteLog(requestXML.Content);
                        resxml = "<xml><ToUserName><![CDATA[" + requestXML.FromUserName + "]]></ToUserName><FromUserName><![CDATA[" + requestXML.ToUserName + "]]></FromUserName><CreateTime>" + ConvertDateTimeInt(DateTime.Now) + "</CreateTime><MsgType><![CDATA[text]]></MsgType><Content><![CDATA[" + requestXML.Content + "]]></Content></xml>";
                        // resxml += mi.GetRePic(requestXML.FromUserName);

                        break;
                    case "location":

                        break;

                    case "image":
                        //图文混合的消息 具体格式请见官方API“回复图文消息”   
                        break;
                }

                System.Web.HttpContext.Current.Response.Write(resxml);
                //  WriteToDB(requestXML, resxml, mi.pid);
            }
            catch (Exception E)
            {
                //WriteTxt("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());  
                //wx_logs.MyInsert("异常：" + ex.Message + "Struck:" + ex.StackTrace.ToString());  
            }
        }

        /// <summary>
        /// 将Unix时间戳转换为DateTime类型时间
        /// </summary>
        /// <param name="d">double 型数字</param>
        /// <returns>DateTime</returns>
        public static System.DateTime ConvertIntDateTime(double d)
        {
            System.DateTime time = System.DateTime.MinValue;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddSeconds(d);
            return time;
        }
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>double</returns>
        public static double ConvertDateTimeInt(System.DateTime time)
        {
            double intResult = 0;
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return intResult;
        }

        public ActionResult get_acctoken()
        {
            string gettokenurl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=wx7bc500cbc32c3e15&secret=74f817b2d21b261d891745a4879244e6";

            //generate http request
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(gettokenurl);
            //use GET method to get url's html
            req.Method = "GET";
            //use request to get response
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            //otherwise will return messy code
            //  Encoding htmlEncoding = Encoding.GetEncoding(htmlCharset);
            StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8);
            //read out the returned html
            string respHtml = sr.ReadToEnd();
            //上边为读取json数据，下边就是解析了
            //传说中的反序列化
            //另外，为了方便我在model里新建了一个Access_Token实体
            Access_Token j2 = new JavaScriptSerializer().Deserialize<Access_Token>(respHtml);
            //acctoken是一个静态变量，全局的就是。
            //当然你也可把他写入文件或者数据库
            finalstr = j2.access_token;

            WriteFile(Server.MapPath("~/log.txt"), "token:" + finalstr);

            return Content(finalstr);

        }

        public ActionResult creat_mymenu() 
        {
            finalstr = "Tm8WCsRvHCrsrJpejES-BDlZqDLSiXvRyYjQ_TyifZ_61LxR7eSxf57gwvSTp8ws8bYsEnMqHwoErcRZFIeMR0HLO8-1lw5bm6Bwx0Bx9Ig";
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + 
                finalstr; 
            string data = "{\"button\":[{\"type\":\"click\",\"name\":\"今日歌曲\",\"key\":\"V1001_TODAY_MUSIC\"},{\"type\":\"click\",\"name\":\"歌手简介\",\"key\":\"V1001_TODAY_SINGER\"},{\"name\":\"菜单\",\"sub_button\":[{\"type\":\"view\",\"name\":\"搜索\",\"url\":\"http://www.soso.com/\"},{\"type\":\"view\",\"name\":\"视频\",\"url\":\"http://v.qq.com/\"},{\"type\":\"click\",\"name\":\"赞一下我们\",\"key\":\"V1001_GOOD\"}]}]}";
            System.Net.HttpWebRequest httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            httpWebRequest.Method = "POST"; 
            byte[] postBytes = Encoding.UTF8.GetBytes(data); 
            //httpWebRequest.ContentType = "text/xml";
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            // httpWebRequest.ContentLength = Encoding.UTF8.GetByteCount(data);
            //strJson为json字符串 
            Stream stream = httpWebRequest.GetRequestStream(); 
            stream.Write(postBytes, 0, postBytes.Length); 
            stream.Close();
            //发送完毕，接受返回值 
            var response = httpWebRequest.GetResponse(); 
            Stream streamResponse = response.GetResponseStream(); 
            StreamReader streamRead = new StreamReader(streamResponse); 
            String responseString = streamRead.ReadToEnd();
            WriteFile(Server.MapPath("~/log.txt"), "restr:" + responseString);
            return Content(responseString); 
        } 

    }
}
