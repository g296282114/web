using System;
using System.Collections.Generic;
using System.Text;
using torsion.DALFactory;
using torsion.IDAL;

namespace torsion.BLL
{
    public class SoftInfo
    {
        
        private readonly ISoftInfo dal = DataAccess.CreateSoftInfo();

        public const int HeartBeatTime = 240000;//4分钟
        public static List<Model.SoftInfo> gl_si = null;

        public int Update_SoftInfo(torsion.Model.SoftInfo tsi)
        {
            return dal.Update_SoftInfo(tsi);
        }

        public Model.JsonModel.RecData heartbeat(Model.SoftInfo si, Model.JsonModel.RecData jmrd)
        {
            Model.JsonModel.RecData rsmrd = new Model.JsonModel.RecData();

            for(int i=0;i<=HeartBeatTime;i++)
            {
                if (si.cmd != 0)
                {
                    rsmrd.cmd = si.cmd;
                    rsmrd.cdata = si.sendStr;
                    return rsmrd;
                }
                System.Threading.Thread.Sleep(500);
            }
            rsmrd.cmd = 0;
            return rsmrd;
        }

        public int con_SoftInfo(torsion.Model.SoftInfo tsi)
        {
            int ri = dal.con_SoftInfo(tsi);
            clean_SoftInfo(tsi.secretKey);
            do
            {
                tsi.assess_token = Model.GlfGloFun.GenerateCheckCode();
            }
            while(get_SoftInfo(tsi.assess_token) != null);
            gl_si.Add(tsi);
            return ri;
        }

        public Model.SoftInfo get_SoftInfo(string access_token)
        {
            Model.SoftInfo rsi = null;
            foreach (Model.SoftInfo si in gl_si)
            {
                if (si.assess_token == access_token)
                {
                    rsi = si;
                }
            }
            return rsi;
        }
        public void clean_SoftInfo(string secretKey = "")
        {
            foreach (Model.SoftInfo si in gl_si)
            {
                if (si.lastTime.AddHours(1) < DateTime.Now || si.secretKey == secretKey)
                {
                    gl_si.Remove(si);
                }
            }
        }


    }
}
