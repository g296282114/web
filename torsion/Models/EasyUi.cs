using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace torsion.Models
{
    public class EasyUi
    {
        public class DataGridJson
        {
            public class AttendanceInfo
            {
                public int total = 0;
                public torsion.Model.AttendanceSet.DatasetStat.DRAttendanceInfo[] rows;
            }
            public class ClassesInfo
            {
                public int total = 0;
                public torsion.Model.AttendanceSet.DatasetStat.DRClassesInfo[] rows;
            }
            public class ResultInfo
            {
                public int total = 0;
                public torsion.Model.AttendanceSet.DatasetStat.DRResultInfo[] rows;
            }


        }
    
    }
}