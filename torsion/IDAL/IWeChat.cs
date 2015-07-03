using System;
using System.Collections.Generic;
using System.Text;

namespace torsion.IDAL
{
    public interface IWeChat
    {
        #region  成员方法



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
         torsion.Model.WeChat GetModel();
         int UpdateConf(string conName, string conValue);

        #endregion  成员方法
    }
}
