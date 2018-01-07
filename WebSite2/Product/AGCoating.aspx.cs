﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;
using Pub;

public partial class Product_AGCoating : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonClass.isAllow(User.Identity.Name, this, "AG涂布");
            txtLot.Attributes.Add("onkeypress", "EnterTextBox('btnEnter')");
            //AG涂布需要分批 modify by lei.xue on 2017-4-27=====================================================
            txtSplitLength.Attributes.Add("onkeypress", "EnterTextBoxSplit('btnSplitEnter')");
            txtPreWidth.Attributes.Add("readonly", "true");
            txtPreLength.Attributes.Add("readonly", "true");
            txtSplitLength.Attributes.Add("readonly", "true");
            txtRestLength.Attributes.Add("readonly", "true");
            
        }
    }

    private void setddl(string workshop)
    {
        CRUD.Setdll(ddlWorkshop, "workshop");
        CRUD.Setdll(ddlGlueType, "AGCoatingGlueType");
        //ddlWorkshop.SelectedIndex = 0;
        ddlWorkshop.ClearSelection();
        ddlWorkshop.Items.FindByValue(workshop).Selected = true;
        ddlEqp.ClearSelection();
        CRUD.SetEqpDDL(ddlEqp, ddlWorkshop.SelectedValue, lblWorksiteID.Text);
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

        DataTable lotDt = CRUD.GetLotBasisInfo(txtLot.Text);
        ViewState["lotDt"] = lotDt;
        string workshop = lotDt.Rows[0]["workshopID"].ToString();
        setddl(workshop);

        string WO = lotDt.Rows[0]["workorder"].ToString();
        ViewState["WO"] = WO;
        #region 批次流程

 
        //查询批次流程
        CRUD.setLabelProcess(lblLotprocess, lblCurrnentWorksite, lblEndProcess, txtLot.Text, lblWorksiteID.Text);
        #endregion
        //FilmCRUD.GetPreLengthAndWidth(txtPreLength, txtPreWidth, txtLot.Text, lblWorksiteID.Text, WO, "");
        //txtRestLength.Text = lotDt.Rows[0]["restlength"].ToString();
        //txtPreLength.Text = lotDt.Rows[0]["mouldlength"].ToString();
        //txtPreWidth.Text = lotDt.Rows[0]["mouldwidth"].ToString();
        //=======前站宽幅长度和剩余长度取有效值 modify by lei.xue on 2017-6-1================
        txtRestLength.Text = lotDt.Rows[0]["restlength"].ToString();
        txtPreLength.Text = lotDt.Rows[0]["validlength"].ToString();
        txtPreWidth.Text = lotDt.Rows[0]["validwidth"].ToString();
        //==========标签增加产品类型 add by lei.xue on 2017-5-20================
        txtProductType.Text = lotDt.Rows[0]["producttype"].ToString();
        LabelInfo();
    }
    private void LabelInfo()
    {
        //查询UV成型分批子批序列号最大数量 modify by lei.xue on 2017-3-27=========================================
        string strResult = AGCoating.QueryMaxAGCoatingLotID(txtLot.Text);
        int intQty = 0;
        if (strResult != "fail")
        {
            intQty = Convert.ToInt32(strResult) + 1;
        }
        else
        {
            JScript.Alert("查询分批数量出错", this);
            return;
        }

        //string Qty = (Convert.ToInt32(UVComplete.QueryMaxUVCompleteLotID()) + 1).ToString();
        string Qty = intQty.ToString();
        txtLabelInfo.Text = txtLot.Text+ "-" + Convert.ToInt32(Qty).ToString("00");
    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        #region//插入basis表 ， 更新母批剩余数量 ，子批flowidno为当点站===========================================
        
        //==============不分批子批长度取母批的长度 modify by lei.xue on 2017-5-28==================================
        string SplitLength = "";
        if (cbxSplit.Checked == true)
        {
            SplitLength = txtSplitLength.Text;
        }
        else
        {
            //母批直接分批的时候改为分批长度取剩余长度 modify by lei.xue on 2017-8-19
            //SplitLength = txtPreLength.Text;
            SplitLength = txtRestLength.Text;
        }

        LotBasisDatalist dl = new LotBasisDatalist();
        DataTable dt = (DataTable)ViewState["lotDt"];
        dl.flowid = dt.Rows[0]["flowid"].ToString();
        dl.workshopid = ddlWorkshop.SelectedValue;
        dl.lottype = "Film";
        dl.status = "Active";
        dl.createuser = System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString();
        dl.currentflowidno = (Convert.ToInt32(dt.Rows[0]["flowidno"].ToString()) + 1).ToString(); //流程编号加1
        dl.factoryid = "";
        dl.workorder = ViewState["WO"].ToString();
        dl.reworkorder = "";
        dl.lotid = txtLabelInfo.Text;
        dl.lotcount = dt.Rows[0]["lotcount"].ToString();
        dl.ProcessComplete = "N";
        //dl.UVCompleteLotid = txtLot.Text;
        dl.length = SplitLength;//txtSplitLength.Text;
        //================剩余长度和宽度为母批的有效长度和宽度modify by lei.xue on 2017-6-1=============
        dl.restlength = txtValidLength.Text;//SplitLength;//txtSplitLength.Text;
        dl.width = txtPreWidth.Text;
        dl.restwidth = txtValidWidth.Text;//txtPreWidth.Text;
        dl.Filmlevel = "A";
        
        dl.eqpid = ddlEqp.SelectedValue;
        //增加有效幅宽 add by lei.xue on 2017-4-18=================================================
        dl.validwidth = txtValidWidth.Text;
        //================增加有效长度 add by lei.xue on 2017-5-28=================================
        dl.validlength = txtValidLength.Text;
        string result = CreateMouldLot.InsertLot(dl);
        if (result != "success")
        {
            JScript.Alert(result, this);
            return;
        }
        #endregion
        #region//插入分批记录到分批表===============================================================
        dl.sublotid = txtLabelInfo.Text;
        dl.lotid = txtLot.Text;
        string resultsplit = PasteFilm.InsertSplitLot(dl);
        #endregion

        #region//更新母批剩余数量===================================================================
        Decimal rest = Convert.ToDecimal(txtRestLength.Text) - Convert.ToDecimal(SplitLength);//txtSplitLength.Text);
        string strRest = PasteFilm.UpdateParentQty(txtLot.Text, rest.ToString(), "Length");
        if (strRest != "success")
        {
            JScript.Alert("更新母批剩余数量失败！", this);
            return;
        }
        #endregion
        string Checkoutresult = AGCoating.FilmCheckOut(txtLabelInfo.Text,
                              "",
                              ddlEqp.SelectedValue,
                              ddlWorkshop.SelectedValue,
                              lblWorksiteID.Text,
                              System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),
                              ddlGlueType.SelectedValue
                              );

        if (Checkoutresult == "success")
        {
            //JScript.AlertAndRedirect("AG涂布过站成功！", "AGCoating.aspx", this);
            //ClearInfo();
            //打印标签调用前台方法
            //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>PrintLabel();</script>");
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>PrintLabel('" + txtLabelInfo.Text + "','../Product/AGCoating.aspx','" + txtProductType.Text + "');</script>");
        }
        else
        {
            JScript.Alert("AG涂布过站失败！", this);
            return;
        }
    }
    private void ClearInfo()
    {
        
        txtPreWidth.Text = "";
        txtPreLength.Text = "";
    }




    protected void btnSplitEnter_Click(object sender, EventArgs e)
    {
        //LabelInfo();
    }
}