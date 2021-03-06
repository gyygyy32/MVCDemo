﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Pub;

/// <summary>
/// CreateLot 的摘要说明
/// </summary>
public class CreateLot
{
    static DbUtility dbhelp = new DbUtility(System.Configuration.ConfigurationManager.ConnectionStrings["mesConn"].ToString(), DbProviderType.MySql);

    public CreateLot()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static int QueryMaxNum(string WO)
    {
        string sql = " select ifnull(max(right(lotid,3)),0) num from jh_mes.tlotbasis where   lottype = 'Film' and workorder = '"+WO+"' and lotid like '"+WO+"%' -- and date(createtime) =  curdate() ";
        DbDataReader result = dbhelp.ExecuteReader(sql, null);
        if (result.Read())
        {
            return Convert.ToInt32(result["num"]);
        }
        else
        {
            return 0;
        }

    }

    public static DataTable QueryWorkorderIno(string WO)
    {
        string sql = "select " +
  "bomid ," +
  "(select distinct remark from jh_mes.tworkflow where flowid = a.flowid) FlowidAlias ," +
  "flowid,"+
  "workshopid ," +
  "createtime," +
  "createuser ," +
  "MouldPinMin ," +
  "MouldThinkness ," +
  "MouldLength ," +
  "MouldWidth ," +
  "MouldType ," +
  "MouldPETType ," +
  "workordertype  from jh_mes.tworkorderinfo a where workorderid = '" + WO + "';";
        return dbhelp.ExecuteDataTable(sql, null);
    }

   // 更改工单状态 add by lei.xue on 2017-6-23
    public static string ChangWOStatus(string WO,string status)
    {
        string sql = " update tworkorderinfo set status ='"+status+"' where workorderid = '"+WO+"' ; ";
        int result = dbhelp.ExecuteNonQuery(sql, null);
        if (result > 0)
        {
            return "success";
        }
        else
        {
            return "fail";
        }

    }
}