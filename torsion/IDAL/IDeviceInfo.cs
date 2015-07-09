using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace torsion.IDAL
{
    public interface IDeviceInfo
    {
        DataSet DeviceInfoSoftId(int id);
        int DeviceInfoId(torsion.Model.DeviceInfo di);

    }
}
