using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using Newtonsoft.Json;
namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int ATDAY = 2;
        
        private void button1_Click(object sender, EventArgs e)
        { 
            torsion.Model.AttendanceSet.StaffClasses json = new torsion.Model.AttendanceSet.StaffClasses();
            torsion.BLL.AttendanceSet webll = new torsion.BLL.AttendanceSet();
            json.sid = 0;
            json.cid = 0;
            json.c_start = DateTime.Now.AddDays(-1);
            json.c_end = DateTime.Now;
            json.type = 1;
            json.num = 1;
            json.days = "_1_1_1111111";

    
            webll.insert_StaffClasses(json);
        }

        void dataGridColumnName( torsion.BLL.AttendanceSet webll,DataGridViewColumnCollection agvcc)
        {
            foreach (DataGridViewColumn agvc in agvcc)
            {
                torsion.Model.AttendanceSet.ResultUnit asru = webll.ResultUnitByName(agvc.DataPropertyName);
                if (asru != null)
                {
                    string unitName = "";
                    switch (asru.Units)
                    {
                        case 0:
                            unitName = "[分]";
                            break;
                        case 1:
                            unitName = "[时]";
                            break;
                        case 2:
                            unitName = "[天]";
                            break;
                        case 3:
                            unitName = "[次]";
                            break;
                    }
                    agvc.HeaderText += unitName;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // torsion.Model.AttendanceSet.StaffClasses json = new torsion.Model.AttendanceSet.StaffClasses();
            torsion.BLL.AttendanceSet webll = new torsion.BLL.AttendanceSet();
            DataSet ds = new DataSet();
          //  webll.set_tjDataSet(ds);
           // json.id = 1;
            
            //torsion.Model.AttendanceSet.DatasetStat.DRAttendanceInfo drai = new torsion.Model.AttendanceSet.DatasetStat.DRAttendanceInfo();
            //webll.add_DataRow(ds,drai);
            int[] sid = new int[1];
            sid[0] = 0;
           
            //torsion.Model.AttendanceSet.DatasetStat.DRAttendanceInfo drai = new torsion.Model.AttendanceSet.DatasetStat.DRAttendanceInfo();
            //webll.add_DataRow(ds, drai);
            webll.get_StatDataSet(ds, sid, dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.DataSource = ds.Tables["DRAttendanceInfo"];
            dataGridView2.DataSource = ds.Tables["DRClassesInfo"];
            dataGridView3.DataSource = ds.Tables["DRResultInfo"];

            dataGridColumnName(webll,dataGridView2.Columns);
            dataGridColumnName(webll,dataGridView3.Columns);
            

            //webll.get_StaffClasses(json);
        }

        public int StaffDayClass(int sid,DateTime day,DataSet ds,int dsi,torsion.Model.AttendanceSet.ClassesInfo asci,DataSet tjds)
        {
            int rei = dsi;
            if (ds.Tables[0].Rows.Count == 0)
                return rei;
            Boolean blw = true;
            DateTime dt;
            DateTime? sdt=null;
            DateTime? edt = null;
            while(blw)
            {
                dt = Convert.ToDateTime(ds.Tables[0].Rows[dsi]["CheckTime"].ToString());
                if(dt>=day.AddMinutes(asci.valid_s) && dt < day.AddMinutes(asci.valid_e))
                {
                    if (!sdt.HasValue)
                    {
                        sdt = dt;
                        edt = dt;
                    }

                    if (dt > edt)
                        edt = dt;

                }
                if (dt > day.AddMinutes(asci.valid_e))
                    blw = false;
                if (dsi >= ds.Tables[0].Rows.Count-1)
                    blw = false;

                if (dt < day.AddDays(-ATDAY) )
                    rei = dsi;
                dsi++;
            }




            if (sdt.HasValue || edt.HasValue)
            {
                if (!edt.HasValue)
                    edt = sdt;
                DataRow dr = tjds.Tables["Classes"].NewRow();
                dr["Useid"] = sid;
                dr["ClassesDate"] = day;
                dr["ClassesName"] = asci.t_name;
                dr["StartWork"] = day.AddMinutes(asci.work_s);
                dr["EndWork"] = day.AddMinutes(asci.work_e);
                dr["InsWork"] = sdt;
                dr["OutWork"] = edt;
                dr["Work"] = 0;
                dr["Late"] = 0;
                dr["Early"] = 0;
                dr["OverTime"] = 0;
                dr["WorkTime"] = 0;
                dr["RealTime"] = 0;


                TimeSpan ts;
                ts = (edt.Value > day.AddMinutes(asci.work_e) ? day.AddMinutes(asci.work_e) : edt.Value) - (sdt.Value < day.AddMinutes(asci.work_s) ? day.AddMinutes(asci.work_s) : sdt.Value);
                dr["Work"] = ts.TotalHours;
                if (day.AddMinutes(asci.work_s) < sdt.Value)
                {
                    ts =  sdt.Value - day.AddMinutes(asci.work_s);
                    dr["Late"] = ts.TotalHours;
                }

                if (day.AddMinutes(asci.work_e) > edt.Value)
                {
                    ts = day.AddMinutes(asci.work_e) - edt.Value;
                    dr["Early"] = ts.TotalHours;
                }
                    

                dr["WorkTime"] = dr["Work"];
                dr["RealTime"] = dr["Work"];

                if (asci.timeinfo != null)
                    foreach (torsion.Model.AttendanceSet.TimeInfo asti in asci.timeinfo)
                    {
                        if (!(day.AddMinutes(asti.work_s) > edt || day.AddMinutes(asti.work_e) < sdt))
                        {
                            ts = (edt.Value > day.AddMinutes(asti.work_e) ? day.AddMinutes(asti.work_e) : edt.Value) - (sdt.Value < day.AddMinutes(asti.work_s) ? day.AddMinutes(asti.work_s) : sdt.Value);
                            dr["OverTime"] = Convert.ToDouble(dr["OverTime"]) + ts.TotalHours;
                            dr["WorkTime"] = Convert.ToDouble(dr["WorkTime"]) + ts.TotalHours * asti.rate;
                            dr["RealTime"] = Convert.ToDouble(dr["RealTime"]) + ts.TotalHours;
                        }


                    }
                
                

                //textBox2.Text += sid.ToString().PadRight(10) + "     " + day.ToString("yyyy-MM-dd HH:mm:ss") + "     " + asci.t_name.PadRight(10) + "     " + day.AddMinutes(asci.work_s).ToString("yyyy-MM-dd HH:mm:ss")
                //     + "     " + day.AddMinutes(asci.work_e).ToString("yyyy-MM-dd HH:mm:ss") ;
                //textBox2.Text += "     " + sdt.Value.ToString("yyyy-MM-dd HH:mm:ss");
                ////textBox2.Text += sdt.Value < day.AddMinutes(asci.work_s) ? day.AddMinutes(asci.work_s).ToString("yyyy-MM-dd HH:mm:ss") : sdt.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //textBox2.Text += "     " + edt.Value.ToString("yyyy-MM-dd HH:mm:ss");
                ////textBox2.Text += edt.Value > day.AddMinutes(asci.work_e) ? day.AddMinutes(asci.work_e).ToString("yyyy-MM-dd HH:mm:ss") : edt.Value.ToString("yyyy-MM-dd HH:mm:ss");  
                
                //textBox2.Text += System.Environment.NewLine;
                //if(asci.timeinfo != null)
                //foreach (torsion.Model.AttendanceSet.TimeInfo asti in asci.timeinfo)
                //{
                //    if (!(day.AddMinutes(asti.work_s) > edt || day.AddMinutes(asti.work_e) < sdt))
                //    {
                //        textBox2.Text += "".PadRight(10) + "     " + day.ToString("                   ") + "     " + asti.t_name.PadRight(10) + "     "
                //        + day.AddMinutes(asti.work_s).ToString("yyyy-MM-dd HH:mm:ss") + "     " + day.AddMinutes(asti.work_e).ToString("yyyy-MM-dd HH:mm:ss");
                //        textBox2.Text += "     ";
                //        textBox2.Text += sdt.Value < day.AddMinutes(asti.work_s) ? day.AddMinutes(asti.work_s).ToString("yyyy-MM-dd HH:mm:ss") : sdt.Value.ToString("yyyy-MM-dd HH:mm:ss") ;
                //        textBox2.Text += "     ";
                //        textBox2.Text += edt.Value > day.AddMinutes(asti.work_e) ? day.AddMinutes(asti.work_e).ToString("yyyy-MM-dd HH:mm:ss") : edt.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //        textBox2.Text += System.Environment.NewLine;
                //    }
                    
                    
                //}

                tjds.Tables["Classes"].Rows.Add(dr);
            }


            dataGridView1.DataSource = tjds.Tables["Classes"];
            return rei;
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataSet tjds;
            tjds = new DataSet();
            DataTable dt = new DataTable("Classes");

            dt.Columns.Add(new DataColumn("Useid", typeof(int)));
            dt.Columns.Add(new DataColumn("ClassesDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("ClassesName", typeof(string)));
            dt.Columns.Add(new DataColumn("StartWork", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("EndWork", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("InsWork", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("OutWork", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Work", typeof(Double)));
            dt.Columns.Add(new DataColumn("Late", typeof(Double)));
            dt.Columns.Add(new DataColumn("Early", typeof(Double)));
            dt.Columns.Add(new DataColumn("OverTime", typeof(Double)));
            dt.Columns.Add(new DataColumn("WorkTime", typeof(Double)));
            dt.Columns.Add(new DataColumn("RealTime", typeof(Double)));
            

            tjds.Tables.Add(dt);

            textBox2.Text = "";
            textBox2.Text += "人员id".PadRight(8) + "     " + "班次日期".PadRight(15) + "     " + "班次名".PadRight(7) + "     " + "上班时间".PadRight(15) + "     " + "下班时间".PadRight(15) + "     " + "签到时间".PadRight(15) + "     " + "签退时间";
            textBox2.Text += System.Environment.NewLine;
            dateTimePicker1.Value = dateTimePicker1.Value.Date;
            dateTimePicker2.Value = dateTimePicker2.Value.Date;
            List<torsion.Model.AttendanceSet.StaffClasses> assc = new List<torsion.Model.AttendanceSet.StaffClasses>();
            torsion.BLL.AttendanceSet asdll = new torsion.BLL.AttendanceSet();
            torsion.BLL.Attendance adll = new torsion.BLL.Attendance();
            TimeSpan ts = dateTimePicker2.Value - dateTimePicker1.Value;

           
      
            DateTime tstart = dateTimePicker1.Value;
            int sid = int.Parse(textBox1.Text);



            DataSet ds = adll.get_AttendanceInfo(sid, dateTimePicker1.Value.AddDays(-2), dateTimePicker2.Value.AddDays(2));
            int rds = 0;
                 
            for (int i = 0; i < ts.TotalDays; i++)
            {
                int scday = asdll.get_StaffClassesInfo(assc, 0, DateTime.Now);
                torsion.Model.AttendanceSet.ClassesInfo[] aact = new torsion.Model.AttendanceSet.ClassesInfo[assc.Count];
                for (int j = 0;j<assc.Count ;j++)
                {
                    aact[j] = new torsion.Model.AttendanceSet.ClassesInfo();
                    aact[j].id = assc[j].cid;
                    asdll.get_ClassesSetInfo(aact[j]);
                }

                if (i + scday > ts.TotalDays)
                    scday = Convert.ToInt32(ts.TotalDays)-i;
                for (int j = 0; j < scday; j++)
                {
                    for (int k = 0; k < assc.Count; k++)
                    {
                        if (asdll.bl_StaffClass(assc[k], tstart.AddDays(i)))
                        {
                           rds = StaffDayClass(sid, tstart.AddDays(i),ds,rds,aact[k],tjds);
                        }
                    }

                    i++;
                }
            }
            
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime? dt = null;
            string str = "";
            double db = 1.5;
            int ti = Convert.ToInt32(db);


            MessageBox.Show("bb".PadRight(10));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            torsion.Model.AttendanceSet.DatasetStat nc = new torsion.Model.AttendanceSet.DatasetStat();
            Type t = nc.GetType();
            Assembly assem = Assembly.GetAssembly(t);
            foreach (Type st in assem.GetTypes())
            {
                if (st.DeclaringType == null)
                    continue;
                if (st.DeclaringType.FullName == t.FullName)
                {
                    textBox2.Text += "-------------------------" + System.Environment.NewLine;
                    textBox2.Text += st.Name + System.Environment.NewLine;
                    PropertyInfo[] ci = st.GetProperties();
                    foreach (PropertyInfo c in ci)
                    {
                        textBox2.Text += c.PropertyType +":"+ c.Name + System.Environment.NewLine;
                        

                    }
                }
                

            }
            
    
        }

        public void getname()
    {
        StackTrace st = new StackTrace(true);
        textBox2.Text = "get:" + st.GetFrame(1).GetMethod().Name;
    }
        
        private void button7_Click(object sender, EventArgs e)
        {
            torsion.BLL.AttendanceSet webll = new torsion.BLL.AttendanceSet();
            webll.init_ResultUnit();
            
            foreach(torsion.Model.AttendanceSet.ResultUnit asru in torsion.BLL.AttendanceSet.gl_ResultUnit)
            {
                textBox2.Text += asru.ItemName + "       " + asru.Itemid + "       " + asru.MinUnit.ToString();
                textBox2.Text += System.Environment.NewLine;
            }
           // torsion.Model.AttendanceSet.ResultUnit asru = webll.ResultUnitById(0);
            
        }
        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show(torsion.Model.GlfGloFun.GenerateCheckCode());
            return;
            MessageBox.Show(ConvertUnit(20.1, "").ToString());
            // getname();
            //StackTrace st = new StackTrace(true);
            //textBox2.Text = st.GetFrame(1).GetMethod().Name.ToString(); 
            torsion.Model.GlfGloFun.Write_Err("test");
        }

        public Double ConvertUnit(Double var, string Utype)
        {
            torsion.Model.AttendanceSet.ResultUnit asru = new torsion.Model.AttendanceSet.ResultUnit();
            asru.MinUnit = 0.5m;
            asru.Units = 1;
            asru.SRControl = 0;
            Double rvar = var;
            if (asru != null)
            {
                switch (asru.Units)
                {
                    //分
                    case 0:
                        rvar = var * 60;
                        break;
                    //时
                    case 1:
                        rvar = var;
                        break;
                    //天
                    case 2:
                        rvar = var / 24;
                        break;
                    //次
                    case 3:
                        rvar = 1;
                        break;
                }
                // rvar = (rvar / asru.MinUnit) * asru.MinUnit;
                switch (asru.SRControl)
                {
                    //向下舍弃
                    case 1:
                        rvar = Math.Floor(rvar / (Double)asru.MinUnit) * (Double)asru.MinUnit;
                        break;
                    //向上进位
                    case 2:
                        rvar = Math.Ceiling(rvar / (Double)asru.MinUnit) * (Double)asru.MinUnit;
                        break;
                    //四舍五入
                    default:
                        rvar = Math.Round(rvar / (Double)asru.MinUnit) * (Double)asru.MinUnit;
                        break;

                }
            }
            return rvar;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            torsion.Model.DeviceInfo[] di = new torsion.Model.DeviceInfo[0];
         
            MessageBox.Show( JsonConvert.SerializeObject(di));
        }

    }
}
