using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.Model
{
    public static class GlfGloVar
    {
        public  const int RE_SUCCESS = 1;
        public  const int RE_UNKNOWERROR = 0;

        public  const int CMD_HEARTBEAT = 0x7f;

        public  const int CMD_CONEQ = 0x10 + 0xff;
        public  const int CMD_NEEDCONNECT = 0x20 + 0xff;
        public const int CMD_DEVICELIST = 0x30 + 0xff;
        public const int CMD_NEWRECORD = 0x40 + 0xff;
        public const int CMD_OPENLOCK = 0x50 + 0xff;

        public  const int CMD_ERRCODE_OTHER = 0x01 + 0xffff;
        public  const int CMD_ERRCODE_JSONFORMAT = 0x10 + 0xffff;

        public  const int ACCESS_TOKEN_LEN = 30;


       


        public  const string ERRSTR_UNREGISTERED = "unregistered";


        public  const int SERVER_POST_TIMEOUT =  1000 * 30;
        public const int CLIENT_POST_TIMEOUT = SERVER_POST_TIMEOUT + 1000 * 60;

        public  const int SERVER_SLEEP_TIME = 500;
        public  const int CLIENT_SLEEP_TIME = 500;

        public const int SENDTIMEOUT = 20 * 1000;

        public static string TEST_STRING = "";
        

    }
}
