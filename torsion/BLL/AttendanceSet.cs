using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Data;
using torsion.DALFactory;
using torsion.IDAL;
namespace torsion.BLL
{
    public class AttendanceSet
    {
        const int ATDAY = 2;
        public static List<torsion.Model.AttendanceSet.ResultUnit> gl_ResultUnit = new List<Model.AttendanceSet.ResultUnit>();
        public static Double gl_DayHour = 0;
        private readonly IAttendanceSet dal = DataAccess.CreateAttendanceSet();

        public DataSet get_ResultUnit()
        {
            return dal.get_ResultUnit();
        }

        public int get_StaffClasses(torsion.Model.AttendanceSet.StaffClasses assc)
        {
            return dal.get_StaffClasses(assc);
        }
        public int update_StaffClasses(torsion.Model.AttendanceSet.StaffClasses assc)
        {
            return dal.update_StaffClasses(assc);
        }
        public int insert_StaffClasses(torsion.Model.AttendanceSet.StaffClasses assc)
        {
            return dal.insert_StaffClasses(assc);
        }
        public int delete_StaffClasses(int id)
        {
            return dal.delete_StaffClasses(id);
        }



        public int get_ClassesSetInfo(torsion.Model.AttendanceSet.ClassesInfo act)
        {
            return dal.get_ClassesSetInfo(act);
        }
        public int update_ClassesSetInfo(torsion.Model.AttendanceSet.ClassesInfo act)
        {
            return dal.update_ClassesSetInfo(act);
        }
        public int insert_ClassesSetInfo(torsion.Model.AttendanceSet.ClassesInfo act)
        {
            return dal.insert_ClassesSetInfo(act);
        }
        public int delete_ClassesSetInfo(int id)
        {
            return dal.delete_ClassesSetInfo(id);
        }

        public int get_StaffClassesInfo(List<torsion.Model.AttendanceSet.StaffClasses> asci, int sid, DateTime dt)
        {
            return dal.get_StaffClassesInfo(asci,sid,dt);
        }
         public Boolean bl_StaffClass(torsion.Model.AttendanceSet.StaffClasses assc, DateTime dt)
        {
            return dal.bl_StaffClass(assc, dt);
        }


        #region 自定义方法

         public void init_ResultUnit(Boolean bl = false)
         {
             if (bl)
             {
                 if (gl_ResultUnit != null)
                     gl_ResultUnit.Clear();
             }
             if (gl_ResultUnit.Count == 0)
             {
                 DataSet ds = get_ResultUnit();
                 for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                 {
                     torsion.Model.AttendanceSet.ResultUnit asru = new Model.AttendanceSet.ResultUnit();
                     get_DataRow(ds.Tables[0],i,asru);
                     gl_ResultUnit.Add(asru);

                 }
             }
             
         }
         public torsion.Model.AttendanceSet.ResultUnit ResultUnitById(int id)
         {
             init_ResultUnit();
             if (gl_ResultUnit.Count > id)
                 return gl_ResultUnit[id];
             else
                 return null;
         }
         public torsion.Model.AttendanceSet.ResultUnit ResultUnitByName(string t_name)
         {
             init_ResultUnit();
             switch (t_name.ToLower())
             {
                 case "work":
                 case "worktime":
                 case "realtime":
                 case "shouldwork":
                     t_name = "Normal";
                     break;
             }
             foreach (torsion.Model.AttendanceSet.ResultUnit asru in gl_ResultUnit)
             {
                 if (asru.ItemName.ToLower() == t_name.ToLower())
                     return asru;
             }
             return null;
         }

         public Double ConvertUnit(Double var,string Utype)
         {
             
             torsion.Model.AttendanceSet.ResultUnit asru =ResultUnitByName(Utype);
             Double rvar = var;
             if (asru != null)
             {
                 switch(asru.Units)
                 {
                         //分
                     case 0:
                         rvar = var*60;
                         break;
                         //时
                     case 1:
                         rvar = var;
                         break;
                         //天
                     case 2:
                         rvar = var / gl_DayHour;
                         break;
                         //次
                     case 3:
                         rvar = 1;
                         break;
                 }
                // rvar = (rvar / asru.MinUnit) * asru.MinUnit;
                 switch (asru.SRControl)
                 {
                         //向下舍弃
                     case 1:
                         rvar = Math.Floor(rvar / (Double)asru.MinUnit) * (Double)asru.MinUnit;
                         break;
                         //向上进位
                     case 2:
                         rvar = Math.Ceiling(rvar / (Double)asru.MinUnit) * (Double)asru.MinUnit;
                         break;
                         //四舍五入
                     default:
                         rvar = Math.Round(rvar / (Double)asru.MinUnit) * (Double)asru.MinUnit;
                         break;
                         
                 }
             }
             return rvar;
         }


         public int get_DataRow(DataTable dt,int rn, object drai)
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
                 GlfGloFun.Write_Err( e.Message);
                 return 0;
             }
             return 1;
         }
         public int add_DataRow(DataSet ds,object drai)
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
         public int get_StaffClassDay(torsion.Model.AttendanceSet.DatasetStat.DRResultInfo drri, DateTime day, DataSet ds, int dsi, torsion.Model.AttendanceSet.StaffClasses assc, torsion.Model.AttendanceSet.ClassesInfo asci, DataSet tjds)
         {
             int sid = drri.Userid;
             int rei = dsi;
             if (ds.Tables[0].Rows.Count == 0)
                 return rei;
             Boolean blw = true;

             TimeSpan ts;
             DateTime dt;
             DateTime? sdt = null;
             DateTime? edt = null;
             if (!bl_StaffClass(assc, day))
                 return 1;

             torsion.Model.AttendanceSet.DatasetStat.DRAttendanceInfo drai = new Model.AttendanceSet.DatasetStat.DRAttendanceInfo();

             while (blw)
             {
                
                 dt = Convert.ToDateTime(ds.Tables[0].Rows[dsi]["CheckTime"].ToString());
                 if (dt >= day.AddMinutes(asci.valid_s) && dt < day.AddMinutes(asci.valid_e))
                 {
                     //drai.Logid = Convert.ToInt32(ds.Tables[0].Rows[dsi]["logid"].ToString());
                     //drai.Userid = Convert.ToInt32(ds.Tables[0].Rows[dsi]["Userid"].ToString());
                     //drai.AttFlag = Convert.ToInt32(ds.Tables[0].Rows[dsi]["AttFlag"].ToString());
                     //drai.CheckType = Convert.ToInt32(ds.Tables[0].Rows[dsi]["CheckType"].ToString());
                     //drai.OpenDoor = Convert.ToInt32(ds.Tables[0].Rows[dsi]["OpenDoor"].ToString());
                     //drai.Sensorid = Convert.ToInt32(ds.Tables[0].Rows[dsi]["Sensorid"].ToString());
                     //drai.CheckTime = Convert.ToDateTime(ds.Tables[0].Rows[dsi]["CheckTime"].ToString());
                     get_DataRow(ds.Tables[0],dsi, drai);
 
                     if (!sdt.HasValue)
                     {                       
                         sdt = dt;
                         edt = dt;
                         drai.Suggest = 1;
                         add_DataRow(tjds, drai);
                     }

                     if (dt > edt)
                         edt = dt;

                 }
                 if (dt > day.AddMinutes(asci.valid_e))
                     blw = false;
                 if (dsi >= ds.Tables[0].Rows.Count - 1)
                     blw = false;

                 if (dt < day.AddDays(-ATDAY))
                     rei = dsi;
                 dsi++;
             }
             if (edt > sdt)
             {
                 drai.Suggest = 2;
                 add_DataRow(tjds,drai);
             }


             torsion.Model.AttendanceSet.DatasetStat.DRClassesInfo drci = new Model.AttendanceSet.DatasetStat.DRClassesInfo();
             drci.ClassesDate = day;
             drci.ClassesName = asci.t_name;
             drci.InsWork = sdt.ToString();
             drci.OutWork = edt > sdt ? edt.ToString() : "";
             drci.StartWork = day.AddMinutes(asci.work_s);
             drci.EndWork = day.AddMinutes(asci.work_e);
             Boolean blAbsent = true;
             if (sdt.HasValue)
             {
                 if (edt > sdt)
                 {
                     blAbsent = false;
                 }
             }
             
             if (!blAbsent)
             {
                 ts = (edt.Value > day.AddMinutes(asci.work_e) ? day.AddMinutes(asci.work_e) : edt.Value) - (sdt.Value < day.AddMinutes(asci.work_s) ? day.AddMinutes(asci.work_s) : sdt.Value);
                 drci.Work = ConvertUnit(ts.TotalHours, "Work");
                 if (day.AddMinutes(asci.work_s) < sdt.Value)
                 {
                     ts = sdt.Value - day.AddMinutes(asci.work_s);
                     drci.Late = ConvertUnit(ts.TotalHours, "Late");
                 }

                 if (day.AddMinutes(asci.work_e) > edt.Value)
                 {
                     ts = day.AddMinutes(asci.work_e) - edt.Value;
                     drci.Early = ConvertUnit(ts.TotalHours, "Early");
                 }


                 drci.WorkTime = drci.Work;
                 drci.RealTime = drci.Work;

                 if (asci.timeinfo != null)
                     foreach (torsion.Model.AttendanceSet.TimeInfo asti in asci.timeinfo)
                     {
                         if (!(day.AddMinutes(asti.work_s) > edt || day.AddMinutes(asti.work_e) < sdt))
                         {
                             ts = (edt.Value > day.AddMinutes(asti.work_e) ? day.AddMinutes(asti.work_e) : edt.Value) - (sdt.Value < day.AddMinutes(asti.work_s) ? day.AddMinutes(asti.work_s) : sdt.Value);
                             drci.OverTime +=  ConvertUnit(ts.TotalHours, "OverTime");
                             drci.WorkTime += ConvertUnit(ts.TotalHours * asti.rate, "WorkTime");
                             drci.RealTime += ConvertUnit(ts.TotalHours, "RealTime");
                         }


                     }
             }
             else
             {
                 ts = day.AddMinutes(asci.work_e) - day.AddMinutes(asci.work_s);
                 drci.Absent = ConvertUnit(ts.TotalHours, "Absent");         
             }

             add_DataRow(tjds, drci);
             drri.ShouldWork += ConvertUnit(gl_DayHour, "ShouldWork");

             drri.Work += drci.Work;
             drri.WorkTime += drci.WorkTime;
             drri.Late += drci.Late;
             drri.Early += drci.Early;
             drri.Absent += drci.Absent;
             drri.OverTime += drci.OverTime;
             drri.RealTime += drci.RealTime;

             return rei;
         }
         public int set_tjDataSet(DataSet tjds)
         {
             tjds.Tables.Clear();

             torsion.Model.AttendanceSet.DatasetStat asds = new torsion.Model.AttendanceSet.DatasetStat();
             Type tasds = asds.GetType();
             Assembly assem = Assembly.GetAssembly(tasds);
             foreach (Type st in assem.GetTypes())
             {
                 if (st.DeclaringType == null)
                     continue;
                 if (st.DeclaringType.FullName == tasds.FullName)
                 {
                     DataTable dt = new DataTable(st.Name);
                     PropertyInfo[] ci = st.GetProperties();
                     foreach (PropertyInfo c in ci)
                     {
                         dt.Columns.Add(new DataColumn(c.Name, c.PropertyType));
                     }
                     tjds.Tables.Add(dt);
                 }


             }

             return 1;
         }
         public int get_StatDataSet(DataSet tjds, int[] asid, DateTime tstart, DateTime tend)
         {
             if (asid.Length == 0)
                 return 1;
             

             torsion.BLL.Attendance adll = new torsion.BLL.Attendance();
             tstart = tstart.Date;
             tend = tend.Date;
             set_tjDataSet(tjds);

             List<torsion.Model.AttendanceSet.StaffClasses> assc = new List<torsion.Model.AttendanceSet.StaffClasses>();

             TimeSpan ts = tend - tstart;

             foreach (int sid in asid)
             {



                 torsion.Model.AttendanceSet.DatasetStat.DRResultInfo drri = new Model.AttendanceSet.DatasetStat.DRResultInfo();
                 drri.Userid = sid;

                 DataSet ds = adll.get_AttendanceInfo(sid, tstart.AddDays(-ATDAY), tend.AddDays(ATDAY));

                 int rds = 0;

                 for (int i = 0; i < ts.TotalDays; i++)
                 {
                     assc.Clear();
                     int scday = get_StaffClassesInfo(assc, sid, tstart.AddDays(i));
                     torsion.Model.AttendanceSet.ClassesInfo[] aact = new torsion.Model.AttendanceSet.ClassesInfo[assc.Count];
                     for (int j = 0; j < assc.Count; j++)
                     {
                         aact[j] = new torsion.Model.AttendanceSet.ClassesInfo();
                         aact[j].id = assc[j].cid;
                         get_ClassesSetInfo(aact[j]);
                         
                     }

                     if (i + scday > ts.TotalDays)
                         scday = Convert.ToInt32(ts.TotalDays) - i;
                     for (int j = 0; j < scday; j++)
                     {
                         gl_DayHour = 0;
                         for (int k = 0; k < assc.Count; k++)
                         {
                             if (bl_StaffClass(assc[k], tstart.AddDays(i)))
                             {
                                 gl_DayHour += (aact[k].work_e - aact[k].work_s)/60;
                             }
                         }
                         for (int k = 0; k < assc.Count; k++)
                         {
                             rds = get_StaffClassDay(drri, tstart.AddDays(i), ds, rds,assc[k], aact[k], tjds);
                         }

                         i++;
                     }
                 }
                 add_DataRow(tjds,drri);
             }

             return 1;
         }

        
        #endregion

    }
}
