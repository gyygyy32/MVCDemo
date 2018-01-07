﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="AGCheck.aspx.cs" Inherits="Product_AGCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/submenu.css" rel="stylesheet" />
    <style type="text/css">
        .labelinfo {
            /*font-size: 14px;
            color: #666;
            font-weight: 700;
            padding-right: 10px;
            height: 42px;
            width: 75px;
            display: inline-block;
            text-align: right;*/
            display: inline-block;
            /*float: left;*/
            height: 42px;
            /*width: 75px;*/
            width: 85px;
            margin-right: 5px;
            line-height: 42px;
            font-size: 14px;
            color: #666;
            font-weight: 700;
            text-align: right;
            /*border:solid;*/
        }

        .txtinfo {
            box-sizing: border-box;
            height: 28px;
            /*Width: 213px;*/
            width: 180px;
            /*border-width: 2px;
    border-style: inset;
    border-color: initial;*/
        }

        .tb tr td {
            /*width: 350px;*/
            /*width: 380px;*/
            width: 300px;
            text-align: left;
            /*border:solid;*/
        }

        .tb {
            margin-left: auto;
            margin-right: auto;
        }

        .hide {
            display: none;
        }
        /*fieldset {
            text-align: center;
        }*/
        #rework {
            width: auto;
        }
    </style>
    <script src="../JS/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        //$(function () {
        //    var width = $('.tb').css('width');//document.getElementById('tbworkorder').offsetWidth;
        //    var tbprocess = document.getElementById('tbprocess');//.style.width = 1064;
        //    tbprocess.style.cssText = "width:1154px;margin-left:auto;margin-right:auto";

        //})

        function EnterTextBox() {
            if (event.keyCode == 13 && document.getElementById("<%=txtLot.ClientID%>").value != "") {
                event.keyCode = 9;
                event.returnValue = false;
                document.getElementById("<%=btnEnter.ClientID %>").click();
            }
        }

        function CheckData() {
            if (document.getElementById("<%=txtLot.ClientID%>").value == "") {
                alert("请输入批次号！");
                return false;
            }
            //checkbox是否选中
            if (document.getElementById("<%=cbxUpdateLevel.ClientID%>").checked == true) {
                if (document.getElementById("<%=ddlFilmLevel.ClientID%>").value == ""
                    || document.getElementById("<%=txtReason.ClientID%>").value == "") {
                    alert("请输入变更等级和原因");
                    return false;
                }
            }

            //=================检查检验项目=========================================================
            //1、厚度
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
            //厚度改为需要输入 modify by lei.xue on 2017-10-24
            if (document.getElementById("<%=txtThinknessLeft.ClientID%>").value == "") {
                alert("请输入厚度左检验信息！");
                return false;
            }
            if (document.getElementById("<%=txtThinknessMiddle.ClientID%>").value == "") {
                alert("请输入厚度中检验信息！");
                return false;
            }
            if (document.getElementById("<%=txtThinknessRight.ClientID%>").value == "") {
                alert("请输入厚度右检验信息！");
                return false;
            }
            if (document.getElementById("<%=ddlThinknessResult.ClientID%>").value == "") {
                alert("请输入厚度检验结果！");
                return false;
            }
            //2、外观
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
            //外观检验信息需要输入 modify by lei.xue on 2017-10-24
            if (document.getElementById("<%=txtAppearanceLeft.ClientID%>").value == "") {
                alert("请输入外观左检验信息！");
                return false;
            }
            if (document.getElementById("<%=txtAppearanceRight.ClientID%>").value == "") {
                alert("请输入外观右检验信息！");
                return false;
            }
            if (document.getElementById("<%=ddlAppearanceResult.ClientID%>").value == "") {
                alert("请输入外观检验结果！");
                return false;
            }
            //3、粒子 密度&高度 密度改为分布 modify by lei.xue on 2017-4-19===========================
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
<%--            if (document.getElementById("<%=txtParticleDistribution.ClientID%>").value == "") {
                alert("请输入粒子密度检验信息！");
                return false;
            }
            if (document.getElementById("<%=txtParticleHeight.ClientID%>").value == "") {
                alert("请输入粒子高度检验信息！");
                return false;
            }--%>
            if (document.getElementById("<%=ddlParticleResult.ClientID%>").value == "") {
                alert("请输入粒子检验结果！");
                return false;
            }
            //4、翘曲变形
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
<%--            if (document.getElementById("<%=txtBuckling.ClientID%>").value == "") {
                alert("请输入翘曲变形检验信息！");
                return false;
            }--%>
            if (document.getElementById("<%=ddlBucklingResult.ClientID%>").value == "") {
                alert("请输入翘曲变形检验结果！");
                return false;
            }
            //5、MD雾度
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
<%--            if (document.getElementById("<%=txtMDHaze.ClientID%>").value == "") {
                alert("请输入MD雾度检验信息！");
                return false;
            }--%>
            if (document.getElementById("<%=ddlMDHazeResult.ClientID%>").value == "") {
                alert("请输入MD雾度检验结果！");
                return false;
            }
            //6、MD穿透率
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
<%--            if (document.getElementById("<%=txtPenetrance.ClientID%>").value == "") {
                alert("请输入MD穿透率检验信息！");
                return false;
            }--%>
            if (document.getElementById("<%=ddlPenetranceResult.ClientID%>").value == "") {
                alert("请输入MD穿透率检验结果！");
                return false;
            }
            //7、可用宽幅
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
<%--            if (document.getElementById("<%=txtAvailableWidth.ClientID%>").value == "") {
                alert("请输入MD穿透率检验信息！");
                return false;
            }--%>
            if (document.getElementById("<%=ddlAvailableWidthResult.ClientID%>").value == "") {
                alert("请输入MD穿透率检验结果！");
                return false;
            }
            //8、纹路
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
<%--            if (document.getElementById("<%=txtLines.ClientID%>").value == "") {
                alert("请输入纹路检验信息！");
                return false;
            }--%>
            if (document.getElementById("<%=ddlLinesResult.ClientID%>").value == "") {
                alert("请输入纹路检验结果！");
                return false;
            }
            //9、百格
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
<%--            if (document.getElementById("<%=txtBaige.ClientID%>").value == "") {
                alert("请输入百格检验信息！");
                return false;
            }--%>
            if (document.getElementById("<%=ddlBaigeResult.ClientID%>").value == "") {
                alert("请输入百格检验结果！");
                return false;
            }
            //10、铅笔硬度
            //检验信息可以不输入 modify by lei.xue on 2017-9-15
<%--            if (document.getElementById("<%=txtPencilHardness.ClientID%>").value == "") {
                alert("请输入铅笔硬度检验信息！");
                return false;
            }--%>
            if (document.getElementById("<%=ddltxtPencilHardnessResult.ClientID%>").value == "") {
                alert("请输入铅笔硬度检验结果！");
                return false;
            }


            //=================检查检验项目=========================================================
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="submenu">
        <div id="right_menu">
            <div id="daohang">
                <img src="../image/home.png" />制造模块-AG涂布检验
                <asp:Label ID="lblWorksiteID" runat="server" CssClass="hide">M37</asp:Label>
            </div>
            <!--end daohang-->
            <div id="button">
                <ul>
                    <li>
                        <asp:Button ID="btnSaveClose" runat="server" Text="确定"
                            CssClass="daohang_btn_saveclose" OnClientClick=" return CheckData()" OnClick="btnSaveClose_Click" /></li>
                    <%--                    <li>
                        <asp:Button ID="btnEdit" runat="server" Text="修改"
                            CssClass="daohang_btn_edit" /></li>
                    <li>
                        <asp:Button ID="btnReset" runat="server" OnClientClick="ClearAllTextBox()" Text="重置"
                            CssClass="daohang_btn_reset" /></li>
                    <li>
                        <input type="button" class="daohang_btn_return" value="返回" onclick="window.location.reload('SpcWriter_Index.aspx');" /></li>--%>
                </ul>
            </div>
            <!--end button-->
        </div>

    </div>

    <div id="worksite">

        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>站点信息</legend>

            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">批次号：</label>
                        <asp:TextBox ID="txtLot" runat="server" CssClass="txtinfo"></asp:TextBox>
                        <asp:Button ID="btnEnter" runat="server" CssClass="hide" Text="Button" OnClick="btnEnter_Click" />
                    </td>

                    <td>
                        <label class="labelinfo">车间：</label>
                        <asp:DropDownList ID="ddlWorkshop" runat="server" CssClass="txtinfo" disabled="disabled"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">设备机台：</label>
                        <asp:DropDownList ID="ddlEqp" runat="server" CssClass="txtinfo" disabled="disabled"></asp:DropDownList>
                    </td>
                    <td></td>
                </tr>

            </table>
            <div>
                <table id="tbprocess" style="width: 1218px; margin-left: auto; margin-right: auto">
                    <tr>
                        <td style="text-align: left;">
                            <label class="labelinfo">批次流程：</label>
                            <asp:Label ID="lblLotprocess" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblCurrnentWorksite" runat="server" ForeColor="#ffffff" BackColor="#3366ff"></asp:Label>
                            <asp:Label ID="lblEndProcess" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>

    </div>
    <div id="thinkness">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>厚度</legend>
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">左：</label>
                        <asp:TextBox ID="txtThinknessLeft" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">中：</label>
                        <asp:TextBox ID="txtThinknessMiddle" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">右：</label>
                        <asp:TextBox ID="txtThinknessRight" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddlThinknessResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>

                </tr>
            </table>
        </fieldset>
    </div>
    <!-------------------------外观--------------------------------------->
    <div id="appearance">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>外观</legend>
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">左：</label>
                        <asp:TextBox ID="txtAppearanceLeft" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">右：</label>
                        <asp:TextBox ID="txtAppearanceRight" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddlAppearanceResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td></td>

                </tr>
            </table>
        </fieldset>
    </div>
    <!-------------------------外观--------------------------------------->
    <!-------------------------粒子--------------------------------------->
    <!-------------------------密度改为分布 modify by lei.xue on 2017-4-19---------------->
    <div id="particle">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>粒子</legend>
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">分布：</label>
                        <asp:TextBox ID="txtParticleDistribution" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">高度：</label>
                        <asp:TextBox ID="txtParticleHeight" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">大小：</label>
                        <asp:TextBox ID="txtParticleSize" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddlParticleResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>


                </tr>
            </table>
        </fieldset>
    </div>
    <!-------------------------粒子--------------------------------------->

    <div id="material">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>检验信息</legend>
            <table class="tb">
                <!--------------------------------------翘曲变形-------------------------------------------->
                <tr>
                    <td>
                        <label class="labelinfo">翘曲变形：</label>
                        <asp:TextBox ID="txtBuckling" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>

                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddlBucklingResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">雾度：</label>
                        <asp:TextBox ID="txtMDHaze" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>

                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddlMDHazeResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>

                </tr>
                <!--------------------------------------翘曲变形-------------------------------------------->
                <!--------------------------------------MD雾度-------------------------------------------->
                <!--<tr>

                    <td></td>
                    <td></td>

                </tr>-->
                <!--------------------------------------MD雾度-------------------------------------------->
                <!--------------------------------------MD穿透率-------------------------------------------->
                <tr>
                    <td>
                        <label class="labelinfo">穿透率：</label>
                        <asp:TextBox ID="txtPenetrance" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>

                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddlPenetranceResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">可用宽幅：</label>
                        <asp:TextBox ID="txtAvailableWidth" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>

                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddlAvailableWidthResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>

                </tr>
                <!--------------------------------------MD穿透率-------------------------------------------->
                <!--------------------------------------可用宽幅-------------------------------------------->
                <!--<tr>

                    <td></td>
                    <td></td>

                </tr>-->
                <!--------------------------------------可用宽幅-------------------------------------------->
                <!--------------------------------------纹路-------------------------------------------->
                <tr>
                    <td>
                        <label class="labelinfo">纹路：</label>
                        <asp:TextBox ID="txtLines" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>

                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddlLinesResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">百格：</label>
                        <asp:TextBox ID="txtBaige" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>

                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddlBaigeResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>

                </tr>
                <!--------------------------------------纹路-------------------------------------------->
                <!--------------------------------------百格-------------------------------------------->
                <!--<tr>

                    <td></td>
                    <td></td>

                </tr>-->
                <!--------------------------------------百格-------------------------------------------->
                <!--------------------------------------铅笔硬度-------------------------------------------->
                <tr>
                    <td>
                        <label class="labelinfo">铅笔硬度：</label>
                        <asp:TextBox ID="txtPencilHardness" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>

                    <td>
                        <label class="labelinfo">检验结果：</label>
                        <asp:DropDownList ID="ddltxtPencilHardnessResult" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>

                </tr>
                <!--------------------------------------铅笔硬度-------------------------------------------->
                <tr>
                    <td>
                        <label class="labelinfo">等级：</label>
                        <asp:TextBox ID="txtFilmLevel" runat="server" CssClass="txtinfo" disable="false" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <div class="labelinfo">
                            <asp:CheckBox ID="cbxUpdateLevel" runat="server" />
                            <label>等级：</label>
                        </div>
                        <asp:DropDownList ID="ddlFilmLevel" CssClass="txtinfo" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">变更原因：</label>
                        <asp:TextBox ID="txtReason" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td></td>

                </tr>
            </table>
        </fieldset>
    </div>

</asp:Content>
