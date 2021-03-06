﻿using System;
using System.Data;

namespace torsion.IDAL
{
    /// <summary>
    /// 接口层IUser 的摘要说明。
    /// </summary>
    public interface IAttendance
    {
        #region  成员方法

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        DataSet get_AttendanceInfo(int sid, DateTime sdt, DateTime edt);
        int Insert_AttendanceInfo(torsion.Model.Attendance.AttendanceInfo[] aai);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        #endregion  成员方法
    }
}