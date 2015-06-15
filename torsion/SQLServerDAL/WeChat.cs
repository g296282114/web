﻿using System;
using System.Collections.Generic;
using System.Text;
using torsion.IDAL;
using Maticsoft.DBUtility;
using System.Data.SqlClient;
using System.Data;//请先添加引用

namespace torsion.SQLServerDAL
{
    public class WeChat:IWeChat
    {
        public WeChat()
        {

        }
        public torsion.Model.WeChat GetModel()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select conName,conValue from WeChat_Conf ");

            torsion.Model.WeChat model = new torsion.Model.WeChat();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            foreach (DataRow dw in ds.Tables[0].Rows)
            {
                switch (dw["conName"].ToString())
                {

                    case "token":
                        model.token = dw["conValue"].ToString();
                        break;
                    case "acToken":
                        model.acToken = dw["conValue"].ToString();
                        break;
                    case "WeChatID":
                        model.WeChatID = dw["conValue"].ToString();
                        break;
                    case "appID":
                        model.appID = dw["conValue"].ToString();
                        break;
                    case "appsecret":
                        model.appsecret = dw["conValue"].ToString();
                        break;
                    case "userID":
                        model.userID = dw["conValue"].ToString();
                        break;

                }

            }

            return model;
        }
       

    }
}
