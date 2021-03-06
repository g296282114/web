using System;
using System.Reflection;
using System.Configuration;
namespace torsion.DALFactory
{
	/// <summary>
    /// Abstract Factory pattern to create the DAL。
    /// 如果在这里创建对象报错，请检查web.config里是否修改了<add key="DAL" value="Maticsoft.SQLServerDAL" />。
	/// </summary>
	public sealed class DataAccess
	{
        private static readonly string AssemblyPath = ConfigurationManager.AppSettings["DAL"];        
		public DataAccess()
		{ }

        #region CreateObject 

		//不使用缓存
        private static object CreateObjectNoCache(string AssemblyPath,string classNamespace)
		{		
			try
			{
				object objType = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);	
				return objType;
			}
			catch//(System.Exception ex)
			{
				//string str=ex.Message;// 记录错误日志
				return null;
			}			
			
        }
		//使用缓存
		private static object CreateObject(string AssemblyPath,string classNamespace)
		{			
			object objType = DataCache.GetCache(classNamespace);
			if (objType == null)
			{
				try
				{
                    Assembly ts = Assembly.Load(AssemblyPath);
					objType = ts.CreateInstance(classNamespace);					
					DataCache.SetCache(classNamespace, objType);// 写入缓存
				}
				catch//(System.Exception ex)
				{
					//string str=ex.Message;// 记录错误日志
				}
			}
			return objType;
		}
        #endregion

		/// <summary>
		/// 创建Admin数据层接口
		/// </summary>
		public static torsion.IDAL.IAdmin CreateUser()
		{

			string ClassNamespace = AssemblyPath +".Admin";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
            return (torsion.IDAL.IAdmin)objType;
		}
        public static torsion.IDAL.IAttendance CreateAttendance()
		{

            string ClassNamespace = AssemblyPath + ".Attendance";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
            return (torsion.IDAL.IAttendance)objType;
		}
        public static torsion.IDAL.IAttendanceSet CreateAttendanceSet()
        {

            string ClassNamespace = AssemblyPath + ".AttendanceSet";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (torsion.IDAL.IAttendanceSet)objType;
        }
        public static torsion.IDAL.IWeChat CreateWeChat()
        {

            string ClassNamespace = AssemblyPath + ".WeChat";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (torsion.IDAL.IWeChat)objType;
        }
        public static torsion.IDAL.ISoftInfo CreateSoftInfo()
        {

            string ClassNamespace = AssemblyPath + ".SoftInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (torsion.IDAL.ISoftInfo)objType;
        }
        public static torsion.IDAL.IDeviceInfo CreateDeviceInfo()
        {
            string ClassNamespace = AssemblyPath + ".DeviceInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (torsion.IDAL.IDeviceInfo)objType;
        }
        public static torsion.IDAL.IMenus CreateMenus()
        {
            string ClassNamespace = AssemblyPath + ".Menus";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (torsion.IDAL.IMenus)objType;
        }
        public static torsion.IDAL.IUserInfo CreateUserInfo()
        {
            string ClassNamespace = AssemblyPath + ".UserInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (torsion.IDAL.IUserInfo)objType;
        }

        

}
}