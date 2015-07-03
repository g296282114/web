using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Net;

namespace torsion.Controllers
{
    public class GlobalController 
    {
        //
        // GET: /Global/
        

        public static bool Write_Err(string str)
        {

            // System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        
          //  System.IO.File.WriteAllText(@"Err.txt",str);
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter( System.AppDomain.CurrentDomain.BaseDirectory+"Err.txt", true, System.Text.Encoding.UTF8);
                sw.Write(str + "\r\n");
                sw.Flush();
            }
            finally
            {
                sw.Close();
            }
            return true;

        }
        public static string Get_Post_String(HttpRequestBase request)
        {
            string postStr = "";
            if (request.HttpMethod.ToLower() == "post")
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                postStr = Encoding.UTF8.GetString(b);
                if (!string.IsNullOrEmpty(postStr))
                {
                    Write_Err("poststr:" + postStr);
                }                 //WriteLog("postStr:" + postStr);
            }
            return postStr;
        }
        public static string Send_Post_String(string url, string postString)
        {
            if (string.IsNullOrEmpty(postString) || string.IsNullOrEmpty(url))
            {
                return "";
            }
            byte[] postBytes = Encoding.UTF8.GetBytes(postString);
            System.Net.HttpWebRequest httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
            httpWebRequest.Method = "POST";
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
            return responseString; 
        }
        public static string Get_Get_String(string url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }

    }
}
