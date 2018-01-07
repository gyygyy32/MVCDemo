﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Web.UI.WebControls;
/// <summary>
/// LineOutputReport 的摘要说明
/// </summary>
public class LineOutputReport
{
    static DbUtility dbhelp = new DbUtility(System.Configuration.ConfigurationManager.ConnectionStrings["mesConn"].ToString(), DbProviderType.MySql);

	public LineOutputReport()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static void setWorksiteDDL(DropDownList ddl)
    {
        ddl.AppendDataBoundItems = true;
        ddl.Items.Add(new ListItem("AG涂布", "M35"));
 
        ddl.Items.Add(new ListItem("UV背涂", "M40"));
 
        ddl.Items.Add(new ListItem("UV成型", "M45"));
      
        ddl.Items.Add(new ListItem("贴膜", "M50"));
 
        ddl.DataBind();
    }
    public static DataTable QueryData(string bt, string et, string wotype, string woproducttype, string wowidth, string wothinkness, string worksite,string classname)
    {
        string sql = "call prLineOutputReport('" + bt + "','"
                                            + et + "','','" + woproducttype + "','" + wothinkness + "','" + wotype + "','" + wowidth + "','" + worksite + "','"+classname+"');";
        return dbhelp.ExecuteDataTable(sql, null);
    }
}