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

        public static List<Model.SoftInfo> gl_si = new List<Model.SoftInfo>();

        public int Update_SoftInfo(torsion.Model.SoftInfo tsi)
        {
            return dal.Update_SoftInfo(tsi);
        }

        public Model.JsonModel.RecData heartbeat(string access_token, Model.JsonModel.RecData jmrd,Model.JsonModel.RecData rjmrd)
        {
            torsion.Model.GlfGloVar.TEST_STRING += "com_softinfo" + "    " + access_token+"<br/>";
             Model.SoftInfo si ;
            for (int i = 0; i <= torsion.Model.GlfGloVar.SERVER_POST_TIMEOUT ; i += torsion.Model.GlfGloVar.SERVER_SLEEP_TIME)
            {
                si = get_SoftInfo(access_token);
                if (si == null)
                {
                    rjmrd.cmd = torsion.Model.GlfGloVar.CMD_NEEDCONNECT;
                    return rjmrd;
                }
                switch (si.cmd)
                {

                    case torsion.Model.GlfGloVar.CMD_HEARTBEAT:
                    case 0:
                        System.Threading.Thread.Sleep(torsion.Model.GlfGloVar.SERVER_SLEEP_TIME);
                        break;
                    default:
                        rjmrd.cmd = si.cmd;
                        rjmrd.cdata = si.sendStr;
                        si.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
                        return rjmrd;
                        break;
                }
                
            }
            rjmrd.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
            return rjmrd;
        }

        public int con_SoftInfo(torsion.Model.SoftInfo tsi)
        {
            
            int ri = dal.con_SoftInfo(tsi);
            if (ri != Model.GlfGloVar.RE_SUCCESS)
                return ri;
            clean_SoftInfo(tsi.secretKey);
            do
            {
                tsi.assess_token = Model.GlfGloFun.GenerateCheckCode();
            }
            while(get_SoftInfo(tsi.assess_token) != null);
            gl_si.Add(tsi);
            torsion.Model.GlfGloVar.TEST_STRING += "con_softinfo" + "    " + tsi.assess_token + "<br/>";
            return ri;
        }

        public Model.SoftInfo get_SoftInfo(string access_token)
        {
            Model.SoftInfo rsi = null;
            for (int i = 0; i < gl_si.Count; i++)
            {
                if (gl_si[i].assess_token == access_token)
                {
                    rsi = gl_si[i];
                }
            
            }
          
            return rsi;
        }
        public void clean_SoftInfo(string secretKey = "")
        {
            for (int i = 0; i < gl_si.Count; i++)
            {
                if (gl_si[i].lastTime.AddHours(1) < DateTime.Now || gl_si[i].secretKey == secretKey)
                {
                    gl_si.Remove(gl_si[i]);
                }
            }
            
        }


    }
}
