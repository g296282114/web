using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace torsion.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /StaffInfo/
        BLL.UserInfo webll = new BLL.UserInfo();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public int AddBaseInfo(torsion.Model.UserInfo.BaseInfo bi)
        {
            if (ModelState.IsValid)　　//此处仅作演示，不考虑安全性
            {
                //插入数据库省略
                torsion.Model.UserInfo ui = new Model.UserInfo();
                //ui.baseInfo = new Model.UserInfo.BaseInfo();
                ui.baseInfo = bi;

                return webll.add_UserInfo(ui);
            }
            return 0;
        }
        [HttpPost]
        public int ModifyBaseInfo(torsion.Model.UserInfo.BaseInfo bi)
        {
            if (ModelState.IsValid)　　//此处仅作演示，不考虑安全性
            {
                //插入数据库省略
                torsion.Model.UserInfo ui = new Model.UserInfo();
                ui.baseInfo = bi;

                return webll.update_UserInfo(ui);
            }
            return 0;
        }
        [HttpPost]
        public int DelBaseInfo(torsion.Model.UserInfo.BaseInfo bi)
        {
            if (ModelState.IsValid)　　//此处仅作演示，不考虑安全性
            {
                //插入数据库省略
                torsion.Model.UserInfo ui = new Model.UserInfo();
                ui.baseInfo = bi;

                return webll.del_UserInfo(ui);
            }
            return 0;
        }
        public string UserInfoJson(string search = "",int deptid=0)
        {
            
            DataSet ds = webll.get_UserInfo(search, deptid);
            torsion.Model.UserInfo.BaseInfo[] uibi = new Model.UserInfo.BaseInfo[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                uibi[i] = new Model.UserInfo.BaseInfo();
                torsion.Model.GlfGloFun.get_DataRow(ds.Tables[0], i, uibi[i]);
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(uibi);
        }


    }
}
