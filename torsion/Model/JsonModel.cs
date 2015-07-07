using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.Model
{
    public class JsonModel
    {
        public class RecData
        {
            public RecData()
            {
                cmd = 0;
                cdata = "";
            }
            public int errcode { get; set; }
            public int cmd { get; set; }
            public string cdata { get; set; }
        }
    }
}
