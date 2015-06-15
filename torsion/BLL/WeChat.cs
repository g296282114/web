using System;
using System.Collections.Generic;
using System.Text;
using torsion.DALFactory;
using torsion.IDAL;
namespace torsion.BLL
{
    public class WeChat
    {
        private readonly IWeChat dal = DataAccess.CreateWeChat();
		public WeChat()
		{}
		#region  成员方法

        public torsion.Model.WeChat GetModel()
        {

            return dal.GetModel();
        }


		#endregion  成员方法
	}
}
