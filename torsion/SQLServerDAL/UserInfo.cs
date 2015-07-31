using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using torsion.IDAL;
using System.Data;
using Maticsoft.DBUtility;
using System.Data.SqlClient;

namespace torsion.SQLServerDAL
{
    public class UserInfo : IUserInfo
    {
        public int OneUserInfo(torsion.Model.UserInfo ui)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [id] ,[UserCode] ,[Name] ,[Pwd],[Deptid] ,[Groupid] ,[UserType] ,[CompareType]  from glf_UserInfo where 1=1 ");
                if (ui.baseInfo.id != 0)
                {

                    strSql.Append(" and  id = " + ui.baseInfo.id);

                }
                if (ui.baseInfo.UserCode != "")
                {
                    strSql.Append(" and UserCode = '" + ui.baseInfo.UserCode + "'");
                }
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables[0].Rows.Count <= 0) return 2;
                torsion.Model.GlfGloFun.get_DataRow(ds.Tables[0], 0, ui.baseInfo);
            }
            catch (Exception e)
            {
                torsion.Model.GlfGloFun.Write_Err(e.Message);
                return 0;
            }
           
            return torsion.Model.GlfGloVar.RE_SUCCESS;

        }
        public int add_UserInfo(torsion.Model.UserInfo ui)
        {
            StringBuilder strSql = new StringBuilder();


            try
            {

                strSql.Append("insert into glf_UserInfo([UserCode] ,[Name] ,[Pwd],[Deptid] ,[Groupid] ,[UserType] ,[CompareType]) values (@UserCode,@Name,@Pwd,@Deptid,@Groupid,@UserType,@CompareType) ");
                SqlParameter[] updSC = 
            {
                    new SqlParameter("@UserCode", SqlDbType.VarChar,20),
                    new SqlParameter("@Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Pwd", SqlDbType.VarChar,50),
                    new SqlParameter("@Deptid", SqlDbType.Int),
                    new SqlParameter("@Groupid", SqlDbType.Int),
                    new SqlParameter("@UserType", SqlDbType.Int),
                    new SqlParameter("@CompareType", SqlDbType.Int)
 
            };
                updSC[0].Value = ui.baseInfo.UserCode;
                updSC[1].Value = ui.baseInfo.Name;
                updSC[2].Value = ui.baseInfo.Pwd;
                updSC[3].Value = ui.baseInfo.Deptid;
                updSC[4].Value = ui.baseInfo.Groupid;
                updSC[5].Value = ui.baseInfo.UserType;
                updSC[6].Value = ui.baseInfo.CompareType;
                if (DbHelperSQL.ExecuteSql(strSql.ToString(), updSC) <= 0)
                    return 2;
            }
            catch
            {
                return 3;
            }
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
        public int update_UserInfo(torsion.Model.UserInfo ui)
        {
            StringBuilder strSql = new StringBuilder();


            try
            {
                strSql.Append("update glf_UserInfo set UserCode=@UserCode,Name=@Name,Pwd=@Pwd,Deptid=@Deptid,Groupid=@Groupid,UserType=@UserType,CompareType=@CompareType where id =@id ");
                SqlParameter[] updSC = 
            {
                 new SqlParameter("@UserCode", SqlDbType.VarChar,20),
                    new SqlParameter("@Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Pwd", SqlDbType.VarChar,50),
                    new SqlParameter("@Deptid", SqlDbType.Int),
                    new SqlParameter("@Groupid", SqlDbType.Int),
                    new SqlParameter("@UserType", SqlDbType.Int),
                    new SqlParameter("@CompareType", SqlDbType.Int),
                    new SqlParameter("@id", SqlDbType.Int)
 
            };
                updSC[0].Value = ui.baseInfo.UserCode;
                updSC[1].Value = ui.baseInfo.Name;
                updSC[2].Value = ui.baseInfo.Pwd;
                updSC[3].Value = ui.baseInfo.Deptid;
                updSC[4].Value = ui.baseInfo.Groupid;
                updSC[5].Value = ui.baseInfo.UserType;
                updSC[6].Value = ui.baseInfo.CompareType;
                updSC[7].Value = ui.baseInfo.id;

                if (DbHelperSQL.ExecuteSql(strSql.ToString(), updSC) <= 0)
                    return 2;
            }
            catch
            {
                return 3;
            }
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
        public int del_UserInfo(torsion.Model.UserInfo ui)
        {
            StringBuilder strSql = new StringBuilder();


            try
            {

                strSql.Append("delete from  glf_UserInfo where id =@id ");
                SqlParameter[] updSC = 
            {
                    new SqlParameter("@id", SqlDbType.Int),
 
            };
                updSC[0].Value = ui.baseInfo.id;
                if (DbHelperSQL.ExecuteSql(strSql.ToString(), updSC) <= 0)
                    return 2;
            }
            catch
            {
                return 3;
            }
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
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

        public DataSet get_UserExtend()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id] ,[FieldName] ,[FieldContent] ,[FieldType],[FieldValue] from glf_ExtendField ");
            return DbHelperSQL.Query(strSql.ToString());

        }

        public int add_UserExtend(torsion.Model.UserInfo.ExtendInfo ei)
        {
             StringBuilder strSql = new StringBuilder();
           
            
            try
            {
                strSql.Append("alter table glf_ExtendInfo add COLUMN  " + ei.FieldName + " varchar(255)");
                 DbHelperSQL.ExecuteSql(strSql.ToString());
                 strSql.Remove(0, strSql.Length);
                 strSql.Append("insert into glf_ExtendField([FieldName],[FieldContent],[FieldType],[FieldValue]) values (@FieldName,@FieldContent,@FieldType,@FieldValue)");
                 SqlParameter[] updSC = 
            {
                    new SqlParameter("@FieldName", SqlDbType.VarChar,50),
                    new SqlParameter("@FieldContent", SqlDbType.VarChar,1000),
                    new SqlParameter("@FieldType", SqlDbType.Int),
                    new SqlParameter("@FieldValue", SqlDbType.VarChar,255),
 
            };
                 updSC[0].Value = ei.FieldName;
                 updSC[1].Value = ei.FieldContent;
                 updSC[2].Value = ei.FieldType;
                 updSC[3].Value = ei.FieldValue;
               if(DbHelperSQL.ExecuteSql(strSql.ToString(),updSC) <=0)
                   return 2;
            }
            catch
            {
                return 3;
            }
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }

        public int del_UserExtend(torsion.Model.UserInfo.ExtendInfo ei)
        {
            StringBuilder strSql = new StringBuilder();


            try
            {
                strSql.Append("alter table glf_ExtendInfo drop column  " + ei.FieldName );
                DbHelperSQL.ExecuteSql(strSql.ToString());
                strSql.Remove(0, strSql.Length);
                strSql.Append("delete from  glf_ExtendField where id =@id ");
                SqlParameter[] updSC = 
            {
                    new SqlParameter("@id", SqlDbType.Int),
 
            };
                updSC[0].Value = ei.id;
                if (DbHelperSQL.ExecuteSql(strSql.ToString(), updSC) <= 0)
                    return 2;
            }
            catch
            {
                return 3;
            }
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }

        public int update_UserExtend(torsion.Model.UserInfo.ExtendInfo sei,torsion.Model.UserInfo.ExtendInfo dei)
        {
            StringBuilder strSql = new StringBuilder();


            try
            {
                strSql.Append("alter table glf_ExtendInfo rename column  " + sei.FieldName +" to "+dei.FieldName );
                DbHelperSQL.ExecuteSql(strSql.ToString());
                strSql.Remove(0, strSql.Length);
                strSql.Append("update glf_ExtendField set FieldName=@FieldName,FieldContent=@FieldContent,FieldType=@FieldType,FieldValue=@FieldValue where id =@id ");
                SqlParameter[] updSC = 
            {
                new SqlParameter("@FieldName", SqlDbType.VarChar,50),
                new SqlParameter("@FieldContent", SqlDbType.VarChar,1000),
                new SqlParameter("@FieldType", SqlDbType.Int),
                new SqlParameter("@FieldValue", SqlDbType.VarChar,255),
                new SqlParameter("@id", SqlDbType.Int)
 
            };
                updSC[0].Value = dei.FieldName;
                updSC[1].Value = dei.FieldContent;
                updSC[2].Value = dei.FieldType;
                updSC[3].Value = dei.FieldValue;
                updSC[4].Value = dei.id;
                if (DbHelperSQL.ExecuteSql(strSql.ToString(), updSC) <= 0)
                    return 2;
            }
            catch
            {
                return 3;
            }
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
    }
}
