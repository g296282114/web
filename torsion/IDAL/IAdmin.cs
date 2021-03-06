using System;
using System.Data;
namespace torsion.IDAL
{
	/// <summary>
	/// 接口层IUser 的摘要说明。
	/// </summary>
	public interface IAdmin
	{
		#region  成员方法
		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        torsion.Model.Admin GetModel(int UserID);

		#endregion  成员方法
	}
}
