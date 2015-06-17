using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.Model
{
    public class WeChat
    {
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
            set 
            { 
                _token = value;
                actoken_time = DateTime.Now;
            }
            get 
            { 
                TimeSpan ts = DateTime.Now-actoken_time;
                if (ts.Seconds > _actoken_expired - 200)
                    return "";
                return _token; 
            }
        }
        public string acToken
        {
            set { _acToken = value; }
            get { return _acToken; }
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
