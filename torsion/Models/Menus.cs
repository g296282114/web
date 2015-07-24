using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace torsion.Models
{
    public class Menus
    {
        //
        // GET: /Menus/
        public HomeMenu[] hm;
        public class HomeMenu
        {
            public int id { get; set; }
            public string name { get; set; }
            public string ico { get; set; }
            public string url { get; set; }
            public SubHomeMenu[] shm;
        }

        public class SubHomeMenu
        {
            public int id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
        }

    }
}
