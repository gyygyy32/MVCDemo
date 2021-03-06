﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Pub;

public partial class Mould_CarveCheckOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonClass.isAllow(User.Identity.Name, this, "喷砂出站");
            //lblWorksiteID.Text = "M05";
            txtLot.Attributes.Add("onkeypress", "EnterTextBox('btnEnter')");
            //CRUD.SetCBL(cblDCMaterial, "DCMaterial");
            //setddl();

        }

    }
    protected void btnEnter_Click(object sender, EventArgs e)
    {
        //查询是否可以过站
        string result = CRUD.QueryStationOfLot(lblWorksiteID.Text, txtLot.Text);
        if (result != "success")
        {
            JScript.Alert(result, this);
            txtLot.Text = "";
            return;
        }
        //是否进站
        //result = CRUD.CheckInIsOK(txtLot.Text, lblWorksiteID.Text);
        //if (result != "success")
        //{
        //    JScript.Alert("批次尚未进站！", this);
        //    txtLot.Text = "";
        //    return;
        //}       
        string MouldLot = txtLot.Text;
        string workshop="";
        string eqp="";
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
        //绑定checkin时的信息
        //ddlWorkshop.ClearSelection();
        //ddlWorkshop.Items.FindByValue(workshop).Selected = true;
        //ddlEqp.ClearSelection();
        //ddlEqp.Items.FindByValue(eqp).Selected = true;
        //txtWorkshop.Text = workshop;
        //txtEqp.Text = eqp;
        //锁定机台和车间选项
        //ddlEqp
        setddl(workshop, eqp);

        //查询批次流程
        CRUD.setLabelProcess(lblLotprocess, lblCurrnentWorksite, lblEndProcess, txtLot.Text, lblWorksiteID.Text);
        //生成模具编号：规则：模具编号+类型[Z]+年月日—001Z170118
        DataTable dt = CRUD.GetWorkflow(txtLot.Text);
        string type = dt.Rows[0]["flowid"].ToString();
        if (type == "flow001" || type == "flow002")
        {
            txtLabelInfo.Text = txtLot.Text + "Z" + System.DateTime.Now.ToString("yyMMdd");
        }
        else if (type == "flow003")
        {
            txtLabelInfo.Text = txtLot.Text + "W" + System.DateTime.Now.ToString("yyMMdd");
        }
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
        if (txtLot.Text == "")
        {
            JScript.Alert("请刷入批次号", this);
            return;
        }
        //========防止重复出站 add by lei.xue on 2017-8-29=======================
        //查询是否可以过站
        string result1 = CRUD.QueryStationOfLot(lblWorksiteID.Text, txtLot.Text);
        if (result1 != "success")
        {
            JScript.Alert(result1, this);
            txtLot.Text = "";
            return;
        }
        string result = "";
        
            result = Grit.GritCheckOut(txtLot.Text, txtLabelInfo.Text, ddlEqp.SelectedValue, ddlWorkshop.Text, lblWorksiteID.Text
                                    , System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),txtHaze.Text);
            if (result == "success")
            {
                //打印标签调用前台方法
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>PrintLabel();</script>");
                JScript.Alert("喷砂出站成功！", this);
                ClearInfo();
                return;
            }
            else
            {
                JScript.Alert("喷砂出站失败！", this);
                return;
            }
        
        

    }
    /// <summary>
    /// 清除页面信息
    /// </summary>
    private void ClearInfo()
    {
        txtLot.Text = "";
        //txtLabelInfo.Text = "";
        lblLotprocess.Text = "";
        lblLotprocess.Text = "";
        lblCurrnentWorksite.Text = "";
        lblEndProcess.Text = "";
    }
}