﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
/// <summary>
/// Package 的摘要说明
/// </summary>
public class Package
{
    static DbUtility dbhelp = new DbUtility(System.Configuration.ConfigurationManager.ConnectionStrings["mesConn"].ToString(), DbProviderType.MySql);

	public Package()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string QueryMaxPinHao(string PinHao, string ProductType)
    {
        string sql = "";
        if (ProductType == "成品")
        {
            sql = "select count(*) from jh_mes.tpackinginfo where producttype = '成品' and left(pinhao,4) = '" + PinHao.Substring(0, 4) + "'; ";
            object ob = dbhelp.ExecuteScalar(sql, null);
            if (ob != null)
            {
                return ob.ToString();
            }
            else
            {
                return "fail";
            }
        }
        else
        {
            sql = "select count(*) from jh_mes.tpackinginfo where producttype = '半成品' and left(pinhao,5) = '" + PinHao.Substring(0, 5) + "'; ";
            object ob = dbhelp.ExecuteScalar(sql, null);
            if (ob != null)
            {
                return ob.ToString();
            }
            else
            {
                return "fail";
            }
        }
    }

    //增加装箱前净重和装箱后毛重信息
    public static string InsertPacking(string lot,string PinHao, string productType,string user,string beforeweight,string afterweight)
    {
        string sql = "insert into jh_mes.tpackinginfo(lotid,PinHao,Producttype,createtime,createuser,BeforePackageWeight,AfterPackageWeight "
                   + ")values ("
                   + "'" + lot + "',"
                   + "'" + PinHao + "',"
                   + "'" + productType + "',"
                   + " sysdate(),"
                   + "'" + user + "',"
                   + "'" + beforeweight + "',"
                   + "'" + afterweight + "');"
                   + " update jh_mes.tlotbasis set processcomplete = 'Y',package = 'Y',packagetime = now(),packageuser = '"+user+"' where lotid = '"+lot+"' ;";
                  
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

    public static DataTable GetPackaInfo(string lot)
    {
        string sql = " select * from tpackinginfo where lotid = '"+lot+"'";
        return dbhelp.ExecuteDataTable(sql, null);
    }

    public static string FilmCheckOut(string lot, string labellot, string eqp, string workshopid, string worksiteid, string userid,
                                   string prelength, string prewidth, string length, string width)
    {
        string columnname = "UVCompletelotid";//字段名默认值
        //string lotUseQuantity = CRUD.GetUseQuantityOfLot(lot);
        string lotserial = lot + System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
        string sql = "insert into jh_mes.twiplotlog(lotid,lotserial,eqpid,createtime,createuser,workshopid,worksiteid," + columnname + ")"
                   + " values('" + lot + "','" + lotserial + "','" + eqp + "',now(),'" + userid + "','" + workshopid + "','" + worksiteid + "', '" + labellot + "');";

        //更新checkOut 时间
        sql = sql + "update tlotbasis set checkouttime = now(),flowidno = flowidno+1," + columnname + " = '" + labellot + "',mouldlength = '" + length + "',mouldwidth= '" + width + "',restlength = '" + length + "',restwidth = '" + width + "' where lotid = '" + lot + "';";
        //模具等级变为合格
        //sql += " update jh_mes.tlotbasis set mouldlevel = '合格' where lotid = '" + lot + "';";
        //插入前站卷材长度
        sql = sql + "insert into jh_mes.twiplotdetail(lotserial,worksiteid,paratype,paraid,createuser,createtime)"
                  + " values('" + lotserial + "','" + worksiteid + "','PreMouldLength','" + prelength + "','" + userid + "',now());";
        //插入前站幅宽规格
        sql = sql + "insert into jh_mes.twiplotdetail(lotserial,worksiteid,paratype,paraid,createuser,createtime)"
          + " values('" + lotserial + "','" + worksiteid + "','PreMouldWidth','" + prewidth + "','" + userid + "',now());";
        //插入当站卷材长度
        sql = sql + "insert into jh_mes.twiplotdetail(lotserial,worksiteid,paratype,paraid,createuser,createtime)"
                  + " values('" + lotserial + "','" + worksiteid + "','MouldLength','" + length + "','" + userid + "',now());";
        //插入当站幅宽规格
        sql = sql + "insert into jh_mes.twiplotdetail(lotserial,worksiteid,paratype,paraid,createuser,createtime)"
                  + " values('" + lotserial + "','" + worksiteid + "','MouldWidth','" + width + "','" + userid + "',now());";
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