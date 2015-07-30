using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Reflection;

namespace torsion.BLL
{
    public static class  GlfGloFun
    {

        public static int get_DataRow(DataTable dt, int rn, object drai)
        {
            try
            {
                Type tdrai = drai.GetType();
                PropertyInfo[] ci = tdrai.GetProperties();


                foreach (PropertyInfo c in ci)
                {
                    if (dt.Columns.Contains(c.Name))
                    {

                        if (c.PropertyType == typeof(string))
                            c.SetValue(drai, dt.Rows[rn][c.Name].ToString(), null);
                        else
                            c.SetValue(drai, dt.Rows[rn][c.Name], null);
                    }

                }

            }
            catch (Exception e)
            {
                GlfGloFun.Write_Err(e.Message);
                return 0;
            }
            return 1;
        }

        public static string lsterr = "";
        static Object thisLock = new Object();

        public static void Write_Err(string err,int errcode = 0)
        {
            lock (thisLock)
            {
                if (err == lsterr) return;
                StackTrace st = new StackTrace(true);
                StreamWriter sw = null;
                try
                {
                    lsterr = errcode.ToString().PadRight(5);
                    lsterr += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string tstr = "";
                    for (int i = 1; i < st.FrameCount && i < 5; i++)
                    {
                        tstr += st.GetFrame(i).GetMethod().Name;

                    }
                    lsterr += tstr.PadRight(40);
                    lsterr += err;
                    sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Err.txt", true, System.Text.Encoding.UTF8);
                    sw.WriteLine(err);
                    lsterr = err;
                }
                finally
                {
                }
            }
            // System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //  System.IO.File.WriteAllText(@"Err.txt",str);
           

        }
    }
}
