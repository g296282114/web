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
        public DataSet get_UserInfo(string search,int deptid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id] ,[UserCode] ,[Name] ,[Pwd],[Deptid] ,[Groupid] ,[UserType] ,[CompareType]  from glf_UserInfo where 1=1 ");
            if (search.Trim() != "")
            {
                string idstr = " id=";
                try
                {
                    idstr += int.Parse(search).ToString()+" or ";
                }
                catch
                {
                    idstr = "";
                }
                strSql.Append(" and ( " + idstr + " Name like '%" + search + "%' or UserCode like '%" + search + "%')");

            }
            if (deptid != 0)
            {
                if(deptid < 0)
                    strSql.Append(" and Deptid < 0");
                else
                    strSql.Append(" and Deptid = "+deptid);
            }
                return DbHelperSQL.Query(strSql.ToString());  

        }
    }
}
