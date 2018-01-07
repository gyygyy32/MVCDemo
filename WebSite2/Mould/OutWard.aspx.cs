﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Pub;

public partial class Mould_OutWard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonClass.isAllow(User.Identity.Name, this, "外发");
            //lblWorksiteID.Text = "M05";
            txtLot.Attributes.Add("onkeypress", "EnterTextBox('btnEnter')");
            //CRUD.SetCBL(cblDCMaterial, "DCMaterial");

            setddl();

        }

    }
    protected void btnEnter_Click(object sender, EventArgs e)
    {
        //查询是否可以过站
        string result = CRUD.QueryStationOfLot(lblWorksiteID.Text, txtLot.Text);
        if (result != "success")
        {
            if (result == "该批次已经结束流程！")
            {
                JScript.Alert("批次流程已经结束，请复位后重新过站！", this);
                return;
            }
            else
            {
                JScript.Alert(result, this);
                txtLot.Text = "";
                return;
            }
        }
       
        



        //查询批次流程
        CRUD.setLabelProcess(lblLotprocess, lblCurrnentWorksite, lblEndProcess, txtLot.Text, lblWorksiteID.Text);
        //查询车间 、checkin的机台
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

    private void setddl()
    {
        CRUD.Setdll(ddlWorkshop, "workshop");
        CRUD.Setdll(ddlMouldPitch, "MouldPitch");
        CRUD.Setdll(ddlMouldStructure, "MouldStructure");

        ddlWorkshop.SelectedIndex = 0;
        CRUD.SetEqpDDL(ddlEqp, ddlWorkshop.SelectedValue, lblWorksiteID.Text);

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
        string countresult = "";
           result = OutWard.OutWardCheckOut(txtLot.Text,
                                           txtLabelInfo.Text,
                                           ddlEqp.SelectedValue,
                                           ddlWorkshop.SelectedValue,
                                           lblWorksiteID.Text,
                                           System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),
                                           txtDKWidth.Text,
                                           ddlMouldStructure.SelectedValue,
                                           ddlMouldPitch.SelectedValue
                                           );
           //countresult = CRUD.UpdateMouldLotCount(txtLot.Text);                
            if (result == "success" )
            {
                //打印标签调用前台方法
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>PrintLabel();</script>");
                //JScript.Alert("外发出站成功！", this);
                //ClearInfo();
                return;
            }
            else
            {
                JScript.Alert("外发出站失败！", this);
                return;
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
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string result = CRUD.RestartMouldLotProcess(txtLot.Text);
        if (result == "success")
        {
            JScript.Alert("复位成功，模具可以继续过站", this);
            return;
        }
        else
        {
            JScript.Alert("复位失败！", this);
            return;
        }
    }
}