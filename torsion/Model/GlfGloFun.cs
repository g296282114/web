using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Reflection;

namespace torsion.Model
{
    public static class GlfGloFun
    {
   
        public static string GenerateCheckCode()
        {
            
            string str = string.Empty;
           
            Random random = new Random();
            for (int i = 0; i < torsion.Model.GlfGloVar.ACCESS_TOKEN_LEN; i++)
            {
                int num = random.Next();
                str = str + ((char)('A' + ((ushort)(num % 26)))).ToString();
            }
            return str;
        }
        public static string DataSetToJson(DataSet ds)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder("[");
            for (int o = 0; o < ds.Tables.Count; o++)
            {
                str.Append("{");
                str.Append(string.Format("\"{0}\":[", ds.Tables[o].TableName));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    str.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        str.Append(string.Format("\"{0}\":\"{1}\",", ds.Tables[0].Columns[j].ColumnName, ds.Tables[0].Rows[i][j].ToString()));
                    }
                    str.Remove(str.Length - 1, 1);
                    str.Append("},");
                }
                str.Remove(str.Length - 1, 1);
                str.Append("]},");
            }
            str.Remove(str.Length - 1, 1);
            str.Append("]");
            return str.ToString();
        }


        public static int get_DataRow(DataTable dt, int rn, object drai)
        {
            try
            {
                Type tdrai = drai.GetType();
                PropertyInfo[] ci = tdrai.GetProperties();


                foreach (PropertyInfo c in ci)
                {
                    if (dt.Columns.Contains(c.Name) && c.CanWrite)
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
        public static int add_DataRow(DataSet ds, object drai)
        {
            try
            {
                Type tdrai = drai.GetType();
                DataRow dr = ds.Tables[tdrai.Name].NewRow();
                PropertyInfo[] ci = tdrai.GetProperties();
                foreach (PropertyInfo c in ci)
                {

                    dr[c.Name] = c.GetValue(drai, null);

                }
                ds.Tables[tdrai.Name].Rows.Add(dr);

            }
            catch (Exception e)
            {

                GlfGloFun.Write_Err(e.Message);
                return 0;
            }


            return 1;
        }
        static string lsterr = "";
        static Object errLock = new Object();

        public static void Write_Err(string err, int errcode = 0)
        {
          
            // System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //  System.IO.File.WriteAllText(@"Err.txt",str);
            lock (errLock)
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
                        tstr += st.GetFrame(i).GetMethod().Name+"->";

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

        }

        static Object logLock = new Object();
        public static void Write_Log(string err, int errcode = 0)
        {

            // System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            //  System.IO.File.WriteAllText(@"Err.txt",str);
            lock (logLock)
            {
                StreamWriter sw = null;
                try
                { 
                    sw = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true, System.Text.Encoding.UTF8);
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "      " + err);

                }
                finally
                {
                }

            }


        }
    }
}
