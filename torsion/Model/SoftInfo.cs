using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.Model
{
    public class SoftInfo
    {
        public int id { get; set; }
        public string softKey { get; set; }
        public string softName { get; set; }
        public string secretKey { get; set; }
        public string email { get; set; }
        public DateTime createTime { get; set; }
        public string assess_token { get; set; }
        public DateTime lastTime { get; set; }
        public int conStat { get; set; }
        public int cmd { get; set; }
        public string sendStr { get; set; }
        public string recStr { get; set; }
        public List<torsion.Model.DeviceInfo> di = new List<DeviceInfo>();
    }
}
