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
