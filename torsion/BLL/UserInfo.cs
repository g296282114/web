using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using torsion.IDAL;
using torsion.DALFactory;

namespace torsion.BLL
{
    public class UserInfo
    {
        public static List<torsion.Model.UserInfo.ExtendInfo> _gluiei = new List<Model.UserInfo.ExtendInfo>();

        private readonly IUserInfo dal = DataAccess.CreateUserInfo();

        public List<torsion.Model.UserInfo.ExtendInfo> ListExtend()
        {
            if (_gluiei.Count >= 0) return _gluiei;
            DataSet ds = get_UserExtend();
            _gluiei.Clear();
            torsion.Model.UserInfo.ExtendInfo tue;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tue = new Model.UserInfo.ExtendInfo();
                torsion.Model.GlfGloFun.get_DataRow(ds.Tables[0], i, tue);
                _gluiei.Add(tue);
            }
            return _gluiei;
        }

        public DataSet get_UserInfo(string search,int deptid)
        {
            return dal.get_UserInfo(search,deptid);
        }
        public DataSet get_UserExtend()
        {
            return dal.get_UserExtend();
        }

        public int add_UserExtend(torsion.Model.UserInfo.ExtendInfo ei)
        {
            return dal.add_UserExtend(ei);
        }
        public int del_UserExtend(torsion.Model.UserInfo.ExtendInfo ei)
        {
            return dal.del_UserExtend(ei);
        }
        public int update_UserExtend(torsion.Model.UserInfo.ExtendInfo sei, torsion.Model.UserInfo.ExtendInfo dei)
        {
            return dal.update_UserExtend(sei,dei);
        }

        public int add_UserInfo(torsion.Model.UserInfo ui)
        {
            return dal.add_UserInfo(ui);
        }
        public int update_UserInfo(torsion.Model.UserInfo ui)
        {
            return dal.update_UserInfo(ui);
        }
        public int del_UserInfo(torsion.Model.UserInfo ui)
        {
            return dal.del_UserInfo(ui);
        }
        public int OneUserInfo(torsion.Model.UserInfo ui)
        {
            return dal.OneUserInfo(ui);
        }

    }
}
