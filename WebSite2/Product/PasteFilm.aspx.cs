﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Pub;
public partial class Product_PasteFilm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CommonClass.isAllow(User.Identity.Name, this, "贴膜");
            txtLot.Attributes.Add("onkeypress", "EnterTextBox('btnEnter')");
            txtSplitLength.Attributes.Add("onkeypress", "EnterTextBoxSplit('btnSplitEnter')");
            txtPreWidth.Attributes.Add("readonly", "true");
            txtPreLength.Attributes.Add("readonly", "true");
            txtRestLength.Attributes.Add("readonly", "true");
            //贴膜标签改为可以手输 modify by lei.xue on 2017-11-27
            //txtLabelInfo.Attributes.Add("readonly", "true");
            txtSplitLength.Attributes.Add("readonly", "true");
        }
    }

    private void setddl(string workshop)
    {
        CRUD.Setdll(ddlWorkshop, "workshop");
        //ddlWorkshop.SelectedIndex = 0;
        ddlWorkshop.ClearSelection();
        ddlWorkshop.Items.FindByValue(workshop).Selected = true;
        ddlEqp.ClearSelection();
        CRUD.SetEqpDDL(ddlEqp, ddlWorkshop.SelectedValue, lblWorksiteID.Text);
    }

    protected void btnEnter_Click(object sender, EventArgs e)
    {
        //UV成型改为分批 注释检查是否打印代码 modify by lei.xue on 2017-3-27======================================
        //判断是否是UV成型的条码
        //string check = CRUD.CheckFilmLabel("UV成型", txtLot.Text);
        //if (check == "fail")
        //{
        //    JScript.Alert("条码未打印", this);
        //    return;
        //}
        string originalLot = txtLot.Text;//check
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
        string workshop = lotDt.Rows[0]["workshopID"].ToString();
        //保存批次号lotbasis信息 modify by lei.xue on 2017-2-20
        ViewState["lotDt"] = lotDt;
      
        setddl(workshop);

        string WO = lotDt.Rows[0]["workorder"].ToString();
        ViewState["WO"] = WO;
        #region 批次流程

        //查询批次流程
        CRUD.setLabelProcess(lblLotprocess, lblCurrnentWorksite, lblEndProcess, originalLot, lblWorksiteID.Text);

        #endregion

        ////查询工单中的长、宽
        //dt.Clear();
        //dt = CreateLot.QueryWorkorderIno(WO);
        //txtPreLength.Text = dt.Rows[0]["mouldlength"].ToString();
        //txtPreWidth.Text = dt.Rows[0]["mouldwidth"].ToString();
        //查询前站的长宽
        //批次已过站点

        //查询母批的长度宽度及其剩余量
        //FilmCRUD.GetPreLengthAndWidth(txtPreLength, txtPreWidth, originalLot, lblWorksiteID.Text, WO,"M45");
        //txtRestLength.Text = lotDt.Rows[0]["restlength"].ToString();
        //txtPreLength.Text = lotDt.Rows[0]["mouldlength"].ToString();
        //txtPreWidth.Text = lotDt.Rows[0]["mouldwidth"].ToString();
        //=======前站宽幅长度和剩余长度取有效值 modify by lei.xue on 2017-6-1================
        txtRestLength.Text = lotDt.Rows[0]["restlength"].ToString();
        txtPreLength.Text = lotDt.Rows[0]["validlength"].ToString();
        txtPreWidth.Text = lotDt.Rows[0]["validwidth"].ToString();
        txtProductType.Text = lotDt.Rows[0]["producttype"].ToString();

        //生成条码
        LabelInfo();


    }
    protected void btnSaveClose_Click(object sender, EventArgs e)
    {

        #region//分批条码改为可以手动输入，判断条码是否已经存在 add by lei.xue on 2017-11-8
        string strExistSubLot = FilmCRUD.ExistSublot(txtLot.Text, txtLabelInfo.Text);
        if (strExistSubLot == "success")
        {
            JScript.Alert("该分批条码已经存在", this);
            return;
        }
        #endregion

        //插入basis表 ， 更新母批剩余数量 ，子批flowidno为当点站
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
        dl.currentflowidno =(Convert.ToInt32( dt.Rows[0]["flowidno"].ToString()) + 1).ToString(); //流程编号加1
        dl.factoryid = "";
        dl.workorder = ViewState["WO"].ToString();
        dl.reworkorder = "";
        dl.lotid = txtLabelInfo.Text;
        dl.lotcount = dt.Rows[0]["lotcount"].ToString();
        dl.ProcessComplete = "N";
        dl.UVCompleteLotid = txtLot.Text;
        dl.length = SplitLength;//txtSplitLength.Text;
        //================剩余长度和宽度为母批的有效长度和宽度modify by lei.xue on 2017-6-1=============
        dl.restlength = txtValidLength.Text;//SplitLength;//txtSplitLength.Text;
        dl.width = txtPreWidth.Text;
        dl.restwidth = txtValidWidth.Text;//txtPreWidth.Text;
        dl.Filmlevel = "A";
        //增加贴膜设备编号 add by lei.xue on 2017-2-22
        dl.eqpid = ddlEqp.SelectedValue;
        //增加有效幅宽 add by lei.xue on 2017-4-19
        dl.validwidth = txtValidWidth.Text;
        //增加有效长度 add by lei.xue on 2017-6-11
        dl.validlength = txtValidLength.Text;
        string result = CreateMouldLot.InsertLot(dl);
        if (result != "success")
        {
            JScript.Alert(result, this);
            return;
        }
        
        //插入分批记录到分批表
        dl.sublotid = txtLabelInfo.Text;
        dl.lotid = txtLot.Text;
        string resultsplit =  PasteFilm.InsertSplitLot(dl);

        //更新母批剩余数量
        Decimal rest = Convert.ToDecimal(txtRestLength.Text)-Convert.ToDecimal(SplitLength);//txtSplitLength.Text );
        string strRest = PasteFilm.UpdateParentQty(ViewState["originalLot"].ToString(), rest.ToString(), "Length");
        if (strRest != "success" )
        {
            JScript.Alert("更新母批剩余数量失败！", this);
            return;
        }

        if (result != "success" || resultsplit != "success")
        {
            JScript.Alert("创建贴膜条码失败！", this);
            return;
        }
        
        //子批过站记录 add by lei.xue on 2017-4-26=========================================
        string strWipinfo =  PasteFilm.FilmCheckOut(txtLabelInfo.Text, ddlEqp.SelectedValue, ddlWorkshop.SelectedValue, lblWorksiteID.Text, System.Web.HttpContext.Current.Request.Cookies["userID"].Value.ToString());
        if (strWipinfo == "fail")
        {
            JScript.Alert("子批过站记录失败！", this);
            return;
        }
        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>PrintLabel('" + txtLabelInfo.Text + "','../Product/PasteFilm.aspx','" + txtProductType.Text + "');</script>");
        //JScript.Alert("创建贴膜条码成功！", this);
    }
    protected void btnSplitEnter_Click(object sender, EventArgs e)
    {
        //分批数量限制01-99
        //计算分批批号排序，查询分批表
        string strQty = PasteFilm.CheckSplitSum(txtLot.Text);
        if (strQty == "fail")
        {
            JScript.Alert("查询分批数量出错！", this);
            return;
        }
        else if (Convert.ToInt32(strQty) > 99)
        {
            JScript.Alert("分批数量大于99,无法分批", this);
            return;
        }
        //生成分批批号即子批（记录子批号的长和宽） ，
        //贴膜条码规则：贴膜后13码：在UV成型后10码基础上+‘-’+2码（代表第几卷，如01、02、03……递增）
        int intQty = Convert.ToInt32(strQty) + 1;
        txtLabelInfo.Text = txtLot.Text + "-" + string.Format("{0:D2}", intQty);
    }
    private void LabelInfo()
    {
        //分批数量限制01-99
        //计算分批批号排序，查询分批表
        string strQty = PasteFilm.CheckSplitSum(txtLot.Text);
        if (strQty == "fail")
        {
            JScript.Alert("查询分批数量出错！", this);
            return;
        }
        else if (Convert.ToInt32(strQty) > 99)
        {
            JScript.Alert("分批数量大于99,无法分批", this);
            return;
        }
        //生成分批批号即子批（记录子批号的长和宽） ，
        //贴膜条码规则：贴膜后13码：在UV成型后10码基础上+‘-’+2码（代表第几卷，如01、02、03……递增）
        int intQty = Convert.ToInt32(strQty) + 1;
        txtLabelInfo.Text = txtLot.Text + "-" + string.Format("{0:D2}", intQty);
    }
}