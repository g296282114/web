using System;
using System.Data;
using System.Collections.Generic;
using LTP.Common;
using torsion.DALFactory;
using torsion.IDAL;
namespace torsion.BLL
{
	/// <summary>
	/// ҵ���߼���Admin ��ժҪ˵����
	/// </summary>
	public class Admin
	{
		private readonly IAdmin dal=DataAccess.CreateUser();
		public Admin()
		{}
		#region  ��Ա����



        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public torsion.Model.Admin GetModel(int UserID)
        {

            return dal.GetModel(UserID);
        }


		#endregion  ��Ա����
	}
}

