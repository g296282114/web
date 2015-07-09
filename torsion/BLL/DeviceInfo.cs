using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using torsion.DALFactory;
using torsion.IDAL;

namespace torsion.BLL
{
    public class DeviceInfo
    {
        private readonly IDeviceInfo dal = DataAccess.CreateDeviceInfo();

        public static List<Model.SoftInfo> gl_si = new List<Model.SoftInfo>();

        public DataSet DeviceInfoSoftId(int id)
        {
            return dal.DeviceInfoSoftId(id);
        }

        public int DeviceInfoId(torsion.Model.DeviceInfo di)
        {
            return dal.DeviceInfoId(di);
        }

    }
}
