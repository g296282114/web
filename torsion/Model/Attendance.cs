using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.Model
{
    [Serializable]
    public class Attendance
    {
        
        public Attendance()
		{}
        public class JsonAttendance
        {
            public JsonAttendance()
            {
                errnum = 0;
            }
            public int allnum { get; set; }
            public int sendnum { get; set; }
            public int errnum { get; set; }
            public AttendanceInfo[] aai;
        }
 
        public class AttendanceInfo
        {
          public int logid{get;set;}
          public int Userid { get; set; }
          public DateTime CheckTime { get; set; }
          public int CheckType { get; set; }
          public int Sensorid { get; set; }
          public int WorkType { get; set; }
          public int AttFlag { get; set; }
          public int Checked { get; set; }
          public int Exported { get; set; }
          public int OpenDoorFlag { get; set; }

        }
        
        private int _userCode;
        private string _userName;
        private DateTime? _attendanceTime;
        private int _sensorId;

        public int userCode
        {
            set { _userCode = value; }
            get { return _userCode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string userName
        {
            set { _userName = value; }
            get { return _userName; }
        }

        public DateTime? attendanceTime
        {
            set { _attendanceTime = value; }
            get { return _attendanceTime; }
        }

        public int sensorId
        {
            set { _sensorId = value; }
            get { return _sensorId; }
        }
    }
}
