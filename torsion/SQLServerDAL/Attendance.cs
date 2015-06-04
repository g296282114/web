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

    }
}
