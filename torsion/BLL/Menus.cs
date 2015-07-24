using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using torsion.IDAL;
using System.Data;
using torsion.DALFactory;

namespace torsion.BLL
{
    public class Menus
    {
        private readonly IMenus dal = DataAccess.CreateMenus();
        public int get_Menus(torsion.Model.Menus mm)
        {
            return dal.get_Menus(mm);
        }
    }
}
