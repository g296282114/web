using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace torsion.Model
{
    public class UserInfo
    {
        public class BaseInfo
        {
            public int id { get; set; }
            public string UserCode { get; set; }
            public string Name { get; set; }
            public string Pwd { get; set; }
            public int Deptid { get; set; }
            public int Groupid { get; set; }
            public int UserType { get; set; }
            public int CompareType { get; set; }
        }
    }
}
