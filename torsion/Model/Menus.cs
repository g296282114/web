using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace torsion.Model
{
    public class Menus
    {
        public MenusTitle[] mt;
        public class MenusTitle
        {
            public int id { get; set; }
            public string name { get; set; }
            public int parid { get; set; }
            public string ico { get; set; }
            public string url { get; set; }
            public int subnum { get; set; }
        }
        

    }
}
