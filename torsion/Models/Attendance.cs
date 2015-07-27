using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace torsion.Models
{
    public class Attendance
    {

    }
    public class AttendanceDataSet
    {
        torsion.Model.AttendanceSet.DatasetStat.DRAttendanceInfo[] drai;
        torsion.Model.AttendanceSet.DatasetStat.DRClassesInfo[] drci;
        torsion.Model.AttendanceSet.DatasetStat.DRResultInfo[] drri;
    }
    public class AttdanceJson
    {
        public int usercode { get; set; }
        public string name { get; set; }
        public string checktime { get; set; }
        public int sensorid { get; set; }
    }
    public class AttdanceFooter
    {
        public string usercode { get; set; }
        public int name { get; set; }
        public int sensorid { get; set; }
    }
    public class AttdanceDataGrid
    {
        public int total = 0;
        public AttdanceJson[] rows;
        public AttdanceFooter[] footer;
    }
    
}