﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Data;

/// <summary>
/// CRUD 的摘要说明
/// </summary>
public class CRUD
{
    static DbUtility dbhelp = new DbUtility(System.Configuration.ConfigurationManager.ConnectionStrings["mesConn"].ToString(), DbProviderType.MySql);

    public CRUD()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    //添加页面的 绑定下拉选项
    public static void setWorkShopddl_add(DropDownList ddl)
    {
        ddl.AppendDataBoundItems = true;
        string sql = "select paraid , paraname from jh_mes.tparaconfig where paratype='workshop' ";
        using (DbDataReader reader = dbhelp.ExecuteReader(sql, null))
        {
            while (reader.Read())
            {
                ddl.Items.Add(new ListItem(reader["paraname"].ToString(), reader["paraid"].ToString()));
            }
            ddl.DataBind();
        }
    }

    #region 绑定下拉选项通用方法
    public static void Setdll(DropDownList ddl, string ddltype)
    {
        ddl.AppendDataBoundItems = false;
        string sql = "select  paraname,paraid from jh_mes.tparaconfig where paratype = '" + ddltype + "' ; ";

        using (DbDataReader reader = dbhelp.ExecuteReader(sql, null))
        {
            while (reader.Read())
            {
                ddl.Items.Add(new ListItem(reader["paraname"].ToString(), reader["paraid"].ToString()));
            }
            ddl.DataBind();
        }
    }
    #endregion
    #region 查询机台编号
    public static void SetEqpDDL(DropDownList ddl, string workshop, string worksite)
    {
        ddl.AppendDataBoundItems = true;
        ddl.Items.Clear();
        string sql = "select  eqpid,eqpname from jh_mes.teqpinfo where workshopid = '" + workshop + "' and worksiteid = '" + worksite + "' ; ";
        using (DbDataReader reader = dbhelp.ExecuteReader(sql, null))
        {
            while (reader.Read())
            {
                ddl.Items.Add(new ListItem(reader["eqpname"].ToString(), reader["eqpid"].ToString()));
            }
            ddl.DataBind();
        }
    }
    /// <summary>
    /// 查询工单编号
    /// </summary>
    /// <param name="ddl"></param>
    /// <param name="workshop"></param>
    public static void SetWODDL(DropDownList ddl)
    {
        ddl.AppendDataBoundItems = true;
        string sql = "select  workorderid from jh_mes.tworkorderinfo   ; ";
        using (DbDataReader reader = dbhelp.ExecuteReader(sql, null))
        {
            while (reader.Read())
            {
                ddl.Items.Add(new ListItem(reader["workorderid"].ToString(), reader["workorderid"].ToString()));
            }
            ddl.DataBind();
        }
    }
    #endregion
    public static void SetWOCbx(CheckBoxList cbl)
    {
        string sql = "select  workorderid from jh_mes.tworkorderinfo where status='open'  ;";

        DataTable dt = dbhelp.ExecuteDataTable(sql, null);
        cbl.DataSource = dt;
        cbl.DataTextField = "workorderid";
        cbl.DataValueField = "workorderid";
        cbl.DataBind();
    }
    #region 查询流程
    public static void setWorkFlowDDL(DropDownList ddl, string type)
    {
        ddl.AppendDataBoundItems = true;
        string sql = "select  distinct flowid  flowid , remark from jh_mes.tworkflow where flowtype = '" + type + "'  ; ";
        using (DbDataReader reader = dbhelp.ExecuteReader(sql, null))
        {
            while (reader.Read())
            {
                if (type == "Mould")//模具流程下拉选项
                {
                    ddl.Items.Add(new ListItem(reader["flowid"].ToString(), reader["flowid"].ToString()));
                }
                else//制造膜流程下拉选项 显示A、B、C
                {
                    ddl.Items.Add(new ListItem(reader["remark"].ToString(), reader["flowid"].ToString()));
                }
            }
            ddl.DataBind();
        }
    }

    #endregion
    #region 查询制造流程
    public static void setFilmWorkFlowDDL(DropDownList ddl)
    {
        ddl.AppendDataBoundItems = true;
        string sql = "select  distinct flowid flowid from jh_mes.tworkflow where remark<>'' or remark is not null ; ";
        using (DbDataReader reader = dbhelp.ExecuteReader(sql, null))
        {
            while (reader.Read())
            {
                ddl.Items.Add(new ListItem(reader["flowid"].ToString(), reader["flowid"].ToString()));
            }
            ddl.DataBind();
        }
    }
    #endregion

    #region 绑定checkboxlist
    public static void SetCBL(CheckBoxList cbl, string type)
    {
        string sql = "select paraid,paraname from jh_mes.tparaconfig where paratype = '" + type + "'";

        DataTable dt = dbhelp.ExecuteDataTable(sql, null);
        cbl.DataSource = dt;
        cbl.DataTextField = "paraid";
        cbl.DataValueField = "paraname";
        cbl.DataBind();
    }
    #endregion

    #region 查询模具条码是否打印
    public static string CheckMouldLabel(string type, string label)
    {
        string sql = "";
        //外发条码
        if (type == "Z")
        {
            sql = "select carvelotid from jh_mes.tlotbasis where carvelotid = '" + label + "' or  gritlotid = '" + label + "' ;";
        }
        else//自产条码
        {
            sql = "select outwardlotid from jh_mes.tlotbasis where outwardlotid = '" + label + "';";
        }
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
    #endregion 查询膜条码是否打印
    public static string CheckFilmLabel(string type, string label)
    {
        string sql = "";
        //外发条码
        if (type == "UV成型")
        {
            sql = "select lotid from jh_mes.tlotbasis where uvcompletelotid = '" + label + "' order by createtime ;";
        }
        else if (type == "贴膜")
        {
            sql = "select lotid from jh_mes.tlotbasis where pastefilmlotid = '" + label + "' order by createtime ;";
        }
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
    #region
    #endregion

    #region  查询序列号是否可以过站
    public static string QueryStationOfLot(string worksiteID, string lot)
    {
        string flowidno;
        string sql = "select flowidno,mouldlevel ,filmlevel ,processcomplete from tlotbasis where lotid = '" + lot + "' and status = 'Active'";
        using (DbDataReader dr = dbhelp.ExecuteReader(sql, null))
        {
            if (dr.Read())
            {
                if (dr["flowidno"].ToString() != "")
                {
                    flowidno = dr["flowidno"].ToString();
                }
                if (dr["mouldlevel"].ToString() == "Scrap")
                {
                    return "该批次已经报废！";
                }

                //add by lei.xue on 2017-8-2
                if (dr["mouldlevel"].ToString() == "Hold")
                {
                    return "改批次已经被Hold！";
                }

                if (dr["filmlevel"].ToString() == "Scrap")
                {
                    return "该批次已经报废！";
                }
                if (dr["processcomplete"].ToString() == "Y")
                {
                    return "该批次已经结束流程！";
                }
            }
            else
            {
                return "该批次未创建，没有查询到信息！";
            }
        }
        DataTable NextWorksiteID = CRUD.GetNextWorksite(lot);
        if (NextWorksiteID.Rows[0]["worksiteid"].ToString() != worksiteID)
        {
            //return "该批次下一站点不是当前站点，无法过站！";
            return "该批次不在当前站点，当前站点：" + NextWorksiteID.Rows[0]["worksitename"].ToString();
        }
        return "success";
    }
    #endregion
    //查询批次号的返工站点的flowidno
    public static string GetFlowidno(string lot, string worksiteid)
    {
        string sql = "select a.flowidno -1  from jh_mes.tworkflow a inner join jh_mes.tlotbasis b on a.flowid = b.flowid where b.lotid = '" + lot + "' and a.worksiteid='" + worksiteid + "';";
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


    /// <summary>
    /// 查询返工站点对应的流程ID
    /// </summary>
    /// <param name="flowid"></param>
    /// <param name="worksiteid"></param>
    /// <returns></returns>
    public static string GetReworkFlowidno(string flowid, string worksiteid)
    {
        string sql = "select a.flowidno -1 from jh_mes.tworkflow a where a.flowid = '" + flowid + "' and a.worksiteid='" + worksiteid + "'; ";
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

    //查询批次的流程
    public static DataTable GetWorkflow(string lot)
    {
        string sql = "select a.flowid,a.flowidno,a.worksiteid,"
                   + " (select paraname from jh_mes.tparaconfig where paraid = a.worksiteid and paratype = 'worksite') worksitename"
                   + " from jh_mes.tworkflow a inner join tlotbasis b "
                   + " on a.flowid = b.flowid where b.lotid = '" + lot + "' order by flowidno";
        DataTable dt = dbhelp.ExecuteDataTable(sql, null);
        return dt;
    }

    //public static DataTable GetWorkflowByFlowID(string flowID)
    //{
    //    string sql = "select flowid,flowidno,worksiteid,"
    //               + " (select paraname from jh_mes.tparaconfig where paraid = a.worksiteid and paratype = 'worksite') worksitename"
    //               + " from jh_mes.tworkflow   "
    //               + " where flowid = '" + flowID + "' order by flowidno ;";
    //    return dbhelp.ExecuteDataTable(sql, null);
    //}
    public static string GetWorkFlowByFlowID(string flowID)
    {
        //查询批次流程
        string strFlow = "";
        DataTable dt = CRUD.GetWorkflowByFlowid(flowID);
        for (int i = 0; i < dt.Rows.Count; i++)
        {


            strFlow = strFlow + dt.Rows[i]["worksitename"].ToString() + "->";

        }
        if (strFlow != "")
        {
            strFlow = strFlow.Remove((strFlow).Length - 2, 2);
        }
        return strFlow;
    }

    public static DataTable GetWorkflowByFlowid(string flowid)
    {
        string sql = "select a.flowid,a.flowidno,a.worksiteid,"
                   + " (select paraname from jh_mes.tparaconfig where paraid = a.worksiteid and paratype = 'worksite') worksitename"
                   + " from jh_mes.tworkflow a  "
                   + " where a.flowid = '" + flowid + "' order by flowidno";
        DataTable dt = dbhelp.ExecuteDataTable(sql, null);
        return dt;
    }
    public static string GetWorksite(string lot)
    {
        string sql = "select a.worksiteid from jh_mes.tworkflow a inner join tlotbasis b "
                   + " on a.flowid = b.flowid and a.flowidno = b.flowidno "
                   + " where b.lotid = '" + lot + "'";
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

    //由于制造流程每站之后都有检验站，因此需查询批次前两站的ID，用于查询批次之前的长度和宽度
    public static string GetWorksite2(string lot)
    {
        string sql = "select a.worksiteid from jh_mes.tworkflow a inner join tlotbasis b "
                   + " on a.flowid = b.flowid and a.flowidno = b.flowidno-1 "
                   + " where b.lotid = '" + lot + "'";
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

    public static string GetWorksiteName(string lot)
    {
        string sql = "select (select paraname from jh_mes.tparaconfig where paraid =  a.worksiteid and paratype = 'worksite') worksitename from jh_mes.tworkflow a inner join tlotbasis b "
                   + " on a.flowid = b.flowid and a.flowidno = b.flowidno "
                   + " where b.lotid = '" + lot + "'";
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
    public static DataTable GetNextWorksite(string lot)
    {
        string sql = "select a.worksiteid,(select paraname from jh_mes.tparaconfig where paraid =  a.worksiteid and paratype = 'worksite') worksitename "
                   + " from jh_mes.tworkflow a inner join tlotbasis b "
                   + " on a.flowid = b.flowid and a.flowidno = b.flowidno+1 "
                   + " where b.lotid = '" + lot + "'";
        return dbhelp.ExecuteDataTable(sql, null);
    }

    #region 查询模具使用次数
    public static string GetUseQuantityOfLot(string lot)
    {
        string sql = "select lotcount from jh_mes.tlotbasis where lotid ='" + lot + "'";
        object ob = dbhelp.ExecuteScalar(sql, null);
        if (ob != null)
        {
            return ob.ToString();
        }
        else
        {
            return "0";
        }
    }
    #endregion

    #region 批次返工后查询站点是否进站
    //modify by lei.xue on 2017-2-13
    public static DataTable CheckInIsOK(string lot, string worksiteID)
    {
        string sql = "select  a.eqpid ,a.workshopid, a.lotid , a.type,a.createtime from jh_mes.twiplotlog a inner join jh_mes.tlotbasis b on a.lotid = b.lotid and a.lotcount = b.lotcount"
                   + " where a.worksiteid = '" + worksiteID + "' and a.lotid = '" + lot + "' and a.createtime > ifnull((select createtime from treworklog where lotid= '" + lot + "' and (toworksiteid = '" + worksiteID + "' or fromworksiteid = '" + worksiteID + "') order by createtime desc limit 1),'1900-1-1') "
                   + "  order by a.createtime desc ;";
        return dbhelp.ExecuteDataTable(sql, null);
    }
    #endregion

    //查询checkin机台
    //增加rework是否返工字段,processcomplete是否流程结束 modify by lei.xue on 2017-2-13 
    public static DataTable GetCheckInInfo(string lot, string worksiteid)
    {
        string sql = "select a.eqpid ,a.workshopid,b.rework,b.processcomplete from jh_mes.twiplotlog a inner join jh_mes.tlotbasis b on a.lotid = b.lotid and a.lotcount = b.lotcount"
                   + " where a.type = 'CheckIn' and  a.lotid = '" + lot + "' and a.worksiteid = '" + worksiteid + "' order by a.createtime desc ";

        return dbhelp.ExecuteDataTable(sql, null);

    }
    /// <summary>
    /// 查询批次基础信息
    /// </summary>
    /// <param name="lot"></param>
    /// <returns></returns>
    public static DataTable GetLotBasisInfo(string lot)
    {
        string sql = "select   lotid ," +
        "  Carvelotid  ," +
        "  Outwardlotid  ," +
        "  UVCompletelotid  ," +
        "  PasteFilmlotid  ," +
        "  Subsectionlotid  ," +
        "  WorkOrder ," +
        "  ReWorkOrder  ," +
        "  Status  ," +
        "  FactoryID  ," +
        "  WorkshopID  ," +
        "  flowidno," +
        "  worksitename  ," +
        "  checkintime ," +
        "  checkouttime ," +
        "  createtime  ," +
        "  createuser  ," +
        "  flowid  ," +
        "  lottype  ," +
        "  lotcount ," +
        "  MouldLevel," +
        "  Filmlevel," +
        "  rework," +
        "  processcomplete, " +
        "  mouldlength, " +
        "  restlength, " +
        "  mouldwidth, " +
        "  restwidth ," +
        "  eqpid ," +
        " package," +
        " packagetime," +
        " packageuser," +
        " ifnull(validwidth,'') validwidth," +
        " (select mouldtype from tworkorderinfo where workorderid = workorder) producttype," +
        " ifnull(validlength,'') validlength" +
        "  from jh_mes.tlotbasis where lotid = '" + lot + "' ;   ";
        return dbhelp.ExecuteDataTable(sql, null);
    }

    //模具流转次数
    public static string UpdateMouldLotCount(string lot)
    {
        string sql = "update jh_mes.tlotbasis set lotcount = lotcount+1 where lotid = '" + lot + "';";
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

    #region 批次返工
    public static string rework(string FromWorsiteID, string ToWorksiteID, string lot, string user, string flowidno)
    {
        string sql = "insert into jh_mes.treworklog(lotid,createtime,createuser,fromworksiteid,toworksiteid)" +
                     " values('" + lot + "',now(),'" + user + "','" + FromWorsiteID + "','" + ToWorksiteID + "');";
        //模具等级变为不良
        sql += " update jh_mes.tlotbasis set mouldlevel = '不良' where lotid = '" + lot + "';";
        //更新返工站点 
        //批次状态变为返工 modify by lei.xue on 2017-2-13
        sql = sql + "update jh_mes.tlotbasis set flowidno = " + flowidno + " , rework = 'Y' where lotid  = '" + lot + "' ;";

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
    #endregion

    /// <summary>
    /// 变更批次流程
    /// </summary>
    /// <param name="flowid"></param>
    /// <param name="lotid"></param>
    /// <returns></returns>
    public static string ChangLotWorkflow(string flowid, string lotid)
    {
        string sql1 = "";
        string sql = " update jh_mes.tlotbasis set flowid = '" + flowid + "' where lotid = '" + lotid + "' ; ";
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


    #region 进站通用方法
    public static string CheckIn(string lot, string eqp, string workshopid, string worksiteid, string userid)
    {
        string lotUseQuantity = CRUD.GetUseQuantityOfLot(lot);
        string lotserial = lot + System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
        string sql = "insert into jh_mes.twiplotlog(lotid,lotserial,eqpid,createtime,createuser,workshopid,worksiteid,type,lotcount)"
                   + "values('" + lot + "','" + lotserial + "','" + eqp + "',now(),'" + userid + "','" + workshopid + "','" + worksiteid + "','CheckIn'," + lotUseQuantity + ");";

        //更新checkin 时间
        sql = sql + "update tlotbasis set checkintime = now() where lotid = '" + lot + "';";

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
    #endregion
    #region 出站通用方法
    public static string CheckOut(string lot, string labellot, string eqp, string workshopid, string worksiteid, string userid)
    {
        string type = "";
        string sql = "";
        string ColumnName = "";
        string flowid = "";
        DataTable dt = CRUD.GetWorkflow(lot);
        flowid = dt.Rows[0]["flowid"].ToString();

        if (labellot.Length == 10)
        {
            type = labellot.Substring(3, 1).ToString();
        }
        else
        {
            type = "";
        }

        string lotUseQuantity = CRUD.GetUseQuantityOfLot(lot);
        string lotserial = lot + System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
        //if (type == "Z")
        //{
        //    ColumnName = "carvelotid";
        //}
        //else
        //{
        //    ColumnName = "outwardlotid";
        //}
        if (flowid == "flow001")
        {
            ColumnName = "carvelotid";
        }
        else if (flowid == "flow002")
        {
            ColumnName = "gritlotid";
        }
        else
        {
            ColumnName = "outwardlotid";
        }
        sql = "insert into jh_mes.twiplotlog(lotid,lotserial,eqpid,createtime,createuser,workshopid,worksiteid,type,lotcount," + ColumnName + ")"
                   + " values('" + lot + "','" + lotserial + "','" + eqp + "',now(),'" + userid + "','" + workshopid + "','" + worksiteid + "','CheckOut'," + lotUseQuantity + ",'" + labellot + "');";

        //更新checkOut 时间
        sql = sql + "update tlotbasis set checkouttime = now(),flowidno = flowidno+1," + ColumnName + " = '" + labellot + "' where lotid = '" + lot + "';";

        ////插入镀层材料
        //sql = sql + "insert into jh_mes.twiplotdetail(lotserial,worksiteid,paratype,paraid,createuser,createtime)"
        //          + " values('" + lotserial + "','" + worksiteid + "','DCMaterial','" + DCMaterial + "','" + userid + "',now());";
        ////插入镀层厚度
        //sql = sql + "insert into jh_mes.twiplotdetail(lotserial,worksiteid,paratype,paraid,createuser,createtime)"
        //  + " values('" + lotserial + "','" + worksiteid + "','DCThinkness','" + DCThinkness + "','" + userid + "',now());";

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
    #endregion

    /// <summary>
    /// PET膜过站方法
    /// </summary>
    /// <param name="lot"></param>
    /// <param name="carvelot"></param>
    /// <param name="eqp"></param>
    /// <param name="workshopid"></param>
    /// <param name="worksiteid"></param>
    /// <param name="userid"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 制造流程检验站出站方法
    /// </summary>
    /// <param name="lot"></param>
    /// <param name="labellot"></param>
    /// <param name="eqp"></param>
    /// <param name="workshopid"></param>
    /// <param name="worksiteid"></param>
    /// <param name="userid"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public static string FilmCheckOut(string lot, string labellot, string eqp, string workshopid, string worksiteid, string userid,
                                   string level)
    {
        string columnname = "UVCompletelotid";//字段名默认值
        //string lotUseQuantity = CRUD.GetUseQuantityOfLot(lot);
        string lotserial = lot + System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
        string sql = "insert into jh_mes.twiplotlog(lotid,lotserial,eqpid,createtime,createuser,workshopid,worksiteid," + columnname + ")"
                   + " values('" + lot + "','" + lotserial + "','" + eqp + "',now(),'" + userid + "','" + workshopid + "','" + worksiteid + "', '" + labellot + "');";

        //更新checkOut 时间
        sql = sql + "update tlotbasis set checkouttime = now(),flowidno = flowidno+1 where lotid = '" + lot + "';";
        //等级变更
        //sql += " update jh_mes.tlotbasis set mouldlevel = '"+level+"' where lotid = '" + lot + "';";

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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="FromWorsiteID"></param>
    /// <param name="ToWorksiteID"></param>
    /// <param name="lot"></param>
    /// <param name="user"></param>
    /// <param name="flowidno"></param>
    /// <returns></returns>
    public static string updateFilmLevel(string FromLevel, string ToLevel, string lot, string user, string worksiteid)
    {
        string sql = "insert into jh_mes.tupdatefilmlevellog(lotid,createtime,createuser,fromlevel,tolevel,worksiteid)" +
                     " values('" + lot + "',now(),'" + user + "','" + FromLevel + "','" + ToLevel + "','" + worksiteid + "');";
        //模具等级变为不良
        sql += " update jh_mes.tlotbasis set filmlevel = '" + ToLevel + "' where lotid = '" + lot + "';";

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

    /// <summary>
    /// 查询批次某个站点的过站信息 从twiplotdetail表取数据
    /// </summary>
    /// <param name="lot"></param>
    /// <param name="worksiteid"></param>
    /// <returns></returns>
    public static DataTable getStationInfo(string lot, string worksiteid)
    {
        string sql = "select a.paratype, a.paraid,b.eqpid,b.workshopid from jh_mes.twiplotdetail a inner join "
                   + " jh_mes.twiplotlog b on a.lotserial = b.lotserial where b.lotid = '" + lot + "' and b.worksiteid = '" + worksiteid + "' ;";
        return dbhelp.ExecuteDataTable(sql, null);

    }

    public static DataTable getStationInfoOfMould(string lot, string worksiteid)
    {
        string sql = "select a.paratype, a.paraid,b.eqpid,b.workshopid from jh_mes.twiplotdetail a  inner join "
                   + " jh_mes.twiplotlog b on a.lotserial = b.lotserial inner join jh_mes.tlotbasis c on c.lotid = b.lotid and c.lotcount = b.lotcount  where c.lotid = '" + lot + "' and b.worksiteid = '" + worksiteid + "' ;";
        return dbhelp.ExecuteDataTable(sql, null);
    }

    public static void setLabelProcess(Label BeginLabel, Label MiddleLabel, Label EndLabel, string lot, string worksiteID)
    {
        string WorksiteIDOfLot = CRUD.GetWorksite(lot);
        string CurrentSite = "N";
        string EndSite = "N";
        string BeginFlow = "";
        string EndFlow = "";
        string strFlow = "";
        DataTable dt = CRUD.GetWorkflow(lot);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //if (WorksiteIDOfLot == dt.Rows[i]["worksiteID"].ToString())
            //{
            //    strFlow = strFlow + "[" + dt.Rows[i]["worksitename"].ToString() + "]" + "->";
            //    lblCurrnentWorksite.Text = dt.Rows[i]["worksitename"].ToString();
            //}
            //else
            //{
            //    strFlow = strFlow + dt.Rows[i]["worksitename"].ToString() + "->";
            //}

            if (EndSite == "Y")
            {
                EndFlow = EndFlow + dt.Rows[i]["worksitename"].ToString() + "->";
            }
            if (WorksiteIDOfLot == dt.Rows[i]["worksiteID"].ToString())
            {
                strFlow = strFlow + "[" + dt.Rows[i]["worksitename"].ToString() + "]" + "->";
                MiddleLabel.Text = "[" + dt.Rows[i]["worksitename"].ToString() + "]" + "->";
                CurrentSite = "Y";
                EndSite = "Y";
            }
            if (CurrentSite == "N")
            {
                BeginFlow = BeginFlow + dt.Rows[i]["worksitename"].ToString() + "->";
            }


        }
        if (EndFlow != "" && BeginFlow == "" && MiddleLabel.Text != "")
        {
            EndFlow = EndFlow.Remove((EndFlow).Length - 2, 2);
        }
        if (EndFlow == "" && BeginFlow != "" && MiddleLabel.Text != "")
        {
            MiddleLabel.Text = MiddleLabel.Text.Remove((MiddleLabel.Text).Length - 2, 2);
        }

        if (EndFlow == "" && BeginFlow != "" && MiddleLabel.Text == "")
        {
            BeginFlow = BeginFlow.Remove((BeginFlow).Length - 2, 2);
        }

        if (EndFlow != "" && BeginFlow != "" && MiddleLabel.Text != "")
        {
            EndFlow = EndFlow.Remove((EndFlow).Length - 2, 2);
        }

        //string strFlow = "";

        BeginLabel.Text = BeginFlow;

        //当前站点显示颜色 modify by lei.xue on 2017-2-12
        //lblCurrnentWorksite.ForeColor = System.Drawing.Color.FromName("#FFFFFF");
        //lblCurrnentWorksite.BackColor = System.Drawing.Color.FromName("#0080FF");

        EndLabel.Text = EndFlow;
    }

    public static string RestartMouldLotProcess(string lot)
    {
        string sql = "update jh_mes.tlotbasis set flowidno = 0,processcomplete='N' where processcomplete = 'Y' and lotid = '" + lot + "' ;";
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

    public static string RestartMouldLotProcess(string lot, string flowid)
    {
        string sql = "update jh_mes.tlotbasis set flowidno = 0,flowid = '" + flowid + "' ,processcomplete='N' where processcomplete = 'Y' and lotid = '" + lot + "' ;";
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

    //public static string ExistCheck(string lot, string worksiteid)
    //{
    //    //string sql = "select lotid "
    //}


}