using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;

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

    }
}
