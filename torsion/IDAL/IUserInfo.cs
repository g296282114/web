using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace torsion.IDAL
{
    public interface IUserInfo
    {
        DataSet get_UserInfo(string search,int deptid);
    }
}
