using System;
using System.Collections.Generic;
using System.Text;
using torsion.Model;
using System.Data;

namespace torsion.IDAL
{
    public interface IAttendanceSet
    {
        #region  成员方法

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// 
        DataSet get_ResultUnit();

        int insert_StaffClasses(AttendanceSet.StaffClasses assc);
        int update_StaffClasses(AttendanceSet.StaffClasses assc);
        int get_StaffClasses(AttendanceSet.StaffClasses assc);
        int delete_StaffClasses(int id);

        int get_ClassesSetInfo(AttendanceSet.ClassesInfo act);
        int update_ClassesSetInfo(AttendanceSet.ClassesInfo act);
        int insert_ClassesSetInfo(AttendanceSet.ClassesInfo act);
        int delete_ClassesSetInfo(int id);

        int get_StaffClassesInfo(List<torsion.Model.AttendanceSet.StaffClasses> asci, int sid, DateTime dt);
        Boolean bl_StaffClass(torsion.Model.AttendanceSet.StaffClasses assc, DateTime dt);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        #endregion  成员方法
    }
}
