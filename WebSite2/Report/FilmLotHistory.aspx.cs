﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Pub;

public partial class Report_FilmLotHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CommonClass.isAllow(User.Identity.Name, this, "模具查询");
      
        }
    }


    protected void btnSaveClose_Click(object sender, EventArgs e)
    {
        //===============查询站点信息=========================
        DataTable QCdt;
        DataTable dt = FilmQuery.QueryData(txtLot.Text);
        //===============增加无数据提示 add by lei.xue on 2017-8-21===============
        if (dt.Rows.Count == 0)
        {
            JScript.Alert("未查询到数据！", this);
            return;
        }
        //过站实体类
        WipLotDatalist AGDatalist = new WipLotDatalist();
        WipLotDatalist UVBackDatalist = new WipLotDatalist();
        WipLotDatalist UVCompleteDatalist = new WipLotDatalist();
        WipLotDatalist PasteFilmDatalist = new WipLotDatalist();
        WipLotDatalist SubsectionDatalist = new WipLotDatalist();
        WipLotDatalist PackageDatalist = new WipLotDatalist();
        //检验实体类
        AGCoatingQCDatalist QCAGDatalist = new AGCoatingQCDatalist();
        AGCoatingQCDatalist QCUVBackDatalist = new AGCoatingQCDatalist();
        AGCoatingQCDatalist QCUVCompleteDatalist = new AGCoatingQCDatalist();
        AGCoatingQCDatalist QCPasteFilmDatalist = new AGCoatingQCDatalist();
        AGCoatingQCDatalist QCSubsectionDatalist = new AGCoatingQCDatalist();

        //检验结果
        string QCAGJson = "";
        string QCUVBackJson = "";
        string QCUVCompleteJson = "";
        string QCPasteFilmJson = "";
        string QCSubsectionJson = "";

        //工单类型
        string WOType = "";
        //工单厚度
        string WOThinkness = "";
        //本站长度
        string CurrentWorksiteLength = "";
        //本站宽度
        string CurrentWorksiteWidth = "";
        //=============增加本站有效宽幅 add by lei.xue on 2017-5-24=======================================
        string CurrentWorksiteValidWidth = "";
        if (dt.Rows.Count > 0)
        {
            DataRow[] dr;
            dr = dt.Select("1=1","createtime desc");
            WOType = dr[0]["producttype"].ToString();
            WOThinkness = dr[0]["filmthinkness"].ToString();
            CurrentWorksiteLength = dr[0]["filmlength"].ToString();
            CurrentWorksiteWidth = dr[0]["filmwidth"].ToString();
            CurrentWorksiteValidWidth = dr[0]["filmvalidwidth"].ToString();
            #region//1、AG涂布=====================================================================
            dr = dt.Select("worksiteid = 'AG涂布'");
            if (dr.Length > 0)
            {
                AGDatalist.createtime = dr[0]["createtime"].ToString();
                AGDatalist.eqpid = dr[0]["eqpid"].ToString();
                AGDatalist.length = dr[0]["filmlength"].ToString();
                AGDatalist.width = dr[0]["filmwidth"].ToString();
                AGDatalist.PET = dr[0]["PET"].ToString();
                AGDatalist.userid = dr[0]["createuser"].ToString();
                AGDatalist.thinkness = dr[0]["filmthinkness"].ToString();
                AGDatalist.validwidth = dr[0]["filmvalidwidth"].ToString();
                AGDatalist.FatherLotidLength = dr[0]["FatherLotidLength"].ToString();
                AGDatalist.FatherLotidWidth = dr[0]["FatherLotidWidth"].ToString();
                //增加有效长度 add by lei.xue on 2017-6-13
                AGDatalist.validlength = dr[0]["filmvalidlength"].ToString();
                //增加员工姓名 add by lei.xue on 2017-6-16
                AGDatalist.username = dr[0]["username"].ToString();
            }
            //AGCoatingGlueType
            dr = dt.Select("worksiteid = 'AG涂布' and paratypeEn='AGCoatingGlueType'");
            if (dr.Length > 0) { AGDatalist.Glue = dr[0]["paraid"].ToString(); }
            #endregion
            #region//2、UV背涂======================================================================
            dr = dt.Select("worksiteid = 'UV背涂'");
            if (dr.Length > 0)
            {
                UVBackDatalist.createtime = dr[0]["createtime"].ToString();
                UVBackDatalist.eqpid = dr[0]["eqpid"].ToString();
                UVBackDatalist.length = dr[0]["filmlength"].ToString();
                UVBackDatalist.width = dr[0]["filmwidth"].ToString();
                UVBackDatalist.PET = dr[0]["PET"].ToString();
                UVBackDatalist.userid = dr[0]["createuser"].ToString();
                UVBackDatalist.thinkness = dr[0]["filmthinkness"].ToString();
                UVBackDatalist.validwidth = dr[0]["filmvalidwidth"].ToString();
                UVBackDatalist.FatherLotidLength = dr[0]["FatherLotidLength"].ToString();
                UVBackDatalist.FatherLotidWidth = dr[0]["FatherLotidWidth"].ToString();
                //增加有效长度 add by lei.xue on 2017-6-13
                UVBackDatalist.validlength = dr[0]["filmvalidlength"].ToString();
                //增加员工姓名 add by lei.xue on 2017-6-16
                UVBackDatalist.username = dr[0]["username"].ToString();
            }
            //AGCoatingGlueType
            dr = dt.Select("worksiteid = 'UV背涂' and paratypeEn='UVBackGlueType'");
            if (dr.Length > 0) { UVBackDatalist.Glue = dr[0]["paraid"].ToString(); }
            //MouldLot
            dr = dt.Select("worksiteid = 'UV背涂' and paratypeEn='MouldLot'");
            if (dr.Length > 0) { UVBackDatalist.Mouldlot = dr[0]["paraid"].ToString(); }
            //雾度
            dr = dt.Select("worksiteid = 'UV背涂' and paratypeEn='Haze'");
            if (dr.Length > 0) { UVBackDatalist.Haze = dr[0]["paraid"].ToString(); }
            #endregion
            #region//3、UV成型======================================================================
            dr = dt.Select("worksiteid = 'UV成型'");
            if (dr.Length > 0)
            {
                UVCompleteDatalist.createtime = dr[0]["createtime"].ToString();
                UVCompleteDatalist.eqpid = dr[0]["eqpid"].ToString();
                UVCompleteDatalist.length = dr[0]["filmlength"].ToString();
                UVCompleteDatalist.width = dr[0]["filmwidth"].ToString();
                UVCompleteDatalist.PET = dr[0]["PET"].ToString();
                UVCompleteDatalist.userid = dr[0]["createuser"].ToString();
                UVCompleteDatalist.validwidth = dr[0]["filmvalidwidth"].ToString();
                UVCompleteDatalist.FatherLotidLength = dr[0]["FatherLotidLength"].ToString();
                UVCompleteDatalist.FatherLotidWidth = dr[0]["FatherLotidWidth"].ToString();
                //增加有效长度 add by lei.xue on 2017-6-13
                UVCompleteDatalist.validlength = dr[0]["filmvalidlength"].ToString();
                //增加员工姓名 add by lei.xue on 2017-6-16
                UVCompleteDatalist.username = dr[0]["username"].ToString();
                
            }
            //AGCoatingGlueType
            dr = dt.Select("worksiteid = 'UV成型' and paratypeEn='UVCompleteGlueType'");
            if (dr.Length > 0) { UVCompleteDatalist.Glue = dr[0]["paraid"].ToString(); }
            //MouldLot
            dr = dt.Select("worksiteid = 'UV成型' and paratypeEn='MouldLot'");
            if (dr.Length > 0) { UVCompleteDatalist.Mouldlot = dr[0]["paraid"].ToString(); }
            #endregion
            #region//4、贴膜=========================================================================
            dr = dt.Select("worksiteid = '贴膜'");
            if (dr.Length > 0)
            {
                PasteFilmDatalist.createtime = dr[0]["createtime"].ToString();
                PasteFilmDatalist.eqpid = dr[0]["eqpid"].ToString();
                PasteFilmDatalist.length = dr[0]["filmlength"].ToString();
                PasteFilmDatalist.width = dr[0]["filmwidth"].ToString();
                PasteFilmDatalist.userid = dr[0]["createuser"].ToString();
                //增加员工姓名 add by lei.xue on 2017-6-16
                PasteFilmDatalist.username = dr[0]["username"].ToString();
            }
            #endregion

            #region//5、分条=========================================================================
            dr = dt.Select("worksiteid = '分条'");
            if (dr.Length > 0)
            {
                SubsectionDatalist.createtime = dr[0]["createtime"].ToString();
                SubsectionDatalist.eqpid = dr[0]["eqpid"].ToString();
                SubsectionDatalist.length = dr[0]["filmlength"].ToString();
                SubsectionDatalist.width = dr[0]["filmwidth"].ToString();
                SubsectionDatalist.userid = dr[0]["createuser"].ToString();
                //增加员工姓名 add by lei.xue on 2017-6-16
                SubsectionDatalist.username = dr[0]["username"].ToString();
            }
            #endregion

            #region//6、包装========================================================================
            dr = dt.Select("worksiteid = '包装'");
            if (dr.Length > 0)
            {
                PackageDatalist.createtime = dr[0]["createtime"].ToString();
                PackageDatalist.userid = dr[0]["createuser"].ToString();
                //增加员工姓名 add by lei.xue on 2017-6-16
                PackageDatalist.username = dr[0]["username"].ToString();
            }
            #endregion

            //===================检验内容===============================
            QCdt = Runcard.QueryQCData(txtLot.Text);
            if (QCdt.Rows.Count > 0)
            {
                #region//1、AG涂布检验=================================================================
                dr = QCdt.Select("worksiteid = 'AG涂布检验'");
                if (dr.Length > 0)
                {
                    QCAGDatalist.createtime = dr[0]["createtime"].ToString();
                    QCAGDatalist.userid = dr[0]["createuser"].ToString();
                    //增加员工姓名 add by lei.xue on 2017-6-16
                    QCAGDatalist.username = dr[0]["username"].ToString();
                    
                }
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='厚度' and parasubtype = '左' ");
                if (dr.Length > 0) { QCAGDatalist.thinknessleft = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='厚度' and parasubtype = '中' ");
                if (dr.Length > 0) { QCAGDatalist.thinknessmiddle = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='厚度' and parasubtype = '右' ");
                if (dr.Length > 0) { QCAGDatalist.thinknessright = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='MD雾度' ");
                if (dr.Length > 0) { QCAGDatalist.MDhaze = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='MD穿透率' ");
                if (dr.Length > 0) { QCAGDatalist.MDpenetration = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='粒子' and parasubtype = '分布' ");
                if (dr.Length > 0) { QCAGDatalist.ParticleDistribution = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='百格' ");
                if (dr.Length > 0) { QCAGDatalist.baige = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='铅笔硬度' ");
                if (dr.Length > 0) { QCAGDatalist.pencilhardness = dr[0]["paraid"].ToString(); }
                //外观左右
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='外观' and parasubtype = '左' ");
                if (dr.Length > 0) { QCAGDatalist.appearanceleft = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'AG涂布检验' and paratype='外观' and parasubtype = '右' ");
                if (dr.Length > 0) { QCAGDatalist.appearanceright = dr[0]["paraid"].ToString(); }
                #endregion
                #region//2、UV背涂检验
                dr = QCdt.Select("worksiteid = 'UV背涂检验'");
                if (dr.Length > 0)
                {
                    QCUVBackDatalist.createtime = dr[0]["createtime"].ToString();
                    QCUVBackDatalist.userid = dr[0]["createuser"].ToString();
                    //增加员工姓名 add by lei.xue on 2017-6-16
                    QCUVBackDatalist.username = dr[0]["username"].ToString();
                }
                dr = QCdt.Select("worksiteid = 'UV背涂检验' and paratype='厚度' and parasubtype = '左' ");
                if (dr.Length > 0) { QCUVBackDatalist.thinknessleft = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV背涂检验' and paratype='厚度' and parasubtype = '中' ");
                if (dr.Length > 0) { QCUVBackDatalist.thinknessmiddle = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV背涂检验' and paratype='厚度' and parasubtype = '右' ");
                if (dr.Length > 0) { QCUVBackDatalist.thinknessright = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV背涂检验' and paratype='MD雾度' ");
                if (dr.Length > 0) { QCUVBackDatalist.MDhaze = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV背涂检验' and paratype='MD穿透率' ");
                if (dr.Length > 0) { QCUVBackDatalist.MDpenetration = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV背涂检验' and paratype='百格' ");
                if (dr.Length > 0) { QCUVBackDatalist.baige = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV背涂检验' and paratype='铅笔硬度' ");
                if (dr.Length > 0) { QCUVBackDatalist.pencilhardness = dr[0]["paraid"].ToString(); }
                //外观左右
                dr = QCdt.Select("worksiteid = 'UV背涂检验' and paratype='外观' and parasubtype = '左' ");
                if (dr.Length > 0) { QCUVBackDatalist.appearanceleft = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV背涂检验' and paratype='外观' and parasubtype = '右' ");
                if (dr.Length > 0) { QCUVBackDatalist.appearanceright = dr[0]["paraid"].ToString(); }
                #endregion
                #region//3、UV成型检验
                dr = QCdt.Select("worksiteid = 'UV成型检验'");
                if (dr.Length > 0)
                {
                    QCUVCompleteDatalist.createtime = dr[0]["createtime"].ToString();
                    QCUVCompleteDatalist.userid = dr[0]["createuser"].ToString();
                    //增加员工姓名 add by lei.xue on 2017-6-16
                    QCUVCompleteDatalist.username = dr[0]["username"].ToString();
                }
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='厚度' and parasubtype = '左' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.thinknessleft = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='厚度' and parasubtype = '中' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.thinknessmiddle = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='厚度' and parasubtype = '右' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.thinknessright = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='翘曲变形' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.buckling = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='耐磨' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.abrasion = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='百格' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.baige = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='高低差dH' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.HeightDifferenceDH = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='纹路' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.lines = dr[0]["paraid"].ToString(); }
                //增加辉度增益比 add by lei.xue on 2017-5-18
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='辉度增益比' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.brilliancy = dr[0]["paraid"].ToString(); }
                //外观左右
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='外观' and parasubtype = '左' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.appearanceleft = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = 'UV成型检验' and paratype='外观' and parasubtype = '右' ");
                if (dr.Length > 0) { QCUVCompleteDatalist.appearanceright = dr[0]["paraid"].ToString(); }
                #endregion
                #region//4、贴膜
                dr = QCdt.Select("worksiteid = '贴膜检验'");
                if (dr.Length > 0)
                {
                    QCPasteFilmDatalist.createtime = dr[0]["createtime"].ToString();
                    QCPasteFilmDatalist.userid = dr[0]["createuser"].ToString();
                    //增加员工姓名 add by lei.xue on 2017-6-16
                    QCPasteFilmDatalist.username = dr[0]["username"].ToString();
                }

                dr = QCdt.Select("worksiteid = '贴膜检验' and paratype='点线' ");
                if (dr.Length > 0) { QCPasteFilmDatalist.lines = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = '贴膜检验' and paratype='有效宽幅' ");
                if (dr.Length > 0) { QCPasteFilmDatalist.abrasion = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = '贴膜检验' and paratype='百格' ");
                if (dr.Length > 0) { QCPasteFilmDatalist.baige = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = '贴膜检验' and paratype='高低差dH' ");
                if (dr.Length > 0) { QCPasteFilmDatalist.HeightDifferenceDH = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = '贴膜检验' and paratype='纹路' ");
                if (dr.Length > 0) { QCPasteFilmDatalist.lines = dr[0]["paraid"].ToString(); }
                //正面保护膜张力
                dr = QCdt.Select("worksiteid = '贴膜检验' and paratype='正面保护膜张力' ");
                if (dr.Length > 0) { QCPasteFilmDatalist.FrontTension = dr[0]["paraid"].ToString(); }
                //背面保护膜张力
                dr = QCdt.Select("worksiteid = '贴膜检验' and paratype='背面保护膜张力' ");
                if (dr.Length > 0) { QCPasteFilmDatalist.BackTension = dr[0]["paraid"].ToString(); }
                //外观左右
                dr = QCdt.Select("worksiteid = '贴膜检验' and paratype='外观' and parasubtype = '左' ");
                if (dr.Length > 0) { QCPasteFilmDatalist.appearanceleft = dr[0]["paraid"].ToString(); }
                dr = QCdt.Select("worksiteid = '贴膜检验' and paratype='外观' and parasubtype = '右' ");
                if (dr.Length > 0) { QCPasteFilmDatalist.appearanceright = dr[0]["paraid"].ToString(); }
                #endregion
                ////5、分条
                //QCSubsectionJson = DataByWorksite(QCdt, "分条检验");
            }
            //===============查询站点信息=========================
            string content = "       <table class=\"tblothistory\" border=\"2px\" bordercolor=\"#000000\" cellspacing=\"0\">" +
            "           <tr class=\"trlothistory01\">" +
            "               <td class=\"tdtitle\" style=\"font-weight: bolder; font-size: 16px;\">型号</td>" +
            "               <td>"+WOType+"</td>" +
            "               <td class=\"tdtitle\">厚度</td>" +
            "               <td>"+WOThinkness+"</td>" +
            "               <td class=\"tdtitle\">宽幅</td>" +
            //===========当前站点宽幅改为有效宽幅 modify by lei.xue on 2017-5-24===================================
            "               <td>"+CurrentWorksiteValidWidth+"</td>" +
            "               <td class=\"tdtitle\">长度</td>" +
            "               <td>"+CurrentWorksiteLength+"</td>" +
            "           </tr>" +
            "           <tr>" +
            "               <td rowspan=\"9\" class=\"tdtitle\">AG涂布/UV背涂</td>" +
            "           </tr>" +
            "           <tr class=\"trlothistory\">" +
            "               <td>机台</td>" +
            "               <td>" + AGDatalist.eqpid + UVBackDatalist.eqpid + "</td>" +     //ag涂布和uv背涂机台
            "               <td rowspan=\"8\" class=\"tdtitle\">AG涂布/UV背涂检验</td>" +
            "               <td>厚度</td>" +
            "               <td>" + "左:" + QCAGDatalist.thinknessleft + QCUVBackDatalist.thinknessleft + "中:" + QCAGDatalist.thinknessmiddle + QCUVBackDatalist.thinknessmiddle + "右:" + QCAGDatalist.thinknessright + QCUVBackDatalist.thinknessright + "</td>" + //ag涂布和uv背涂检验
            "               <td>百格</td>" +
            "               <td>" + QCAGDatalist.baige + QCUVBackDatalist.baige + "</td>" +

            "           </tr>" +
            "           <tr>" +
            "               <td>模具</td>" +
            "               <td>" + UVBackDatalist.Mouldlot + "</td>" + //uv背涂模具编号
            "               <td>雾度</td>" +
            "               <td>" + UVBackDatalist.Haze + "</td>" +
            "               <td>硬度</td>" +
            "               <td>" + QCAGDatalist.pencilhardness + QCUVBackDatalist.pencilhardness + "</td>" +

            "           </tr>" +
            "           <tr>" +
            "               <td>PET</td>" +
            "               <td>" + AGDatalist.PET + UVBackDatalist.PET + "</td>" +
            "               <td>透光率</td>" +
            "               <td>" + QCAGDatalist.MDpenetration + QCUVBackDatalist.MDpenetration + "</td>" +
            "               <td>时间</td>" +
            "               <td>" + QCAGDatalist.createtime + QCUVBackDatalist.createtime + "</td>" +//ag检验时间

            "           </tr>" +
            "           <tr>" +
            "               <td>胶水</td>" +
            "               <td>" + AGDatalist.Glue + UVBackDatalist.Glue + "</td>" +//ag涂布胶水
            "               <td>粒子密度</td>" +
            "               <td></td>" +
            "               <td>人员</td>" +
            "               <td>" + QCAGDatalist.userid + QCUVBackDatalist.userid + "/" + QCAGDatalist.username + QCUVBackDatalist.username + "</td>" +//ag检验人员

            "           </tr>" +
            "           <tr>" +
            "               <td>宽幅(原始/有效)</td>" +
            //=====================原始宽度改为子批宽幅 modify by lei.xue on 2017-6-16==============================
            //"               <td>" + AGDatalist.FatherLotidWidth + UVBackDatalist.FatherLotidWidth + "/" + AGDatalist.validwidth + UVBackDatalist.validwidth + "</td>" +//ag涂布宽幅
            "               <td>" + AGDatalist.width + UVBackDatalist.width + "/" + AGDatalist.validwidth + UVBackDatalist.validwidth + "</td>" +//ag涂布宽幅
            "               <td rowspan=\"2\">外观</td>" +

            "               <td colspan=\"3\" rowspan=\"2\">" + "左:" + QCAGDatalist.appearanceleft + QCUVBackDatalist.appearanceleft + "右:" + QCAGDatalist.appearanceright + QCUVBackDatalist.appearanceright + "</td>" + //ag涂布外观

            "           </tr>" +
            "           <tr>" +
            "               <td>长度(原始/有效)</td>" +
            "               <td>" + AGDatalist.length + UVBackDatalist.length + "/" + AGDatalist.validlength + UVBackDatalist.validlength + "</td>" + //长度取有效长度 modify by lei.xue on 2017-6-13

            "           </tr>" +
            "           <tr>" +
            "               <td>人员</td>" +
            "               <td>" + AGDatalist.userid + UVBackDatalist.userid +"/"+AGDatalist.username+UVBackDatalist.username+ "</td>" +
            "               <td rowspan=\"2\">纹路</td>" +
            "               <td colspan=\"3\" rowspan=\"2\"></td>" +

            "           </tr>" +
            "           <tr>" +
            "               <td>时间</td>" +
            "               <td>" + AGDatalist.createtime + UVBackDatalist.createtime + "</td>" +
            "           </tr>" +
            "           <tr class=\"trlothistory\">" +
            "               <td rowspan=\"8\" class=\"tdtitle\">UV成型</td>" +
            "               <td>机台</td>" +
            "               <td>" + UVCompleteDatalist.eqpid + "</td>" +
            "               <td rowspan=\"8\"class=\"tdtitle\">成型检验</td>" +
            "               <td>厚度</td>" +
            "               <td>" + "左:" + QCUVCompleteDatalist.thinknessleft + "中:" + QCUVCompleteDatalist.thinknessmiddle + "右:" + QCUVCompleteDatalist.thinknessright + "</td>" +
            "               <td>百格</td>" +
            "               <td>" + QCUVCompleteDatalist.baige + "</td>" +
            "           </tr>" +
            "           <tr>" +
            "               <td>模具</td>" +
            "               <td>" + UVCompleteDatalist.Mouldlot + "</td>" + //uv成型模具
            "               <td>辉度</td>" +
            "               <td>" + QCUVCompleteDatalist.brilliancy + "</td>" + //uv成型辉度增益比
            "               <td>dH</td>" +
            "               <td>" + QCUVCompleteDatalist.HeightDifferenceDH + "</td>" +//uv成型高低差dH
            "           </tr>" +
            "           <tr>" +
            "               <td>PET</td>" +
            "               <td>" + UVCompleteDatalist.PET + "</td>" +//uv成型pet
            "               <td>耐磨</td>" +
            "               <td></td>" +
            "               <td>时间</td>" +
            "               <td>" + QCUVCompleteDatalist.createtime + "</td>" + //uv成型检验时间
            "           </tr>" +
            "           <tr>" +
            "               <td>胶水</td>" +
            "               <td>" + UVCompleteDatalist.Glue + "</td>" +//uv成型胶水
            "               <td>翘曲</td>" +
            "               <td>" + QCUVCompleteDatalist.buckling + "</td>" +//uv成型检验翘曲
            "               <td>人员</td>" +
            "               <td>" + QCUVCompleteDatalist.userid +"/"+QCUVCompleteDatalist.username+ "</td>" +//uv成型检验人员
            "           </tr>" +
            "           <tr>" +
            "               <td>宽幅(原始/有效)</td>" +
            //===================原始宽度改为子批宽度 modify by lei.xue on 2017-6-16==============================================
            "               <td>" + UVCompleteDatalist.width + "/" + UVCompleteDatalist.validwidth + "</td>" + //uv成型宽幅
            "               <td rowspan=\"2\">外观</td>" +
            "               <td colspan=\"3\" rowspan=\"2\">"+"左:"+QCUVCompleteDatalist.appearanceleft+"右:"+QCUVCompleteDatalist.appearanceright+"</td>" + //uv成型检验外观左右
            "           </tr>" +
            "           <tr>" +
            "               <td>长度(原始/有效)</td>" +
            //===================原始宽度改为子批宽度 modify by lei.xue on 2017-6-16==============================================
            "               <td>" + UVCompleteDatalist.length + "/" + UVCompleteDatalist.validlength + "</td>" +//长度取有效长度 modify by lei.xue on 2017-6-13
            "           </tr>" +
            "           <tr>" +
            "               <td>人员</td>" +
            "               <td>" + UVCompleteDatalist.userid +"/"+UVCompleteDatalist.username+ "</td>" +
            "               <td rowspan=\"2\">纹路</td>" +
            "               <td colspan=\"3\" rowspan=\"2\">" + QCUVCompleteDatalist.lines + "</td>" +
            "           </tr>" +
            "           <tr>" +
            "               <td>时间</td>" +
            "               <td>" + UVCompleteDatalist.createtime + "</td>" + //uv成型时间
            "           </tr>" +
            "           <tr class=\"trlothistory\">" +
            "               <td rowspan=\"4\"class=\"tdtitle\">贴膜</td>" +
            "               <td>机台</td>" +
            "               <td>" + PasteFilmDatalist.eqpid + "</td>" +//贴膜机台
            "               <td>宽幅</td>" +
            "               <td>" + PasteFilmDatalist.validwidth + "</td>" +//贴膜宽幅
            "               <td rowspan=\"4\"class=\"tdtitle\">贴膜检验</td>" +
            "               <td>外观</td>" +
            "               <td>" + "左:" + QCPasteFilmDatalist.appearanceleft + "右:" + QCPasteFilmDatalist.appearanceright + "</td>" +//贴膜检验点线 //modify by lei.xue on 2017-5-3
            "           </tr>" +
            "           <tr>" +
            "               <td>正保</td>" +
            "               <td>" + QCPasteFilmDatalist.FrontTension + "</td>" +
            "               <td>长度</td>" +
            "               <td>" + PasteFilmDatalist.length + "</td>" +
            "               <td>纹路</td>" +
            "               <td></td>" +
            "           </tr>" +
            "           <tr>" +
            "               <td>背保</td>" +
            "               <td>" + QCPasteFilmDatalist.BackTension + "</td>" +
            "               <td>时间</td>" +
            "               <td>" + PasteFilmDatalist.createtime + "</td>" +//贴膜时间
            "               <td>人员</td>" +
            "               <td>" + QCPasteFilmDatalist.userid + "</td>" +//贴膜检验人员
            "           </tr>" +
            "           <tr>" +
            "               <td>人员</td>" +
            "               <td>" + PasteFilmDatalist.userid + "/"+PasteFilmDatalist.username+"</td>" +//贴膜人员
            "               <td></td>" +
            "               <td></td>" +
            "               <td>时间</td>" +
            "               <td>" + QCPasteFilmDatalist.createtime + "</td>" +//贴膜检验时间
            "           </tr>" +
            "           <tr class=\"trlothistory\">" +
            "               <td rowspan=\"2\"class=\"tdtitle\">分条</td>" +
            "               <td>机台</td>" +
            "               <td>" + SubsectionDatalist.eqpid + "</td>" +//分条机台
            "               <td>宽幅</td>" +
            "               <td>" + SubsectionDatalist.width + "</td>" +
            "               <td>长度</td>" +
            "               <td>" + SubsectionDatalist.length + "</td>" +
            "               <td></td>" +
            "           </tr>" +

            "           <tr>" +
            "               <td>人员</td>" +
            "               <td>" + SubsectionDatalist.userid+"/" +SubsectionDatalist.username+ "</td>" +//分条人员
            "               <td>时间</td>" +
            "               <td>" + SubsectionDatalist.createtime + "</td>" +//分条时间
            "               <td></td>" +
            "               <td></td>" +
            "               <td></td>" +
            "           </tr>" +
            "           <tr class=\"trlothistory\">" +
            "               <td class=\"tdtitle\">包装</td>" +
            "               <td>人员</td>" +
            "               <td>" + PackageDatalist.userid +"/"+PackageDatalist.username+ "</td>" +//package userid
            "               <td>时间</td>" +
            "               <td>" + PackageDatalist.createtime + "</td>" +
            "               <td></td>" +
            "               <td></td>" +
            "               <td></td>" +
            "           </tr>" +
            "       </table>";

            //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>return loaddata01('1');</script>");
            Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script >loaddata01('" + content + "');</script>");
        }
    }

}