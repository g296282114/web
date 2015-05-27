using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using torsion.DALFactory;
using torsion.IDAL;
namespace torsion.BLL
{
    public class Attendance
    {
        private readonly IAttendance dal = DataAccess.CreateAttendance();
        public Attendance()
		{}
		#region  成员方法



        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

 


        #endregion
    }
}
