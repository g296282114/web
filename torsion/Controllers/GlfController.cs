using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace torsion.Controllers
{
    public class GlfController : Controller
    {

        //model = bll.GetModel(id);
        //
        // GET: /Glf/

        public string Index()
        {
            torsion.BLL.Admin bll = new torsion.BLL.Admin();
            torsion.Model.Admin model = new torsion.Model.Admin();
            DataSet ds = bll.GetList("1=1");
            model = bll.GetModel(1);
            return model.UserName;
        }

    }
}
