using System;
using System.Data;
namespace torsion.IDAL
{
	/// <summary>
	/// �ӿڲ�IUser ��ժҪ˵����
	/// </summary>
	public interface IAdmin
	{
		#region  ��Ա����
		
		/// <summary>
		/// ��������б�
		/// </summary>
		DataSet GetList(string strWhere);

        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        torsion.Model.Admin GetModel(int UserID);

		#endregion  ��Ա����
	}
}
