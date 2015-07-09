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

        public DataSet get_AttendanceInfo(int sid, DateTime sdt, DateTime edt)
        {
            return dal.get_AttendanceInfo(sid,sdt,edt);
        }
        public int Insert_AttendanceInfo(torsion.Model.Attendance.AttendanceInfo[] aai)
        {
            return dal.Insert_AttendanceInfo(aai);
        }

        #endregion
    }
}
