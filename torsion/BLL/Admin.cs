using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using torsion.DALFactory;
using torsion.IDAL;
namespace torsion.BLL
{
	/// <summary>
	/// 业务逻辑类Admin 的摘要说明。
	/// </summary>
	public class Admin
	{
		private readonly IAdmin dal=DataAccess.CreateUser();
		public Admin()
		{}
		#region  成员方法



        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public torsion.Model.Admin GetModel(int UserID)
        {

            return dal.GetModel(UserID);
        }


		#endregion  成员方法
	}
}

