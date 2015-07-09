using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using torsion.IDAL;
using Maticsoft.DBUtility;//请先添加引用
namespace torsion.SQLServerDAL
{
    public class Attendance:IAttendance
    {
        public Attendance()
		{}
        public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select usercode,name,checktime,sensorid ");
            strSql.Append(" FROM checkinout a,userinfo b where a.userid=b.userid ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" and "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
        public int Insert_AttendanceInfo(torsion.Model.Attendance.AttendanceInfo[] aai)
        {
            int innum = 0;
            string strSql = "insert into CheckInOut([Userid],[CheckTime],[CheckType],[Sensorid],[WorkType],[AttFlag],[Checked],[Exported],[OpenDoorFlag]) values (@Userid,@CheckTime,@CheckType,@Sensorid,@WorkType,@AttFlag,@Checked,@Exported,@OpenDoorFlag) ";
            SqlParameter[] insTim = 
            {
                    new SqlParameter("@Userid", SqlDbType.Int),
                    new SqlParameter("@CheckTime", SqlDbType.DateTime),
                    new SqlParameter("@CheckType", SqlDbType.Int),
                    new SqlParameter("@Sensorid", SqlDbType.Float),
                    new SqlParameter("@WorkType", SqlDbType.Int),
                    new SqlParameter("@AttFlag", SqlDbType.Int),
                    new SqlParameter("@Checked", SqlDbType.Int),
                    new SqlParameter("@Exported", SqlDbType.Int),
                    new SqlParameter("@OpenDoorFlag", SqlDbType.Int)
            };
            foreach (torsion.Model.Attendance.AttendanceInfo taai in aai)
            {
                try
                {
                    insTim[0].Value = taai.Userid;
                    insTim[1].Value = taai.CheckTime;
                    insTim[2].Value = taai.CheckType;
                    insTim[3].Value = taai.Sensorid;
                    insTim[4].Value = taai.WorkType;
                    insTim[5].Value = taai.AttFlag;
                    insTim[6].Value = taai.Checked;
                    insTim[7].Value = taai.Exported;
                    insTim[8].Value = taai.OpenDoorFlag;

                    DbHelperSQL.ExecuteSql(strSql.ToString(), insTim);
                    innum++;
                }
                catch
                {
                }
            }
            return innum;
        }
        public DataSet get_AttendanceInfo(int sid,DateTime sdt,DateTime edt)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [Logid],[Userid],[CheckTime],[CheckType],[Sensorid],[WorkType],[AttFlag],[Checked],[Exported],[OpenDoorFlag] from checkinout where Userid = @Userid and CheckTime >= @sdt and CheckTime < @edt order by CheckTime");
            SqlParameter[] selCIO = 
            {
                    new SqlParameter("@Userid", SqlDbType.Int),
                    new SqlParameter("@sdt", SqlDbType.DateTime),
                    new SqlParameter("@edt", SqlDbType.DateTime)
            };
            selCIO[0].Value = sid;
            selCIO[1].Value = sdt;
            selCIO[2].Value = edt;
            return DbHelperSQL.Query(strSql.ToString(),selCIO);
        }

    }
}
