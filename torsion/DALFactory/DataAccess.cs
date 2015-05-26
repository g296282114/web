using System;
using System.Reflection;
using System.Configuration;
namespace torsion.DALFactory
{
	/// <summary>
    /// Abstract Factory pattern to create the DAL��
    /// ��������ﴴ�����󱨴�����web.config���Ƿ��޸���<add key="DAL" value="Maticsoft.SQLServerDAL" />��
	/// </summary>
	public sealed class DataAccess
	{
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];        
		public DataAccess()
		{ }

        #region CreateObject 

		//��ʹ�û���
        private static object CreateObjectNoCache(string AssemblyPath,string classNamespace)
		{		
			try
			{
				object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);	
				return objType;
			}
			catch//(System.Exception ex)
			{
				//string str=ex.Message;// ��¼������־
				return null;
			}			
			
        }
		//ʹ�û���
		private static object CreateObject(string AssemblyPath,string classNamespace)
		{			
			object objType = DataCache.GetCache(classNamespace);
			if (objType == null)
			{
				try
				{
                    Assembly ts = Assembly.Load(AssemblyPath);
					objType = ts.CreateInstance(classNamespace);					
					DataCache.SetCache(classNamespace, objType);// д�뻺��
				}
				catch//(System.Exception ex)
				{
					//string str=ex.Message;// ��¼������־
				}
			}
			return objType;
		}
        #endregion

		/// <summary>
		/// ����Admin���ݲ�ӿ�
		/// </summary>
		public static torsion.IDAL.IAdmin CreateUser()
		{

			string ClassNamespace = AssemblyPath +".Admin";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
            return (torsion.IDAL.IAdmin)objType;
		}

}
}