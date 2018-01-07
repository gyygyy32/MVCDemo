﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Pub;

public partial class Product_SubsectionCheck : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonClass.isAllow(User.Identity.Name, this, "分条检验");
            txtLot.Attributes.Add("onkeypress", "EnterTextBox('btnEnter')");
        }
    }
    private void setddl(string workshop, string eqp)
    {
        //膜的等级
        CRUD.Setdll(ddlFilmLevel, "FilmLevel");
        CRUD.Setdll(ddlWorkshop, "workshop");
        //ddlWorkshop.SelectedIndex = 0;
        ddlWorkshop.ClearSelection();
        ddlWorkshop.Items.FindByValue(workshop).Selected = true;
        ddlEqp.ClearSelection();
        CRUD.SetEqpDDL(ddlEqp, ddlWorkshop.SelectedValue, "M55");//获取贴膜成型的eqp
        ddlEqp.Items.FindByValue(eqp).Selected = true;

        #region //检验结果======================================================
        FilmCRUD.setDDLQCResult(ddlAppearanceResult);

        #endregion
    }
    protected void btnEnter_Click(object sender, EventArgs e)
    {
        //判断是否是UV成型的条码
        //string check = CRUD.CheckFilmLabel("UV成型", txtLot.Text);
        //if (check == "fail")
        //{
        //    JScript.Alert("条码未打印", this);
        //    return;
        //}
        string originalLot = txtLot.Text;
        ViewState["originalLot"] = originalLot;

        //查询是否可以过站
        string result = CRUD.QueryStationOfLot(lblWorksiteID.Text, originalLot);
        if (result != "success")
        {
            JScript.Alert(result, this);
            txtLot.Text = "";
            return;
        }

        DataTable lotDt = CRUD.GetLotBasisInfo(originalLot);
        //string workshop = lotDt.Rows[0]["workshopID"].ToString();
        //膜等级
        txtFilmLevel.Text = lotDt.Rows[0]["filmlevel"].ToString();
        ////查询UV成型站的eqp信息 add by lei.xue on 2017-2-21
        //lotDt = null;
        //lotDt = CRUD.getStationInfo(originalLot, "M50");

        string UVCompleteEqp = lotDt.Rows[0]["eqpid"].ToString();
        string workshop = lotDt.Rows[0]["workshopid"].ToString();
        setddl(workshop, UVCompleteEqp);

        //string WO = lotDt.Rows[0]["workorder"].ToString();
        #region 批次流程

        //查询批次流程
        CRUD.setLabelProcess(lblLotprocess, lblCurrnentWorksite, lblEndProcess, originalLot, lblWorksiteID.Text);
        #endregion
    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        //string result = CRUD.FilmCheckOut(ViewState["originalLot"].ToString(), txtLot.Text, ddlEqp.SelectedValue, ddlWorkshop.SelectedValue,
        //                    lblWorksiteID.Text,
        //                    System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),
        //                    ddlFilmLevel.SelectedValue);
        //string resultLevel = "";
        //if (cbxUpdateLevel.Checked == true)
        //{
        //    resultLevel = CRUD.updateFilmLevel(txtFilmLevel.Text,
        //                                              ddlFilmLevel.SelectedValue,
        //                                              ViewState["originalLot"].ToString(),
        //                                              System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),
        //                                              lblWorksiteID.Text
        //                                              );
        //}
        string strCheck = "";//存放检验结果变量
        string strFilmLevel = "";//模具等级变量
        string result = CRUD.FilmCheckOut(ViewState["originalLot"].ToString(), txtLot.Text, ddlEqp.SelectedValue, ddlWorkshop.SelectedValue,
                            lblWorksiteID.Text,
                            System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),
                            ddlFilmLevel.SelectedValue);
        string resultLevel = "";
        if (cbxUpdateLevel.Checked == true)//膜检验结果为NG
        {
            strCheck = "NG";
            strFilmLevel = ddlFilmLevel.SelectedValue;
            resultLevel = CRUD.updateFilmLevel(txtFilmLevel.Text,
                                                      ddlFilmLevel.SelectedValue,
                                                      ViewState["originalLot"].ToString(),
                                                      System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),
                                                      lblWorksiteID.Text
                                                      );
        }
        else
        {
            strCheck = "OK";
            strFilmLevel = txtFilmLevel.Text;
        }
        #region//检验项目=====================================================================================
        AGCoatingQCDatalist Datalist = new AGCoatingQCDatalist();

        //外观
        //1外观左
        Datalist.appearanceleft = txtAppearanceLeft.Text;
        //2外观右
        Datalist.appearanceright = txtAppearanceRight.Text;
        Datalist.appearanceresult = ddlAppearanceResult.SelectedValue;


        string QCResult = UVBackCheck.CheckInfo(txtLot.Text,
                                          ddlEqp.SelectedValue,
                                          lblWorksiteID.Text,
                                          strCheck,
                                          System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString(),
                                          strFilmLevel,
                                          Datalist
                                          );
        if (QCResult == "fail")
        {
            JScript.Alert("记录检验项目失败", this);
            return;
        }
        #endregion
        if (result == "success" && resultLevel != "fail")
        {
            //JScript.Alert("UV成型检验过站成功！", this);
            JScript.AlertAndRedirect("分条检验过站成功！", "SubsectionCheck.aspx", this);
            //ClearInfo();
            return;
        }
        else
        {
            JScript.Alert("分条检验过站失败！", this);
            return;
        }
    }

    private void ClearInfo()
    {
        txtLot.Text = "";
        txtReason.Text = "";

    }
}