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
 
        public class AttendanceInfo
        {
          int logid{get;set;}
          int Userid{get;set;}
          DateTime CheckTime{get;set;}
            int CheckType{get;set;}
            int Sensorid {get;set;}
            int WorkType {get;set;}
            int AttFlag {get;set;}
            int Checked {get;set;}
            int Exported {get;set;}
            int OpenDoorFlag {get;set;}

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
