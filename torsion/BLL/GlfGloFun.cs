using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace torsion.BLL
{
    public static class  GlfGloFun
    {
        public static void Write_Err(string err,int errcode = 0)
        {

            // System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //  System.IO.File.WriteAllText(@"Err.txt",str);
            StackTrace st = new StackTrace(true);
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Err.log", true, System.Text.Encoding.UTF8);
                sw.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "      " + st.GetFrame(1).GetMethod().Name.PadRight(30) + errcode.ToString().PadRight(10) + err + System.Environment.NewLine);
                sw.Flush();
            }
            finally
            {
                sw.Close();
            }

        }
    }
}
