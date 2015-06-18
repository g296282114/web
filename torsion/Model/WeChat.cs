using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.Model
{
    public class WeChat
    {
        public class reJSON
        {
            public int errCode { get; set; }
            public string errMessage { get; set; }       
        }
        public class Access_Token
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }

        }
        public class JSEQdata
        {
            public JSEQdata() { }
            public int Userid { get; set; }
            // [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 20)]
            public string Checktime { get; set; }
            //  public string Checktime { get; set; }
            public int Checktype { get; set; }
            public int Sensorid { get; set; }
            public int WorkType { get; set; }
            public int AttFlag { get; set; }
            public int OpenDoorFlag { get; set; }
        }

        private string _token = "";
        private string _acToken = "";
        private string _WeChatID = "";
        private string _appID = "";
        private string _appsecret = "";
        private string _userID = "";
        private long _actoken_expired = 0;


        private DateTime actoken_time;

        public WeChat()
		{}
        public string token
        {
            set { _token = value; }
            get { return _token; }
        }
        public string acToken
        {
            set 
            {
                _acToken = value;
                actoken_time = DateTime.Now;
            }
            get 
            { 
                TimeSpan ts = DateTime.Now-actoken_time;
                if (ts.Seconds > _actoken_expired - 200)
                    return "";
                return _acToken; 
            }
           
        }
        public string WeChatID
        {
            set { _WeChatID = value; }
            get { return _WeChatID; }
        }
        public string appID
        {
            set { _appID = value; }
            get { return _appID; }
        }
        public string appsecret
        {
            set { _appsecret = value; }
            get { return _appsecret; }
        }
        public string userID
        {
            set { _userID = value; }
            get { return _userID; }
        }

        public long actoken_expired
        {
            set { _actoken_expired = value; }
            get { return _actoken_expired; }
        }
       
    }

}
