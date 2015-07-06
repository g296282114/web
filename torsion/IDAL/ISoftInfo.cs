using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.IDAL
{
    public interface ISoftInfo
    {
        int Update_SoftInfo(torsion.Model.SoftInfo tsi);
        int con_SoftInfo(torsion.Model.SoftInfo tsi);
    }
}
