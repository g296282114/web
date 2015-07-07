using System;
using System.Collections.Generic;
using System.Text;
using torsion.IDAL;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

namespace torsion.SQLServerDAL
{
    public class SoftInfo : ISoftInfo
    {

        public int Update_SoftInfo(torsion.Model.SoftInfo tsi)
        {
            try
            {
                tsi.lastTime = DateTime.Now;
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update SoftInfo set softName = @softName,lastTime = @lastTime where id = @id");
                SqlParameter[] updSC = 
                {
                new SqlParameter("@softName", SqlDbType.VarChar,50),
                new SqlParameter("@lastTime", SqlDbType.DateTime),
                new SqlParameter("@id", SqlDbType.Int),
                    
                };
                updSC[0].Value = tsi.softName;
                updSC[1].Value = tsi.lastTime;
                updSC[2].Value = tsi.id;

                DbHelperSQL.ExecuteSql(strSql.ToString(), updSC);
            }
            catch (Exception e)
            {
                Model.GlfGloFun.Write_Err(e.Message);
                return 0;
            }
            
            return 1;
        }
        public int con_SoftInfo(torsion.Model.SoftInfo tsi)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [id],[softKey],[softName],[secretKey],[email],[createTime],[lastTime]  from SoftInfo where secretKey = @secretKey ");
                SqlParameter[] selCla = 
                {
                new SqlParameter("@secretKey", SqlDbType.VarChar,50)
                };
                selCla[0].Value = tsi.secretKey;
                DataSet ds = DbHelperSQL.Query(strSql.ToString(), selCla);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    torsion.Model.GlfGloFun.get_DataRow(ds.Tables[0], 0, tsi);
                    Update_SoftInfo(tsi);
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                Model.GlfGloFun.Write_Err(e.Message);
                return 0;
            }
            return 1;
        }
    }
}
