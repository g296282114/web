using System;
using System.Collections.Generic;
using System.Text;


namespace torsion.Model
{
    public class AttendanceSet
    {
        
        public AttendanceSet()
        {
        }

        public class ResultUnit
        {
            public int Itemid { get; set; }
            public string ItemName { get; set; }
            public int Units { get; set; }
            public Decimal MinUnit { get; set; }
            public int SRControl { get; set; }
            public Boolean IsAddup { get; set; }
            public Boolean IsTimes { get; set; }
            public string Showas { get; set; }
            public Double OneDay { get; set; }
            
        }

        #region 统计数据集
        public class DatasetStat
        {
            public class DRAttendanceInfo
            {
                public DRAttendanceInfo()
                {
                }
                public int Logid { get; set; }
                public int Userid { get; set; }
                public DateTime CheckTime { get; set; }
                public int CheckType { get; set; }
                public int Sensorid { get; set; }
                public int AttFlag { get; set; }
                public int OpenDoorFlag { get; set; }
                public int Suggest { get; set; }

            }

            public class DRClassesInfo
            {
                public DRClassesInfo()
                {
                }
                public int Userid { get; set; }
                public DateTime ClassesDate { get; set; }
                public string ClassesName { get; set; }
                public DateTime StartWork { get; set; }
                public DateTime EndWork { get; set; }
                public string InsWork { get; set; }
                public string OutWork { get; set; }
                public Double Work { get; set; }
                public Double Late { get; set; }
                public Double Early { get; set; }
                public Double Absent { get; set; }
                public Double OverTime { get; set; }
                public Double WorkTime { get; set; }
                public Double RealTime { get; set; }

            }

            public class DRResultInfo
            {
                public DRResultInfo()
                {
                }
                public int Userid { get; set; }  
                public Double Work { get; set; }
                public Double Late { get; set; }
                public Double Early { get; set; }
                public Double Absent { get; set; }
                public Double OverTime { get; set; }
                public Double ShouldWork { get; set; }
                public Double RealTime { get; set; }
                public Double WorkTime { get; set; }
                

            }
        }
        
        #endregion

        public class TimeInfo
        {
            public int id { get; set; }
            public int cid { get; set; }
            //上下班时间
            public int work_s { get; set; }
            public int work_e { get; set; }
            //倍率
            public float rate { get; set; }
            //名称
            public string t_name { get; set; }
            public int t_type { get; set; }
            //


        }

        public class ClassesInfo
        {
            public int id { get; set; }
            public string t_name { get; set; }
            //有效范围
            public int valid_s { get; set; }
            public int valid_e { get; set; }
            //上下班时间
            public int work_s { get; set; }
            public int work_e { get; set; }
            //时间段范围
            public int time_s { get; set; }
            public int time_e { get; set; }
            public TimeInfo[] timeinfo;
        }

        public class StaffClasses
        {
            public int id { get; set; }
            public int sid { get; set; }
            public int cid { get; set; }
            public DateTime c_start { get; set; }
            public DateTime c_end { get; set; }
            public int type { get; set; }
            public int num { get; set; }
            public string days { get; set; }
        }
    }
}
