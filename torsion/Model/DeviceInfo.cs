using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.Model
{
    
    public class DeviceInfo
    {
        public DeviceInfo()
        {
            DeviceId = 0;
            BlEnable = 1;
        }
        public int DeviceId { get; set; }
        public int BlEnable { get; set; }
        public DeviceSet deviceSet { get; set; }
        public DeviceStat deviceStat { get; set; }
        public DeviceVersion deviceVersion { get; set; }
        public DeviceNet deviceNet { get; set; }
        public class DeviceSet
        {
            public int SoftId { get; set; }
            public int DeviceType { get; set; }
            public string DeviceName { get; set; }
            public int DeviceGroup { get; set; }
            public int AttandanceStat { get; set; }
            public int LinkMode { get; set; }//0 usb有驱、1 网络服务端、2 RS485、3 usb无驱、4 网络客户端
            public string IP { get; set; }
            public int ComPort { get; set; }
        }
        public class DeviceStat
        {
            public int BlEnable { get; set; }
            public int DeviceID { get; set; }
            public int DeviceCon { get; set; }
            public int StaffNum { get; set; }
            public int FigerPrintNum { get; set; }
            public int PasswordNum { get; set; }
            public int NewRecordNum { get; set; }
            public int AllRecordNum { get; set; }


        }
        public class DeviceVersion
        {
            public string DeviceType { get; set; }
            public string SysVersion { get; set; }
            public int DeviceSerial { get; set; }
        }
        public class DeviceNet
        {
            public string ClientIP { get; set; }
            public string ServerIP { get; set; }
            public int Port { get; set; }
            public string Geteway { get; set; }
            public string SubMask { get; set; }
            public string MAC { get; set; }
            public int WorkMode { get; set; }

        }
        
        
    }
}
