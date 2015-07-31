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
        DataSet get_UserExtend();
        int add_UserExtend(torsion.Model.UserInfo.ExtendInfo ei);
        int del_UserExtend(torsion.Model.UserInfo.ExtendInfo ei);
        int update_UserExtend(torsion.Model.UserInfo.ExtendInfo sei, torsion.Model.UserInfo.ExtendInfo dei);
        int add_UserInfo(torsion.Model.UserInfo ui);
        int update_UserInfo(torsion.Model.UserInfo ui);
        int del_UserInfo(torsion.Model.UserInfo ui);
        int OneUserInfo(torsion.Model.UserInfo ui);
        
    }
}
