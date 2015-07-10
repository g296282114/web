using System;
using System.Collections.Generic;
using System.Text;
using torsion.DALFactory;
using torsion.IDAL;
using System.Data;
using Newtonsoft.Json;


namespace torsion.BLL
{
    public class SoftInfo
    {
        
        private readonly ISoftInfo dal = DataAccess.CreateSoftInfo();
        private readonly IDeviceInfo idi = DataAccess.CreateDeviceInfo();

        public static List<Model.SoftInfo> gl_si = new List<Model.SoftInfo>();

        public int Update_SoftInfo(torsion.Model.SoftInfo tsi)
        {
            return dal.Update_SoftInfo(tsi);
        }
        public int NewRecord(torsion.Model.Attendance.JsonAttendance aja)
        {

            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }
        public int OpenLock(string access_token)
        {
            try
            {
                Model.SoftInfo si = get_SoftInfo(access_token);
                if (si == null)
                    return 0;
                return ServerSendData(si, torsion.Model.GlfGloVar.CMD_OPENLOCK, "0", 2);
            }
            catch (Exception e)
            {
                Model.GlfGloFun.Write_Err(e.Message);
            }
            return 0;
        }
        public int DeviceList(string access_token)
        {
            try
            {
                Model.SoftInfo si = get_SoftInfo(access_token);
                if (si == null)
                    return 0;
                DataSet ds = idi.DeviceInfoSoftId(si.id);
                torsion.Model.DeviceInfo[] di = new Model.DeviceInfo[ds.Tables[0].Rows.Count];
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    di[j] = new torsion.Model.DeviceInfo();
                    di[j].DeviceId = Convert.ToInt32(ds.Tables[0].Rows[j]["DeviceId"]);
                    di[j].deviceSet = new Model.DeviceInfo.DeviceSet();
                    Model.GlfGloFun.get_DataRow(ds.Tables[0], j, di[j].deviceSet);
                }
                return ServerSendData(si, torsion.Model.GlfGloVar.CMD_DEVICELIST, JsonConvert.SerializeObject(di), 2);
            }
            catch (Exception e)
            {
                Model.GlfGloFun.Write_Err(e.Message);
            }
            return 0;
            

        }
        public static int ServerSendData(Model.SoftInfo si, int cmd, string sendstr, int stat)
        {
            Boolean blsend = false;
            TimeSpan ts = DateTime.Now - si.lastTime;
            if (ts.TotalSeconds > torsion.Model.GlfGloVar.SENDTIMEOUT / 1000)
            {
                return 2;
            }
      
            
            for (int i = 0; i < torsion.Model.GlfGloVar.SENDTIMEOUT / torsion.Model.GlfGloVar.SERVER_SLEEP_TIME; i++)
            {
                if (si.cmd == torsion.Model.GlfGloVar.CMD_HEARTBEAT && !blsend)
                {
                    blsend = true;
                    si.cmd = cmd;
                    si.sendStr = sendstr;
                    si.conStat = stat;
                }
                if (si.conStat >= 4 && blsend)
                {
                    si.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
                    return torsion.Model.GlfGloVar.RE_SUCCESS;
                }
                    
                System.Threading.Thread.Sleep(torsion.Model.GlfGloVar.SERVER_SLEEP_TIME);
            }
            si.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
            return 4;
        }
        
        
        public Model.JsonModel.RecData ComDevice(string access_token)
        {
            Model.SoftInfo si;
            Model.JsonModel.RecData rjmrd = new Model.JsonModel.RecData();
            try
            {
                for (int i = 0; i <= torsion.Model.GlfGloVar.SERVER_POST_TIMEOUT; i += torsion.Model.GlfGloVar.SERVER_SLEEP_TIME)
                {
                    si = get_SoftInfo(access_token);
                    if (si == null)
                    {
                        rjmrd.cmd = torsion.Model.GlfGloVar.CMD_NEEDCONNECT;
                        rjmrd.stat = 1;
                        return rjmrd;
                    }
                    if (si.cmd != torsion.Model.GlfGloVar.CMD_HEARTBEAT)
                    {
                        rjmrd.cmd = si.cmd;
                        rjmrd.stat = si.conStat;
                        rjmrd.cdata = si.sendStr;
                        Model.GlfGloFun.Write_Log("sendCmd:"+si.softName + ":" + si.cmd);
                        return rjmrd;
                    }
                    si.lastTime = DateTime.Now;
                    System.Threading.Thread.Sleep(torsion.Model.GlfGloVar.SERVER_SLEEP_TIME);

                }
            }
            catch (Exception e)
            {
                Model.GlfGloFun.Write_Err(e.Message);
            }
            
            rjmrd.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
            rjmrd.stat = 1;
            return rjmrd;
                
        }
        public Model.JsonModel.RecData RecDevice(string access_token, Model.JsonModel.RecData jmrd)
        {
            Model.JsonModel.RecData rjmrd = new Model.JsonModel.RecData();
            Model.SoftInfo si;
            try
            {
                si = get_SoftInfo(access_token);
                if (si == null)
                {
                    rjmrd.cmd = torsion.Model.GlfGloVar.CMD_NEEDCONNECT;
                    rjmrd.stat = 1;
                    return rjmrd;
                }
                if (jmrd.stat >= 4)
                {
                    if (jmrd.stat > 4)
                        si.recStr = jmrd.cdata;
                    si.conStat = jmrd.stat;
                    jmrd.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
                }
                switch (jmrd.cmd)
                {
                    case torsion.Model.GlfGloVar.CMD_DEVICELIST:
                        DeviceList(access_token);
                        break;
                    case torsion.Model.GlfGloVar.CMD_NEWRECORD:
                        torsion.Model.Attendance.JsonAttendance aja = new Model.Attendance.JsonAttendance();
                        aja = Newtonsoft.Json.JsonConvert.DeserializeObject<torsion.Model.Attendance.JsonAttendance>(jmrd.cdata);
                        torsion.BLL.Attendance abll = new Attendance();
                        int innum = abll.Insert_AttendanceInfo(aja.aai);
                        torsion.Model.GlfGloFun.Write_Log("NewRecord:" + si.softName + " rec:" + aja.sendnum + " ins:" + innum);
                        rjmrd.stat = 4;

                        break;
                    default:
                        rjmrd.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
                        rjmrd.stat = 1;
                        break;
                }
            }
            catch (Exception e)
            {
                Model.GlfGloFun.Write_Err(e.Message);
            }
            
            return rjmrd;
        }
        //public Model.JsonModel.RecData heartbeat(string access_token, Model.JsonModel.RecData jmrd,Model.JsonModel.RecData rjmrd)
        //{
        //    torsion.Model.GlfGloVar.TEST_STRING += "com_softinfo" + "    " + access_token+"<br/>";
        //     Model.SoftInfo si ;
        //    for (int i = 0; i <= torsion.Model.GlfGloVar.SERVER_POST_TIMEOUT ; i += torsion.Model.GlfGloVar.SERVER_SLEEP_TIME)
        //    {
        //        si = get_SoftInfo(access_token);

        //        if (si == null)
        //        {
        //            rjmrd.cmd = torsion.Model.GlfGloVar.CMD_NEEDCONNECT;
        //            return rjmrd;
        //        }
                
        //        if (jmrd.stat >= 4)
        //        {
        //            si.conStat = jmrd.stat;
        //            if (jmrd.stat == 4)
        //            {
        //                si.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
                        
        //            }
        //            else
        //            {
                        
        //                si.recStr = jmrd.cdata;
        //            }
        //            jmrd.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
        //        }
 
        //        if (si.cmd == torsion.Model.GlfGloVar.CMD_HEARTBEAT && jmrd.cmd == torsion.Model.GlfGloVar.CMD_HEARTBEAT)
        //        {
        //            System.Threading.Thread.Sleep(torsion.Model.GlfGloVar.SERVER_SLEEP_TIME);
        //        }
        //        else
        //        {
        //            try
        //            {
        //                Boolean blsend = false;
        //                int tcmd = jmrd.cmd;
        //                if (jmrd.cmd == torsion.Model.GlfGloVar.CMD_HEARTBEAT)
        //                {
        //                    blsend = true;
        //                    tcmd = si.cmd;
        //                    rjmrd.stat = si.conStat;

        //                }
                        
        //                rjmrd.cmd = tcmd;
        //                switch (tcmd)
        //                {
        //                    case torsion.Model.GlfGloVar.CMD_DEVICELIST:
                              
        //                         DataSet ds = idi.DeviceInfoSoftId(si.id);
        //                            torsion.Model.DeviceInfo[] di = new Model.DeviceInfo[ds.Tables[0].Rows.Count];
        //                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        //                            {
        //                                di[j] = new torsion.Model.DeviceInfo();
        //                                di[j].DeviceId = Convert.ToInt32(ds.Tables[0].Rows[j]["DeviceId"]);
        //                                di[j].deviceSet = new Model.DeviceInfo.DeviceSet();
        //                                Model.GlfGloFun.get_DataRow(ds.Tables[0], j, di[j].deviceSet);
        //                            }
        //                            rjmrd.cdata = JsonConvert.SerializeObject(di);    
                                
        //                        break;
        //                    case torsion.Model.GlfGloVar.CMD_NEWRECORD:
        //                        rjmrd.stat = 4;
        //                        break;
        //                    default:
        //                        rjmrd.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
        //                        break;
        //                }
        //                if(si.conStat == 1)
        //                    si.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
        //                si.conStat = 3;

        //            }
        //            catch(Exception e)
        //            {
        //                rjmrd.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
        //                Model.GlfGloFun.Write_Err(e.Message);
        //            }
        //            return rjmrd;
        //        }  
        //    }
        //    return rjmrd;
        //}

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
            tsi.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
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
