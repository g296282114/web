using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace torsion.Model
{
    public class Menus
    {
        public List<ParMenus> lpm = new List<ParMenus>();
        public class MenusTitle
        {
            public int id { get; set; }
            public string name { get; set; }
            public int parid { get; set; }
            public string ico { get; set; }
            public string url { get; set; }

        }
        public class ParMenus
        {
            public MenusTitle par;
            public List<MenusTitle> sub = new List<MenusTitle>();
        }

    }
}
