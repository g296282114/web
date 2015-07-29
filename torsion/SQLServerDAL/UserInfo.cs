using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using torsion.IDAL;
using System.Data;
using Maticsoft.DBUtility;

namespace torsion.SQLServerDAL
{
    public class UserInfo : IUserInfo
    {
        public DataSet get_UserInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id] ,[UserCode] ,[Name] ,[Pwd],[Deptid] ,[Groupid] ,[UserType] ,[CompareType] from glf_UserInfo ");
            return DbHelperSQL.Query(strSql.ToString());

        }
    }
}
