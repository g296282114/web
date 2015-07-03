using System;
using System.Collections.Generic;
using System.Text;
using torsion.IDAL;
using Maticsoft.DBUtility;//请先添加引用
using System.Data.SqlClient;
using System.Data;

namespace torsion.SQLServerDAL
{
    public class AttendanceSet : IAttendanceSet
    {
        public AttendanceSet()
        { }

        #region  统计单位
        public DataSet get_ResultUnit()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from StatItems ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region  StaffClasses
        public int get_StaffClasses(torsion.Model.AttendanceSet.StaffClasses assc)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append("select [id],[sid],[cid],[c_start],[c_end],[type],[num],[days]  from StaffClasses where id = @id ");
            SqlParameter[] selCla = 
            {
                new SqlParameter("@id", SqlDbType.Int)
            };
            selCla[0].Value = assc.id;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), selCla);
            if (ds.Tables[0].Rows.Count > 0)
            {
                assc.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                assc.sid = int.Parse(ds.Tables[0].Rows[0]["sid"].ToString());
                assc.cid = int.Parse(ds.Tables[0].Rows[0]["cid"].ToString());
                assc.c_start = Convert.ToDateTime(ds.Tables[0].Rows[0]["c_start"].ToString());
                assc.c_end = Convert.ToDateTime(ds.Tables[0].Rows[0]["c_end"].ToString());
                assc.type = int.Parse(ds.Tables[0].Rows[0]["type"].ToString());
                assc.num = int.Parse(ds.Tables[0].Rows[0]["num"].ToString());
                assc.days = ds.Tables[0].Rows[0]["days"].ToString();
            }
            return 1;
        }
        public int delete_StaffClasses(int id)
        {

            StringBuilder strSql = new StringBuilder();

            strSql.Append("delete form StaffClasses where id = @id ");
            SqlParameter[] delSC = 
            {
                new SqlParameter("@id", SqlDbType.Int)
            };
            delSC[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), delSC);
           
            return 1;
        }
        public int update_StaffClasses(torsion.Model.AttendanceSet.StaffClasses assc)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update StaffClasses set sid = @sid,cid = @cid,c_start = @c_start,c_end = @c_end,type = @type,num = @num,days = @days where id = @id");
            SqlParameter[] updSC = 
            {
                    new SqlParameter("@sid", SqlDbType.Int),
                    new SqlParameter("@cid", SqlDbType.Int),
                    new SqlParameter("@c_start", SqlDbType.DateTime),
                    new SqlParameter("@c_end", SqlDbType.DateTime),
                    new SqlParameter("@type", SqlDbType.Int),
                    new SqlParameter("@num", SqlDbType.Int),
                    new SqlParameter("@days", SqlDbType.VarChar,255),
                    new SqlParameter("@id", SqlDbType.Int)
            };
            updSC[0].Value = assc.sid;
            updSC[1].Value = assc.cid;
            updSC[2].Value = assc.c_start;
            updSC[3].Value = assc.c_end;
            updSC[4].Value = assc.type;
            updSC[5].Value = assc.num;
            updSC[6].Value = assc.days;
            updSC[7].Value = assc.id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), updSC);
          
            return 1;
        }

        public int insert_StaffClasses(torsion.Model.AttendanceSet.StaffClasses assc)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into StaffClasses ([sid],[cid],[c_start],[c_end],[type],[num],[days]) values (@sid,@cid,@c_start,@c_end,@type,@num,@days)");
            SqlParameter[] updSC = 
            {
                    new SqlParameter("@sid", SqlDbType.Int),
                    new SqlParameter("@cid", SqlDbType.Int),
                    new SqlParameter("@c_start", SqlDbType.DateTime),
                    new SqlParameter("@c_end", SqlDbType.DateTime),
                    new SqlParameter("@type", SqlDbType.Int),
                    new SqlParameter("@num", SqlDbType.Int),
                    new SqlParameter("@days", SqlDbType.VarChar,255)
 
            };
            updSC[0].Value = assc.sid;
            updSC[1].Value = assc.cid;
            updSC[2].Value = assc.c_start;
            updSC[3].Value = assc.c_end;
            updSC[4].Value = assc.type;
            updSC[5].Value = assc.num;
            updSC[6].Value = assc.days;


            DbHelperSQL.ExecuteSql(strSql.ToString(), updSC);

            return 1;
        }
        #endregion

        #region  ClassesSetInfo
        public int get_ClassesSetInfo(torsion.Model.AttendanceSet.ClassesInfo act)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [t_name],[valid_s],[valid_e],[work_s],[work_e],[time_s],[time_e]  from ClassesSetInfo where id = @id ");
            SqlParameter[] selCla = 
            {
                new SqlParameter("@id", SqlDbType.Int)
            };
            selCla[0].Value = act.id;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), selCla);

            if (ds.Tables[0].Rows.Count > 0)
            {

                act.t_name = ds.Tables[0].Rows[0]["t_name"].ToString();
                act.valid_s = int.Parse(ds.Tables[0].Rows[0]["valid_s"].ToString());
                act.valid_e = int.Parse(ds.Tables[0].Rows[0]["valid_e"].ToString());
                act.work_s = int.Parse(ds.Tables[0].Rows[0]["work_s"].ToString());
                act.work_e = int.Parse(ds.Tables[0].Rows[0]["work_e"].ToString());
                act.time_s = int.Parse(ds.Tables[0].Rows[0]["time_s"].ToString());
                act.time_e = int.Parse(ds.Tables[0].Rows[0]["time_e"].ToString());
            }
            else
            {
                return 0;
            }
            if (act.timeinfo != null)
                act.timeinfo = null;

            strSql.Remove(0, strSql.Length);
            strSql.Append("select [id],[cid],[work_s],[work_e],[rate],[t_name],[t_type] from TimeSetInfo where cid = @id ");
            SqlParameter[] selTim = 
            {
                new SqlParameter("@id", SqlDbType.Int)
            };
            selTim[0].Value = act.id;     
            using (DataSet dst = DbHelperSQL.Query(strSql.ToString(), selTim))
            {
                if (dst.Tables[0].Rows.Count > 0)
                {
                    act.timeinfo = new torsion.Model.AttendanceSet.TimeInfo[dst.Tables[0].Rows.Count];
                    

                    for (int i = 0; i < dst.Tables[0].Rows.Count ; i++)
                    {
                        act.timeinfo[i] = new Model.AttendanceSet.TimeInfo();
                        act.timeinfo[i].id = int.Parse(dst.Tables[0].Rows[i]["id"].ToString());
                        act.timeinfo[i].cid = int.Parse(dst.Tables[0].Rows[i]["cid"].ToString());
                        act.timeinfo[i].work_s = int.Parse(dst.Tables[0].Rows[i]["work_s"].ToString());
                        act.timeinfo[i].work_e = int.Parse(dst.Tables[0].Rows[i]["work_e"].ToString());
                        act.timeinfo[i].rate = float.Parse(dst.Tables[0].Rows[i]["rate"].ToString());
                        act.timeinfo[i].t_name = dst.Tables[0].Rows[i]["t_name"].ToString();
                        act.timeinfo[i].t_type = int.Parse(dst.Tables[0].Rows[i]["t_type"].ToString());
                    }

                }

            }

            return 1;

        }
        public int delete_ClassesSetInfo(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TimeSetInfo where cid = @id ");

            SqlParameter[] deltim = 
            {
                new SqlParameter("@id", SqlDbType.Int)
            };
            deltim[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), deltim);

            strSql.Remove(0, strSql.Length);
            strSql.Append("delete from ClassesSetInfo where id = @id ");

            SqlParameter[] delCla = 
            {
                new SqlParameter("@id", SqlDbType.Int)
            };
            delCla[0].Value = id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), delCla);
            return 1;

        }
        public int update_ClassesSetInfo(torsion.Model.AttendanceSet.ClassesInfo act)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from TimeSetInfo where cid = @id ");

            SqlParameter[] deltim = 
            {
                new SqlParameter("@id", SqlDbType.Int)
            };
            deltim[0].Value = act.id;
            DbHelperSQL.ExecuteSql(strSql.ToString(), deltim);
            int t_time_s = act.work_s;
            int t_time_e = act.work_e;
            foreach (torsion.Model.AttendanceSet.TimeInfo asti in act.timeinfo)
            {
                if (asti.work_s < t_time_s)
                    t_time_s = asti.work_s;
                if (asti.work_e > t_time_e)
                    t_time_e = asti.work_e;
            }
            strSql.Remove(0, strSql.Length);
            strSql.Append("update ClassesSetInfo set t_name = @t_name,valid_s = @valid_s,valid_e = @valid_e,work_s = @work_s,work_e = @work_e,time_s = @time_s,time_e = @time_e where id = @id");
            SqlParameter[] updCla = 
            {
                    new SqlParameter("@t_name", SqlDbType.VarChar,50),
                    new SqlParameter("@valid_s", SqlDbType.Int),
                    new SqlParameter("@valid_e", SqlDbType.Int),
                    new SqlParameter("@work_s", SqlDbType.Int),
                    new SqlParameter("@work_e", SqlDbType.Int),
                    new SqlParameter("@time_s", SqlDbType.Int),
                    new SqlParameter("@time_e", SqlDbType.Int),
                    new SqlParameter("@id", SqlDbType.Int)
            };
            updCla[0].Value = act.t_name;
            updCla[1].Value = act.valid_s;
            updCla[2].Value = act.valid_e;
            updCla[3].Value = act.work_s;
            updCla[4].Value = act.work_e;
            updCla[5].Value = t_time_s;
            updCla[6].Value = t_time_e;
            updCla[7].Value = act.id;

            DbHelperSQL.ExecuteSql(strSql.ToString(), updCla);

            strSql.Remove(0, strSql.Length);
            strSql.Append("insert into TimeSetInfo([cid],[work_s],[work_e],[rate],[t_name],[t_type]) values (@cid,@work_s,@work_e,@rate,@t_name,@t_type) ");
            SqlParameter[] insTim = 
            {
                    new SqlParameter("@cid", SqlDbType.Int),
                    new SqlParameter("@work_s", SqlDbType.Int),
                    new SqlParameter("@work_e", SqlDbType.Int),
                    new SqlParameter("@rate", SqlDbType.Float),
                    new SqlParameter("@t_name", SqlDbType.VarChar,50),
                    new SqlParameter("@t_type", SqlDbType.Int)
            };
            foreach (torsion.Model.AttendanceSet.TimeInfo att in act.timeinfo)
            {
                insTim[0].Value = act.id;
                insTim[1].Value = att.work_s;
                insTim[2].Value = att.work_e;
                insTim[3].Value = att.rate;
                insTim[4].Value = att.t_name;
                insTim[5].Value = att.t_type;

                DbHelperSQL.ExecuteSql(strSql.ToString(), insTim);

            }
            return 1;
        }

        public int insert_ClassesSetInfo(torsion.Model.AttendanceSet.ClassesInfo act)
        {
            StringBuilder strSql = new StringBuilder();
            int t_time_s = act.work_s;
            int t_time_e = act.work_e;
            foreach (torsion.Model.AttendanceSet.TimeInfo asti in act.timeinfo)
            {
                if (asti.work_s < t_time_s)
                    t_time_s = asti.work_s;
                if (asti.work_e > t_time_e)
                    t_time_e = asti.work_e;
            }
            strSql.Append("insert into ClassesSetInfo([t_name],[valid_s],[valid_e],[work_s],[work_e],[time_s],[time_e]) values (@t_name,@valid_s,@valid_e,@work_s,@work_e,@time_s,@time_e)");
            SqlParameter[] insCla = 
            {
                    new SqlParameter("@t_name", SqlDbType.VarChar,50),
                    new SqlParameter("@valid_s", SqlDbType.Int),
                    new SqlParameter("@valid_e", SqlDbType.Int),
                    new SqlParameter("@work_s", SqlDbType.Int),
                    new SqlParameter("@work_e", SqlDbType.Int),
                    new SqlParameter("@time_s", SqlDbType.Int),
                    new SqlParameter("@time_e", SqlDbType.Int)
            };
            insCla[0].Value = act.t_name;
            insCla[1].Value = act.valid_s;
            insCla[2].Value = act.valid_e;
            insCla[3].Value = act.work_s;
            insCla[4].Value = act.work_e;
            insCla[5].Value = t_time_s;
            insCla[6].Value = t_time_e;

            DbHelperSQL.ExecuteSql(strSql.ToString(), insCla);

            act.id = DbHelperSQL.GetMaxID("id", "ClassesSetInfo") - 1;

            strSql.Remove(0, strSql.Length);
            strSql.Append("insert into TimeSetInfo([cid],[work_s],[work_e],[rate],[t_name],[t_type]) values (@cid,@work_s,@work_e,@rate,@t_name,@t_type) ");
            SqlParameter[] insTim = 
            {
                    new SqlParameter("@cid", SqlDbType.Int),
                    new SqlParameter("@work_s", SqlDbType.Int),
                    new SqlParameter("@work_e", SqlDbType.Int),
                    new SqlParameter("@rate", SqlDbType.Float),
                    new SqlParameter("@t_name", SqlDbType.VarChar,50),
                    new SqlParameter("@t_type", SqlDbType.Int)
            };
            foreach (torsion.Model.AttendanceSet.TimeInfo att in act.timeinfo)
            {
                insTim[0].Value = act.id;
                insTim[1].Value = att.work_s;
                insTim[2].Value = att.work_e;
                insTim[3].Value = att.rate;
                insTim[4].Value = att.t_name;
                insTim[5].Value = att.t_type;

                DbHelperSQL.ExecuteSql(strSql.ToString(), insTim);

            }
            return 1;

        }
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ClassesSetInfo a,TimeSetInfo b where a.id = b.cid ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion
        
        #region get staff classesinfo

        public Boolean bl_StaffClass(torsion.Model.AttendanceSet.StaffClasses assc, DateTime dt)
        {
            Boolean ret = false;
            switch (assc.type)
            {
                case 1:

                    break;
                case 2:
                    int spmonth = ((dt.Year - assc.c_start.Year) * 12 + dt.Month - assc.c_start.Month) % assc.num;
                    if (assc.days[spmonth * 31 + dt.Day] == '1')
                    {
                        ret = true;
                    }
                    break;
            }
            return ret;
        }

        public int get_StaffClassesInfo(List<torsion.Model.AttendanceSet.StaffClasses> assc, int sid, DateTime dt)
        {
            int ret = 100;
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select [id],[sid],[cid],[c_start],[c_end],[type],[num],[days]  from StaffClasses where sid = @sid and  c_end >@c_end  ");
            SqlParameter[] selSC = 
            {
                new SqlParameter("@sid", SqlDbType.Int),
                new SqlParameter("@c_end", SqlDbType.DateTime)
            };
            selSC[0].Value = sid;
            selSC[1].Value = dt;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), selSC);

            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                DateTime tc_s = Convert.ToDateTime(ds.Tables[0].Rows[i]["c_start"].ToString());
                DateTime tc_e = Convert.ToDateTime(ds.Tables[0].Rows[i]["c_end"].ToString());
                TimeSpan ts;
                if (tc_s <= dt)
                {
                    torsion.Model.AttendanceSet.StaffClasses tasci = new Model.AttendanceSet.StaffClasses();
                    tasci.id =  int.Parse(ds.Tables[0].Rows[i]["id"].ToString());
                    tasci.sid = int.Parse(ds.Tables[0].Rows[i]["sid"].ToString());
                    tasci.cid = int.Parse(ds.Tables[0].Rows[i]["cid"].ToString());
                    tasci.c_start = Convert.ToDateTime(ds.Tables[0].Rows[i]["c_start"].ToString());
                    tasci.c_end = Convert.ToDateTime(ds.Tables[0].Rows[i]["c_end"].ToString());
                    tasci.type = int.Parse(ds.Tables[0].Rows[i]["type"].ToString());
                    tasci.num = int.Parse(ds.Tables[0].Rows[i]["num"].ToString());
                    tasci.days = ds.Tables[0].Rows[i]["days"].ToString();
                    assc.Add(tasci);
                    ts = tc_e - dt;
                    if(ret > ts.Days)
                        ret = ts.Days;
                }
                else
                {
                    ts = tc_s - dt;
                    if (ret > ts.Days)
                        ret = ts.Days;
                }
                

            }
          
            return ret;
        }

        #endregion
    }
}
