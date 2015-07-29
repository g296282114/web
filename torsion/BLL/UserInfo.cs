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
        private readonly IUserInfo dal = DataAccess.CreateUserInfo();
        public DataSet get_UserInfo()
        {
            return dal.get_UserInfo();
        }

    }
}
