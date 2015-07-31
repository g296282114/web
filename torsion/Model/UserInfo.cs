using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.Model
{
    public class UserInfo
    {
        public BaseInfo baseInfo;
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
        public class ExtendInfo
        {
            public int id { get; set; }
            public string FieldName { get; set; }
            public int FieldType { get; set; }
            public string FieldContent { get; set; }
            public string FieldValue { get; set; }
        }
    }
}
