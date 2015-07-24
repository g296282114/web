using System;
using System.Collections.Generic;
using System.Text;
using torsion.IDAL;
using System.Data;
using System.Data.SqlClient;
using Maticsoft.DBUtility;

namespace torsion.SQLServerDAL
{
    public class Menus : IMenus
    {
        public int get_Menus(torsion.Model.Menus mm)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [id],[name],[parid],[ico],[url] from Menus order by id ");
                DataSet ds = DbHelperSQL.Query(strSql.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    mm.mt = new Model.Menus.MenusTitle[ds.Tables[0].Rows.Count];
                    Model.Menus.MenusTitle tmmt = null;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        mm.mt[i] = new Model.Menus.MenusTitle();
                        torsion.Model.GlfGloFun.get_DataRow(ds.Tables[0], i, mm.mt[i]);
                        if (mm.mt[i].parid <= 0)
                        {
                            tmmt = mm.mt[i];
                            tmmt.subnum = 0;
                        }  
                        else
                            tmmt.subnum += 1;

                    }
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception e)
            {
                Model.GlfGloFun.Write_Err(e.Message);
                return 0;
            }
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
    }
}
