﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Common;

public partial class Report_OutputReport : System.Web.UI.Page
{
    int sumOutputQty = 0;
    int sumAGradeQty = 0;
    int sumBGradeQty = 0;
    int sumSGradeQty = 0;
    int sumHoldQty = 0;
    int sumScrapQty = 0;
    int sumInputQty = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //绑定查询选项内容
            OutputReport.setWorksiteDDL(ddlWorksite);
            //工单类型
            OutputReport.setWOTypeddl(ddlWOType);
            //工单厚度
            OutputReport.setWOddl(ddlThinkness, "mouldthinkness");
            //工单宽度
            OutputReport.setWOddl(ddlWidth, "mouldwidth");
            //工单产品类型
            OutputReport.setWOddl(ddlWOProducttype, "mouldtype");
            //班次
            OutputReport.setClassDDL(ddlClass);
            BindTimeByClass();
        }
    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        //databind(txtBt.Text, txtEt.Text);
        string bt = "", et = "";
        if (ddlClass.SelectedValue == "ALL")
        {
            bt = txtBt.Text + " 08:30:00";
            et = Convert.ToDateTime(txtEt.Text).AddDays(1).ToString("yyyy-MM-dd") + " 08:30:00";   
        }
        else if (ddlClass.SelectedValue == "Day")
        {
            bt = txtBt.Text + " 08:30:00";
            et = txtEt.Text + " 20:30:00";
        }
        else if (ddlClass.SelectedValue == "Night")
        {
            bt = txtBt.Text + " 20:30:00";
            et = Convert.ToDateTime(txtEt.Text).AddDays(1).ToString("yyyy-MM-dd") + " 08:30:00";  
        }
        databind(bt, et);
    }
    private void databind(string bt, string et)
    {
        DataTable dt;
        //查询检验站点
        if (ddlWorksite.SelectedItem.Text == "AG涂布检验"
          || ddlWorksite.SelectedItem.Text == "UV背涂检验"
          || ddlWorksite.SelectedItem.Text == "贴膜检验"
          || ddlWorksite.SelectedItem.Text == "UV成型检验")
        {
            grd.Visible = false;
            grdQC.Visible = true;
            if (ddlMethod.SelectedValue == "面积")
            {
                dt = OutputReport.QueryData(bt, et, ddlWOType.SelectedValue, ddlWOProducttype.SelectedValue, ddlWidth.SelectedValue, ddlThinkness.SelectedValue, ddlWorksite.SelectedItem.Value, ddlClass.SelectedValue);
                grdQC.DataSource = dt;
                grdQC.DataBind();
            }
            else if (ddlMethod.SelectedValue == "卷数")
            {
                dt = OutputReport.QueryDataByQty(bt, et, ddlWOType.SelectedValue, ddlWOProducttype.SelectedValue, ddlWidth.SelectedValue, ddlThinkness.SelectedValue, ddlWorksite.SelectedItem.Value, ddlClass.SelectedValue);
                grdQC.DataSource = dt;
                grdQC.DataBind();
            }

        }
        //制造站点
        else
        {
            grd.Visible = true;
            grdQC.Visible = false;
            if (ddlWorksite.SelectedValue != "M60")
            {
                grd.Columns[7].Visible = false;
                grd.Columns[8].Visible = false;
                grd.Columns[9].Visible = false;
                grd.Columns[10].Visible = false;
                grd.Columns[11].Visible = false;
            }
            else
            {
                grd.Columns[7].Visible = true;
                grd.Columns[8].Visible = true;
                grd.Columns[9].Visible = true;
                grd.Columns[10].Visible = true;
                grd.Columns[11].Visible = true;
            }
            if (ddlMethod.SelectedValue == "面积")
            {
                dt = OutputReport.QueryData(bt, et, ddlWOType.SelectedValue, ddlWOProducttype.SelectedValue, ddlWidth.SelectedValue, ddlThinkness.SelectedValue, ddlWorksite.SelectedItem.Value, ddlClass.SelectedValue);
                grd.DataSource = dt;
                grd.DataBind();
            }
            else if (ddlMethod.SelectedValue == "卷数")
            {
                dt = OutputReport.QueryDataByQty(bt, et, ddlWOType.SelectedValue, ddlWOProducttype.SelectedValue, ddlWidth.SelectedValue, ddlThinkness.SelectedValue, ddlWorksite.SelectedItem.Value, ddlClass.SelectedValue);
                grd.DataSource = dt;
                grd.DataBind();
            }
            
        }
    }
    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DataRowView myrows = (DataRowView)e.Row.DataItem;
            sumOutputQty += Convert.ToInt32(e.Row.Cells[6].Text);
            sumInputQty += Convert.ToInt32(e.Row.Cells[3].Text);
            //modify by lei.xue on 2017-8-7 原始宽幅、原始长度、投入数量
            if (ddlWorksite.SelectedItem.Text == "包装")
            {
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "";
                //加了5个字段7改为12
                e.Row.Cells[12].Text = "";

                //统计各等级总数
                
                sumAGradeQty += Convert.ToInt32(e.Row.Cells[7].Text);
                sumBGradeQty += Convert.ToInt32(e.Row.Cells[8].Text);
                sumSGradeQty += Convert.ToInt32(e.Row.Cells[9].Text);
                sumHoldQty += Convert.ToInt32(e.Row.Cells[10].Text);
                sumScrapQty += Convert.ToInt32(e.Row.Cells[11].Text);
            }
            else
            {
                //包装站点以外不显示每个等级的数量
                e.Row.Cells[7].Text = "";
                e.Row.Cells[8].Text = "";
                e.Row.Cells[9].Text = "";
                e.Row.Cells[10].Text = "";
                e.Row.Cells[11].Text = "";
            }
        }
        // 合计
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total";
            e.Row.Cells[1].Text = "/";
            e.Row.Cells[2].Text = "/";
            e.Row.Cells[4].Text = "/";
            e.Row.Cells[5].Text = "/";

            
            e.Row.Cells[6].Text = sumOutputQty.ToString();
            //包装投入数量统计和产出不显示 modify by lei.xue on 2017-8-7
            if (ddlWorksite.SelectedItem.Text == "包装")
            {
                //加了5个字段7改为12
                e.Row.Cells[12].Text = "";
                e.Row.Cells[3].Text = "";

                //包装显示各等级数量
                e.Row.Cells[7].Text = sumAGradeQty.ToString();
                e.Row.Cells[8].Text = sumBGradeQty.ToString();
                e.Row.Cells[9].Text = sumSGradeQty.ToString();
                e.Row.Cells[10].Text = sumHoldQty.ToString();
                e.Row.Cells[11].Text = sumScrapQty.ToString();
            }
            else
            {
                //产出率
                //加了5个字段7改为12
                e.Row.Cells[12].Text = (Math.Round(Convert.ToDouble(sumOutputQty) / sumInputQty, 2) * 100).ToString() + "%";
                e.Row.Cells[3].Text = sumInputQty.ToString();

                e.Row.Cells[7].Text = "";
                e.Row.Cells[8].Text = "";
                e.Row.Cells[9].Text = "";
                e.Row.Cells[10].Text = "";
                e.Row.Cells[11].Text = "";
            }
        }
    }

    protected void grdQC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DataRowView myrows = (DataRowView)e.Row.DataItem;
            sumOutputQty += Convert.ToInt32(e.Row.Cells[3].Text);
            sumAGradeQty += Convert.ToInt32(e.Row.Cells[4].Text);
            sumBGradeQty += Convert.ToInt32(e.Row.Cells[5].Text);
            sumSGradeQty += Convert.ToInt32(e.Row.Cells[6].Text);
            sumHoldQty += Convert.ToInt32(e.Row.Cells[7].Text);
            sumScrapQty += Convert.ToInt32(e.Row.Cells[8].Text);

        }
        // 合计
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total";
            e.Row.Cells[1].Text = "/";
            e.Row.Cells[2].Text = "/";

            e.Row.Cells[3].Text = sumOutputQty.ToString();
            e.Row.Cells[4].Text = sumAGradeQty.ToString();
            e.Row.Cells[5].Text = sumBGradeQty.ToString();
            e.Row.Cells[6].Text = sumSGradeQty.ToString();
            e.Row.Cells[7].Text = sumHoldQty.ToString();
            e.Row.Cells[8].Text = sumScrapQty.ToString();
            if ((sumAGradeQty + sumBGradeQty) == 0)
            {
                e.Row.Cells[9].Text = "0";
            }
            else
            {
                e.Row.Cells[9].Text = (Math.Round(Convert.ToDouble(((double)sumAGradeQty + (double)sumBGradeQty) / sumOutputQty), 2) * 100).ToString() + "%";
            }

        }
    }
    protected void ddlClass_TextChanged(object sender, EventArgs e)
    {
        BindTimeByClass();


    }

    private void BindTimeByClass()
    {
        //取消原来的班次时间逻辑 modify by lei.xue on 2017-6-22
        //if (ddlClass.SelectedValue == "ALL")
        //{
        //    txtBt.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00";
        //    txtEt.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 08:30:00";
        //}
        //else if (ddlClass.SelectedValue == "Day")
        //{
        //    txtBt.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00";
        //    txtEt.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 20:30:00";
        //}
        //else if (ddlClass.SelectedValue == "Night")
        //{
        //    txtBt.Text = DateTime.Now.ToString("yyyy-MM-dd") + " 20:30:00";
        //    txtEt.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 08:30:00";
        //}

        if (ddlClass.SelectedValue == "ALL")
        {
            txtBt.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtEt.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        }
        else if (ddlClass.SelectedValue == "Day")
        {
            txtBt.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtEt.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        }
        else if (ddlClass.SelectedValue == "Night")
        {
            txtBt.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            txtEt.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
        }
    }
    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    if (ddlClass.SelectedValue == "ALL")
    //    {

    //        databind(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 08:30:00"
    //               , DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00");
    //    }
    //    else if (ddlClass.SelectedValue == "Day")
    //    {
    //        databind(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 08:30:00"
    //               , DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:30:00");
    //    }
    //    else if (ddlClass.SelectedValue == "Night")
    //    {
    //        databind(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 20:30:00"
    //               , DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00");
    //    }
    //}
}