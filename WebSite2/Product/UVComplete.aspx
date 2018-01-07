﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="UVComplete.aspx.cs" Inherits="Product_UVComplete" %>

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
            width: 125px;
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
            Width: 213px;
        }

        .tb tr td {
            /*width: 350px;*/
            width: 400px;
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

        #flow {
            width: 100%;
        }
    </style>
    <script src="../JS/JH2017-5-31-1.js"></script>
    <script src="../JS/jquery-1.8.3.min.js"></script>
    <script src="../JS/CommonFunction.js"></script>
    <script type="text/javascript">


        function EnterTextBox() {
            if (event.keyCode == 13 && document.getElementById("<%=txtLot.ClientID%>").value != "") {
                event.keyCode = 9;
                event.returnValue = false;
                document.getElementById("<%=btnEnter.ClientID %>").click();
            }
        }
        function EnterTextBoxSplit() {
            if (event.keyCode == 13 && document.getElementById("<%=txtSplitLength.ClientID%>").value != "") {
                event.keyCode = 9;
                event.returnValue = false;
                document.getElementById("<%=btnSplitEnter.ClientID %>").click();
            }
        }
        function EnterMould() {
            if (event.keyCode == 13 && document.getElementById("<%=txtMouldLot.ClientID%>").value != "") {
                event.keyCode = 9;
                event.returnValue = false;
                document.getElementById("<%=btnMouldLot.ClientID %>").click();
            }
        }

        function CheckData() {
            if (document.getElementById("<%=lblLotprocess.ClientID%>").innerText == "" &&
                document.getElementById("<%=lblCurrnentWorksite.ClientID%>").innerText == "" &&
                document.getElementById("<%=lblEndProcess.ClientID%>").innerText == "") {
                alert("请先输入批次号！");
                return false;
            }

            <%--//var length = document.getElementById("<%=txtRestLength.ClientID%>");--%>
            var prelength = document.getElementById("<%=txtPreLength.ClientID%>").value;
            var restLength = document.getElementById("<%=txtRestLength.ClientID%>").value;
            var splitLength = document.getElementById("<%=txtSplitLength.ClientID%>").value;
            var prewidth = document.getElementById("<%=txtPreWidth.ClientID%>").value;
            var validlength = document.getElementById("<%=txtValidLength.ClientID%>").value;
            var validwidth = document.getElementById("<%=txtValidWidth.ClientID%>").value;

<%--            if (document.getElementById("<%=txtGlueQty.ClientID%>").value == "" || isNaN(document.getElementById("<%=txtGlueQty.ClientID%>").value)) {
                alert("请输入胶水数量");
                return false;
            }--%>

            if (document.getElementById("<%=txtDKWidth.ClientID%>").value == "") {
                alert("请获取雕刻幅宽");
                return false;
            }

            if (document.getElementById("<%=txtMouldStructure.ClientID%>").value == "") {
                alert("请获取雕模具结构");
                return false;
            }

            if (document.getElementById("<%=txtMouldPitch.ClientID%>").value == "") {
                alert("请获取模具Pitch");
                return false;
            }
            //有效幅宽 add by lei.xue on 2017-4-18=====================================
            if (document.getElementById("<%=txtValidWidth.ClientID%>").value == "") {
                alert("请输入有效幅宽");
                return false;
            }
            //=========有效长度 add by lei.xue on 2017-5-28=================
            if (document.getElementById("<%=txtValidLength.ClientID%>").value == "") {
                alert("请输入有效长度！");
                return false;
            }

            //if (width.value == "" || isNaN(width.value)) {
            //    alert("请输入幅宽规格！");
            //    return false;
            //}

            //if (length.value == "" || isNaN(length.value)) {
            //    alert("请输入卷材长度！");
            //    return false;
            //}
            //=================取消长度和宽度卡关 modify by lei.xue on 2017-3-17 =====================
            //if (parseFloat(width.value) > parseFloat(prewidth.value)) {
            //    alert("幅宽规格大于前站幅宽规格！");
            //    return false;
            //}

            //if (parseFloat(length.value) > parseFloat(prelength.value)) {
            //    alert("卷材长度大于前站卷材长度！");
            //    return false;
            //}

            //if (parseFloat(width.value) <= 0) {
            //    alert("幅宽规格必须大于0");
            //    return false;
            //}

            //if (parseFloat(length.value) <= 0) {
            //    alert("卷材长度必须大于0");
            //    return false;
            //}
            var checkSplitlength = '';
            //==========UV成型需要分批 modify by lei.xue on 2017-3-27 =======================================================
            //==========分批时检查长度卡关 modify by lei.xue on 2017-5-27=====================================
            //========== 子批不是01必须输入分批宽度 modify by lei.xue on 2017-5-28=============================
            var labelinfo = document.getElementById("<%=txtLabelInfo.ClientID%>").value;
            if ((document.getElementById("<%=cbxSplit.ClientID%>").checked == true))//||
                //(parseInt(labelinfo.substr(labelinfo.length - 2, 2)) > 1))
            {
                //检查分批长度是否大于剩余长度   
                if (isNaN(splitLength) || splitLength == "") {
                    alert("分批长度请输入数字")
                    return false;
                }
                if ((parseFloat(splitLength) > parseFloat(restLength)) || (parseFloat(restLength) == 0)) {
                    alert("分批长度大于剩余长度，请重新输入！")
                    return false;
                }
                checkSplitlength = splitLength;
            }
            else {
                if ((parseFloat(prelength) > parseFloat(restLength)) || (parseFloat(restLength) == 0)) {
                    alert("分批长度大于剩余长度，请重新输入！")
                    return false;
                }
                checkSplitlength = prelength;
            }

            //检查有效长度和宽度不能大于分批长度
            if (parseFloat(validlength) > parseFloat(checkSplitlength)) {
                alert("有效长度大于分批长度，请重新输入！")
                return false;
            }
            //==============有效幅宽不能大于母批幅宽 add by lei.xue on 2017-6-16===================
            if (parseFloat(validwidth) > parseFloat(prewidth)) {
                alert("有效幅宽不能大于母批幅宽，请重新输入！")
                return false;
            }
            if (document.getElementById("<%=txtLabelInfo.ClientID%>").value == "") {
                alert("请生成条码！");
                return false;
            }


        }

        function CheckSplit() {
            var cbxSplit = document.getElementById("<%=cbxSplit.ClientID%>");
            var txtSplit = document.getElementById("<%=txtSplitLength.ClientID%>");

            if (cbxSplit.checked == false) {
                txtSplit.disabled = true;
                txtSplit.readOnly = true;
                txtSplit.style.background = "#e8e8e8";
            }
            else {
                txtSplit.disabled = false;
                txtSplit.readOnly = false;
                txtSplit.style.background = "#FFFFFF";
            }
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div id="submenu">
        <div id="right_menu">
            <div id="daohang">
                <img src="../image/home.png" />制造模块-UV成型
                <asp:Label ID="lblWorksiteID" runat="server" CssClass="hide">M45</asp:Label>
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
                        <input type="button" class="daohang_btn_return" value="返回" onclick="window.location.reload('Index.aspx');" /></li>--%>
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
                        <asp:TextBox ID="txtProductType" runat="server" CssClass="hide"></asp:TextBox>

                    </td>

                    <td>
                        <label class="labelinfo">车间：</label>
                        <asp:DropDownList ID="ddlWorkshop" runat="server" CssClass="txtinfo" disabled="disabled" BackColor="#e8e8e8"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">设备机台：</label>
                        <asp:DropDownList ID="ddlEqp" runat="server" CssClass="txtinfo" AutoPostBack="True" OnTextChanged="ddlEqp_TextChanged"></asp:DropDownList>
                    </td>
                </tr>

            </table>
            <div>
                <table style="width: 1214px; margin-left: auto; margin-right: auto">
                    <tr>
                        <td>
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
    <div id="material">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>信息录入</legend>
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">前站卷材长度：</label>
                        <asp:TextBox ID="txtPreLength" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">剩余卷材长度：</label>
                        <asp:TextBox ID="txtRestLength" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">前站幅宽规格：</label>
                        <asp:TextBox ID="txtPreWidth" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <!--<label class="labelinfo">分批卷材长度：</label>-->
                        <div class="labelinfo">
                            <asp:CheckBox ID="cbxSplit" runat="server" onclick="CheckSplit()" />
                            <label>分批卷材长度：</label>
                        </div>
                        <asp:TextBox ID="txtSplitLength" runat="server" CssClass="txtinfo" onkeypress="return onlyNumber()" BackColor="#e8e8e8"></asp:TextBox>
                        <asp:Button ID="btnSplitEnter" runat="server" CssClass="hide" Text="Button" />
                    </td>
                    <td>
                        <label class="labelinfo">有效卷材长度：</label>
                        <asp:TextBox ID="txtValidLength" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">有效宽幅规格：</label>
                        <asp:TextBox ID="txtValidWidth" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                        <label class="labelinfo">胶水规格：</label>
                        <asp:DropDownList ID="ddlGlueType" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">模具编号：</label>
                        <asp:TextBox ID="txtMouldLot" runat="server" CssClass="txtinfo" onkeypress="return onlyNumber()"></asp:TextBox>
                        <asp:Button ID="btnMouldLot" runat="server" CssClass="hide" Text="Button" OnClick="btnMouldLot_Click" />

                    </td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="MouldInfo">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>模具信息</legend>
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">雕刻幅宽：</label>
                        <asp:TextBox ID="txtDKWidth" CssClass="txtinfo" runat="server" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">模具结构：</label>
                        <asp:TextBox ID="txtMouldStructure" CssClass="txtinfo" runat="server" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">模具Pitch：</label>
                        <asp:TextBox ID="txtMouldPitch" CssClass="txtinfo" runat="server" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="Label">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>标签信息</legend>
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">标签信息：</label>
                        <asp:TextBox ID="txtLabelInfo" CssClass="txtinfo" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
    </div>

</asp:Content>

