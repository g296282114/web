using System;
using System.Collections.Generic;
using System.Text;
using torsion.IDAL;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

namespace torsion.SQLServerDAL
{
    public class DeviceInfo:IDeviceInfo
    {
        public DataSet DeviceInfoSoftId(int id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [DeviceId],[SoftId],[DeviceType],[DeviceName],[DeviceGroup],[AttandanceStat] ,[LinkMode] ,[IP],[ComPort]  from DeviceInfo where SoftId = @SoftId ");
                SqlParameter[] selCla = 
                {
                new SqlParameter("@SoftId",SqlDbType.Int)
                };
                selCla[0].Value = id;
                return DbHelperSQL.Query(strSql.ToString(), selCla);
               
            }
            catch (Exception e)
            {
                Model.GlfGloFun.Write_Err(e.Message);
                return null;
            }
        }
        public int DeviceInfoId(torsion.Model.DeviceInfo di)
        {
           
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
        public int update_DeviceInfo(torsion.Model.DeviceInfo di)
        {
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
        public int insert_DeviceInfo()
        {
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
        public int delete_DeviceInfo()
        {
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
    }
}
