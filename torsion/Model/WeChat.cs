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
        public WeChat()
		{}
        public string token
        {
            set { _token = value; }
            get { return _token; }
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
       
    }
}
