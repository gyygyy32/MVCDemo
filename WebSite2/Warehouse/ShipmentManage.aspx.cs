﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Pub;

public partial class Warehouse_InventoryManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonClass.isAllow(User.Identity.Name, this, "预出库查询");
            setddl();
            //Databind();
        }
    }
    private void setddl()
    {
        CRUD.Setdll(ddlWorkshop, "workshop");
        //CRUD.Setdll(ddlWorksiteID, "worksite");

        ddlWorkshop.SelectedIndex = 0;
        
        //ddlWorksiteID.SelectedIndex = 0;
        //CRUD.SetEqpDDL(ddlEqp, ddlWorkshop.SelectedValue, ddlWorksiteID.SelectedValue);

    }
   
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        Databind();
    }
    private void Databind()
    {
        
        DataTable dt = ShipmentManage.QueryData(ddlWorkshop.SelectedValue,
                                                txtShipmentID.Text,
                                                txtBegintime.Text,
                                                txtEndtime.Text,
                                                txtLotid.Text,
                                                txtPinmin.Text,
                                                txtType.Text
                                                );
        grd.DataSource = dt;
        grd.DataBind();
        //Nmtree.MergeGridViewCell.MergeRow(grd,0,7);
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //当鼠标停留时更改背景色

            e.Row.Cells[1].Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#8EC26F'");
            //当鼠标移开时还原背景色
            e.Row.Cells[1].Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            //设置悬浮鼠标指针形状为"小手"
            e.Row.Cells[1].Attributes["style"] = "Cursor:hand";

            //单击/双击 事件
            e.Row.Cells[1].Attributes.Add("OnClick", "ClickEvent('" + e.Row.Cells[8].FindControl("btnDetail").ClientID + "');selectx(this)");
            //e.Row.Attributes.Add("OnClick", "ClickEvent('" + e.Row.Cells[5].FindControl("btnDetial").ClientID + "');selectx(this)");
            //注：OnClick参数是指明为鼠标单击时间，后个是调用javascript的ClickEvent函数
        }
    }
    protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Detail")
        {
            //在这里对你需要的数据信息进行输出
            //我的处理函数
            // Response.Write("<script>alert('"+ e.CommandArgument  +"')</script>");
            string id = e.CommandArgument.ToString();

            JScript.JavaScriptLocationHref("../Warehouse/EditShipment.aspx?id=" + id, this);
        }
    }
}