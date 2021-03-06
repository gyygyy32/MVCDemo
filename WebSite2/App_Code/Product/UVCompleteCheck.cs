﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// UVCompleteCheck 的摘要说明
/// </summary>
public class UVCompleteCheck
{
    static DbUtility dbhelp = new DbUtility(System.Configuration.ConfigurationManager.ConnectionStrings["mesConn"].ToString(), DbProviderType.MySql);

	public UVCompleteCheck()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static string CheckInfo(string lotid, string eqpid, string worksiteid, string result, string userid, string mouldlevel, AGCoatingQCDatalist datalist)
    {
        string lotserial = lotid + System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //检验结果
        string sql = "insert into jh_mes.tqclog(lotid,lotserial,worksiteid,eqpid,createtime,createuser,result,mouldlevel)"
                   + " values "
                   + " ('" + lotid + "',"
                   + " '" + lotserial + "',"
                   + " '" + worksiteid + "',"
                   + " '" + eqpid + "',"
                   + " now(),"
                   + " '" + userid + "',"
                   + " '" + result + "',"
                   + " '" + mouldlevel + "');";
        //检验项目
        //1、厚度
        //a、厚度左
        string sqldetail = "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) "
                         + " values('" + lotserial + "','" + worksiteid + "','厚度','左','" + datalist.thinknessleft + "','" + datalist.thinknessresult + "','" + userid + "',now());";
        //b、厚度中
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','厚度','中','" + datalist.thinknessleft + "','" + datalist.thinknessresult + "','" + userid + "',now());";
        //c、厚度右
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','厚度','右','" + datalist.thinknessleft + "','" + datalist.thinknessresult + "','" + userid + "',now());";

        //2、翘曲变形
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','翘曲变形','','" + datalist.buckling + "','" + datalist.bucklingresult + "','" + userid + "',now());";
        //3、MD雾度
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','MD雾度','','" + datalist.MDhaze + "','" + datalist.MDhazeresult + "','" + userid + "',now());";
        //4、MD穿透率
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','MD穿透率','','" + datalist.MDpenetration + "','" + datalist.MDpenetrationresult + "','" + userid + "',now());";
        //5、外观
        //a、左
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','外观','左','" + datalist.appearanceleft + "','" + datalist.appearanceresult + "','" + userid + "',now());";
        //b、右
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','外观','右','" + datalist.appearanceright + "','" + datalist.appearanceresult + "','" + userid + "',now());";
        //6、可用宽幅
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','可用宽幅','','" + datalist.availablewidth + "','" + datalist.availablewidthresult + "','" + userid + "',now());";
        //7、纹路
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','纹路','','" + datalist.lines + "','" + datalist.linesresult + "','" + userid + "',now());";
        //8、百格
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','百格','','" + datalist.baige + "','" + datalist.baigeresult + "','" + userid + "',now());";
        //9、铅笔硬度正面
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','铅笔硬度正','','" + datalist.pencilhardnessFront + "','" + datalist.pencilhardnessFrontresult + "','" + userid + "',now());";
        //10、铅笔硬度反面
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','铅笔硬度反','','" + datalist.pencilhardnessBack + "','" + datalist.pencilhardnessBackresult + "','" + userid + "',now());";
        //11、TD雾度
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','TD雾度','','" + datalist.TDhaze + "','" + datalist.TDhazeresult + "','" + userid + "',now());";
        //12、TD穿透率
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','TD穿透率','','" + datalist.TDpenetration + "','" + datalist.TDpenetrationresult + "','" + userid + "',now());";
        //13、灰度增益比
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','辉度增益比','','" + datalist.brilliancy + "','" + datalist.brilliancyResult + "','" + userid + "',now());";
        //14、高低差
        //a、高低差dH
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','高低差dH','','" + datalist.HeightDifferenceDH + "','" + datalist.HeightDifferenceResult + "','" + userid + "',now());";
        //b、高低差Rz
        sqldetail += "insert into jh_mes.tqcdetail(lotserial,worksiteid,paratype,parasubtype,paraid,result,createuser,createtime) ";
        sqldetail += " values('" + lotserial + "','" + worksiteid + "','高低差Rz','','" + datalist.HeightDifferenceRz + "','" + datalist.HeightDifferenceResult + "','" + userid + "',now());";

        int intResult = dbhelp.ExecuteNonQuery(sql + sqldetail, null);
        if (intResult > 0)
        {
            return "success";
        }
        else
        {
            return "fail";
        }

    }
}