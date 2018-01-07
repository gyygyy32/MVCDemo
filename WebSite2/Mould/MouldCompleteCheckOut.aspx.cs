﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Pub;

public partial class Mould_MouldCompleteCheckOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonClass.isAllow(User.Identity.Name, this, "成型出站");
            //lblWorksiteID.Text = "M05";
            txtLot.Attributes.Add("onkeypress", "EnterTextBox('btnEnter')");
            //CRUD.SetCBL(cblDCMaterial, "DCMaterial");
            //setddl();
            //默认选中返工 modify by lei.xue on 2017-3-16
            cbxReworkID.Checked = true;

        }

    }
    protected void btnEnter_Click(object sender, EventArgs e)
    {
        //模具编号
        string MouldLot = "";
        #region 判断条码是否有效
        if (txtLot.Text.Length>3)
        {
            //外发或资产条码是否打印 条码第四位判断：W或Z
            if (txtLot.Text.Substring(3, 1).ToString() == "W")
            {
                if (CRUD.CheckMouldLabel("W", txtLot.Text) == "fail")
                {
                    JScript.Alert("该条码为打印！", this);
                    return;
                }
            }
            else if (txtLot.Text.Substring(3, 1).ToString() == "Z")
            {
                if (CRUD.CheckMouldLabel("Z", txtLot.Text) == "fail")
                {
                    JScript.Alert("该条码未打印！", this);
                    return;
                }
            }

            MouldLot = txtLot.Text.Substring(0, 3).ToString();
        }
        else
        {
            MouldLot = txtLot.Text;
        }
        #endregion
        //查询是否可以过站
        string result = CRUD.QueryStationOfLot(lblWorksiteID.Text, MouldLot);
        if (result != "success")
        {
            JScript.Alert(result, this);
            txtLot.Text = "";
            return;
        }
     
        string workshop="";
        string eqp="";
        //DataTable dtCheckIn = CRUD.GetCheckInInfo(MouldLot, lblWorksiteID.Text);
        //if (dtCheckIn.Rows.Count > 0)
        //{
        //    eqp = dtCheckIn.Rows[0]["eqpid"].ToString();
        //    workshop = dtCheckIn.Rows[0]["workshopid"].ToString();
        //}
        //else
        //{
        //    JScript.Alert("批次尚未进站！", this);
        //    txtLot.Text = "";
        //    return;
        //}
        //是否已经checkin
        DataTable ResDt = CRUD.GetCheckInInfo(MouldLot, lblWorksiteID.Text);
        if (ResDt.Rows.Count > 0)
        {
            //返工后重复过站
            if (ResDt.Rows[0]["rework"].ToString() == "Y")
            {
                DataTable dtCheckInIsOK = CRUD.CheckInIsOK(MouldLot, lblWorksiteID.Text);
                if (dtCheckInIsOK.Rows.Count > 0)
                {
                    workshop = dtCheckInIsOK.Rows[0]["workshopid"].ToString();
                    eqp = dtCheckInIsOK.Rows[0]["eqpid"].ToString();
                }
                else
                {
                    JScript.Alert("该批次尚未进站！", this);
                    return;
                }
            }
            else
            {
                eqp = ResDt.Rows[0]["eqpid"].ToString();
                workshop = ResDt.Rows[0]["workshopid"].ToString();
            }
        }
        else
        {
            JScript.Alert("批次尚未进站", this);
            return;
        }

        setddl(workshop, eqp);
        //批次已过站点
        string WorksiteIDOfLot = CRUD.GetWorksite(MouldLot);

        //查询批次流程
        CRUD.setLabelProcess(lblLotprocess, lblCurrnentWorksite, lblEndProcess,MouldLot, lblWorksiteID.Text);

        ddlWorksite.ClearSelection();
        ddlWorksite.Items.FindByValue("M25").Selected = true;
    }

    private void setddl(string workshop, string eqp)
    {
        CRUD.Setdll(ddlWorkshop, "workshop");
        //ddlWorkshop.SelectedIndex = 0;
        ddlWorkshop.ClearSelection();
        ddlWorkshop.Items.FindByValue(workshop).Selected = true;
        ddlEqp.ClearSelection();
        CRUD.SetEqpDDL(ddlEqp, ddlWorkshop.SelectedValue, lblWorksiteID.Text);
        ddlEqp.Items.FindByValue(eqp).Selected = true;

    }

    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        //string a = ddlWorkshop.SelectedValue;
        if (txtLot.Text == "" || lblLotprocess.Text =="")
        {
            JScript.Alert("请刷入批次号", this);
            return;
        }
        #region 防止重复出站 add by lei.xue on 2017-8-29
        //模具编号
        string MouldLot = "";
        #region 判断条码是否有效
        if (txtLot.Text.Length > 3)
        {
            //外发或资产条码是否打印 条码第四位判断：W或Z
            if (txtLot.Text.Substring(3, 1).ToString() == "W")
            {
                if (CRUD.CheckMouldLabel("W", txtLot.Text) == "fail")
                {
                    JScript.Alert("该条码为打印！", this);
                    return;
                }
            }
            else if (txtLot.Text.Substring(3, 1).ToString() == "Z")
            {
                if (CRUD.CheckMouldLabel("Z", txtLot.Text) == "fail")
                {
                    JScript.Alert("该条码未打印！", this);
                    return;
                }
            }

            MouldLot = txtLot.Text.Substring(0, 3).ToString();
        }
        else
        {
            MouldLot = txtLot.Text;
        }
        #endregion
        //查询是否可以过站
        string result1 = CRUD.QueryStationOfLot(lblWorksiteID.Text, MouldLot);
        if (result1 != "success")
        {
            JScript.Alert(result1, this);
            txtLot.Text = "";
            return;
        }
        #endregion


        //检查是否是标签条码
        string strLabel = "";
        if (txtLot.Text.Length > 3)
        {
            strLabel = txtLot.Text;
        }

        string result = "";
        string CountResult = "";
        if (cbxReworkID.Checked == false) 
        {  
            //result = WIP.WIPCheckOut(txtLot.Text.Substring(0, 3).ToString(), txtLot.Text, ddlEqp.SelectedValue
            //                        , ddlWorkshop.SelectedValue, ddlWorksite.SelectedValue, System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString());
            result=MouldComplete.MouldCompleteCheckOut(txtLot.Text.Substring(0, 3).ToString(), strLabel, ddlEqp.SelectedValue
                                                       , ddlWorkshop.SelectedValue, lblWorksiteID.Text, System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),
                                                       "Y","N");
            CountResult = CRUD.UpdateMouldLotCount(txtLot.Text.Substring(0, 3).ToString());
            if (result == "success" && CountResult=="success")
            {
                //打印标签调用前台方法
                //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>PrintLabel();</script>");
                JScript.Alert("成型出站成功！", this);
                ClearInfo();
                return;
            }
            else
            {
                JScript.Alert("成型出站失败！", this);
                return;
            }
        }
        else
        {
            //返工站点的流程编号
            string Flowidno="";
            Flowidno = CRUD.GetFlowidno(txtLot.Text.Substring(0, 3).ToString(), ddlWorksite.SelectedValue);
            result = MouldComplete.MouldCompleteCheckOut(txtLot.Text.Substring(0, 3).ToString(), strLabel, ddlEqp.SelectedValue
                                                          , ddlWorkshop.SelectedValue, ddlWorksite.SelectedValue, System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),
                                                          "N","Y");

            //记录返工记录
            CRUD.rework(lblWorksiteID.Text, ddlWorksite.SelectedValue, txtLot.Text.Substring(0, 3).ToString(),
                         System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(), Flowidno);

            if (result == "success")
            {
                JScript.Alert("成型出站成功！", this);
                ClearInfo();
                return;
            }
            else
            {
                JScript.Alert("成型出站失败！", this);
                return;
            }
        }

    }
    /// <summary>
    /// 清除页面信息
    /// </summary>
    private void ClearInfo()
    {
        txtLot.Text = "";
        txtLabelInfo.Text = "";
        lblLotprocess.Text = "";
        lblCurrnentWorksite.Text = "";
        lblEndProcess.Text = "";
    }

}