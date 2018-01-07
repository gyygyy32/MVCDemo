﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="LabelReprint.aspx.cs" Inherits="Material_LabelReprint" %>

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
            /*Width: 213px;*/
            width: 213px;
        }

        .tb tr td {
            /*width: 350px;*/
            width: 350px;
            text-align: left;
            /*border:solid;*/
        }

        .tb {
            margin-left: auto;
            margin-right: auto;
        }
        /*fieldset {
            text-align: center;
        }*/
        .hide {
            display: none;
        }
    </style>

    <script type="text/javascript">
        function EnterTextBox() {
            if (event.keyCode == 13 && document.getElementById("<%=txtLot.ClientID%>").value != "") {
                event.keyCode = 9;
                event.returnValue = false;
                document.getElementById("<%=btnEnter.ClientID %>").click();
            }
        }

<%--        function CheckData() {
            if (document.getElementById("<%=txtLot.ClientID%>").value == "") {
                alert("请输入批次号！");
                return false;
            }

            if (document.getElementById("<%=ddlEqp.ClientID%>").value == "") {
                alert("请选择设备编号！");
                return false;
            }

            if (document.getElementById("<%=ddlWorkshop.ClientID%>").value == "") {
                alert("请选择车间！");
                return false;
            }

            if (document.getElementById("<%=txtDKWidth.ClientID%>").value == "") {
                alert("请输入雕刻幅宽！");
                return false;
            }

            if (document.getElementById("<%=ddlMouldStructure.ClientID%>").value == "") {
                alert("请选择模具结构！");
                return false;
            }

            if (document.getElementById("<%=ddlMouldPitch.ClientID%>").value == "") {
                alert("请选择模具Pitch！");
                return false;
            }

            if (document.getElementById("<%=txtLabelInfo.ClientID%>").value == "") {
                alert("请生成条码标签！");
                return false;
            }

        }--%>

        function writeFile(content, txtname, filetype) {
            //"C:\Program Files\Seagull\BarTender Suite\BarTender\bartend.exe" /F="D:\BT32\Pallet.btw" /D="D:\BT32\Pallet.txt" /P /x
            var fso, f, s, filename, f2
            //浏览器是否支持ActiveX控件
            if (!window.ActiveXObject) {
                alert("请使用IE浏览器打印标签");
                window.location.replace("../Product/Package.aspx");
            }
            fso = new ActiveXObject("Scripting.FileSystemObject");
            filename = 'C://BT32//' + txtname + '.' + filetype;
            if (fso.FileExists(filename) == true) {
                f2 = fso.GetFile(filename);
                f2.Delete();
            }


            f = fso.OpenTextFile(filename, 8, true);
            f.WriteLine(content);
            f.Close();
        }
        function PrintLabel() {
            var info = document.getElementById('<%=lblLotprocess.ClientID %>').innerText;
            if (info == "") {
                alert("请输入条码并按回车键");
                return false;
            }
            var labelContent = document.getElementById('<%=txtLot.ClientID %>').value;
            var content = '';
            var boxqty = 0;
            var qty = 0;
            //条码类型
            var labelContentUp = '';
            var labelContentDown = '';
            var Type = document.getElementById('<%=ddlLabelType.ClientID %>').value;

            //bartender9.01是否安装
            fso = new ActiveXObject("Scripting.FileSystemObject");
            filename = 'C://Program Files//Seagull//BarTender Suite//bartend.exe';
            filename64 = 'C://Program Files (x86)//Seagull//BarTender Suite//bartend.exe';
            //C:\Program Files (x86)\Seagull\BarTender Suite\bartend.exe
            //bartender10.1路径
            if ((fso.FileExists(filename) != true) && (fso.FileExists(filename64) != true)) {
                alert("请先安装Bartender10.1软件，目前无法打印");
                return false;
            }

            if (Type == "包装条码") {
                var lot = document.getElementById('<%=txtLot.ClientID %>').value;
                //长度
                var Length = document.getElementById('<%=txtPreLength.ClientID %>').value;
                //宽度
                var Width = document.getElementById('<%=txtPreWidth.ClientID %>').value;
                //厚度
                var Thinkness = document.getElementById('<%=txtThinkness.ClientID %>').value;
                //型号
                var Type = document.getElementById('<%=txtType.ClientID %>').value;
                //PET型号
                var PETType = document.getElementById('<%=txtPETType.ClientID %>').value;
                //品名
                var PinMin = document.getElementById('<%=txtPinMin.ClientID %>').value;
                //品号类型ddlPinHaoType
                var PinHao = document.getElementById('<%=txtPinHao.ClientID %>').value;
                //增加批次等级
                var Level = document.getElementById('<%=txtLevel.ClientID %>').value;
                //add by lei.xue on 2017-3-2 增加显示打印条码时间的标签
                //分条时间
                var SubsectionDate = document.getElementById('<%=txtSubsectionDate.ClientID %>').value;
                //贴膜时间
                var PasteFilmDate = document.getElementById('<%=txtPasteFilmDate.ClientID %>').value;
                //UV成型时间
                var UVCompleteDate = document.getElementById('<%=txtUVCompleteDate.ClientID %>').value;
                //包装增加黄标签 add by lei.xue on 2017-3-7

                var BeforeWeight = document.getElementById('<%=txtBeforePackageWeight.ClientID %>').value;
                var AfterWeight = document.getElementById('<%=txtAfterPackageWeight.ClientID %>').value;

                //增加有效幅宽 add by lei.xue on 2017-5-22
                var ValidWidth = document.getElementById('<%=txtValidWidth.ClientID %>').value;

                if (SubsectionDate != '') {
                    SubsectionDate = new Date(SubsectionDate).Format("yyyy-MM-dd");
                }

                if (PasteFilmDate != '') {
                    PasteFilmDate = new Date(PasteFilmDate).Format("yyyy-MM-dd");
                }

                if (UVCompleteDate != '') {
                    UVCompleteDate = new Date(UVCompleteDate).Format("yyyy-MM-dd");
                }
                //packageup：品名，长度，厚度/1000，宽度，型号，厚度，PET型号
                //packagedown：lot号，长度，型号，厚度，宽度，品号，品名，PET型号
                //down标签修改模板modify by lei.xue on 2017-3-2
                //======================宽度改为有效幅宽 modify by lei.xue on 2017-5-25============
                //======================增加产品等级信息 modify by lei.xue on 2017-5-25============
                var labelContentDown = Type + ',' + Thinkness + ',' + ValidWidth + ',' + lot + ',' + PinHao + ',' + PinMin + ',' + Length + ',' + Level;

                var labelContentDate = lot + ',' + UVCompleteDate + ',' + PasteFilmDate + ',' + SubsectionDate;
                //域顺序：型号，厚度，宽度，宽度*长度，长度，装箱前重，装箱后重
                //======================宽度改为有效幅宽 modify by lei.xue on 2017-5-25============
                var labelContentYellow = Type + ',' + Thinkness + ',' + ValidWidth + ',' + ValidWidth * Length / 1000 + ',' + Length + ',' + BeforeWeight + ',' + AfterWeight;
                //writeFile(labelContentUp, 'PackageUp');
                writeFile(labelContentDown, 'PackageDown', 'txt');
                writeFile(labelContentDate, 'PackageDate', 'txt');
                writeFile(labelContentYellow, 'PackageYellow', 'txt');

                var WshShell = new ActiveXObject("WScript.Shell");
                //var exeContentUp = "bartend.exe /F=\"C:\\BT32\\PackageUp.btw\" /D=\"C:\\BT32\\PackageUp.txt\" /P /x"
                var exeContentDown = "bartend.exe /F=\"C:\\BT32\\PackageDown.btw\" /D=\"C:\\BT32\\PackageDown.txt\" /P /x"
                var exeContentDate = "bartend.exe /F=\"C:\\BT32\\PackageDate.btw\" /D=\"C:\\BT32\\PackageData.txt\" /P /x"
                //modify by lei.xue on 2017-3-2 增加打印时间条码
                var exeContentYellow = "bartend.exe /F=\"C:\\BT32\\PackageYellow.btw\" /D=\"C:\\BT32\\PackageYellow.txt\" /P /x"

                //exeContent = "ping 192.168.1.55";
                //WshShell.run(exeContentUp);
                //modify by lei.xue on 2017-3-7 包装条码采用bat文件执行
                //modify by lei.xue on 2017-5-25 日期条码不需要打印
                //var batContent = exeContentDate + '\r\n' + exeContentDown + '\r\n' + exeContentYellow;
                var batContent = exeContentDown + '\r\n' + exeContentYellow;
                writeFile(batContent, 'usb', 'bat');
                WshShell.Run("C:/BT32/usb.bat", 0, true);
                alert("操作成功，生成标签：" + document.getElementById('<%=txtLot.ClientID %>').value);
                //刷新页面
                window.location.replace("../Material/LabelReprint.aspx");
                return false;
            }
            if (Type == '制造条码') {
                //标签内容增加 产品类型 add by lei.xue on 2017-5-31
                writeFile(labelContent + ',' + document.getElementById('<%=txtProductType.ClientID %>').value, 'FilmLabel', 'txt');

                var WshShell = new ActiveXObject("WScript.Shell");
                var exeContent = "bartend.exe /F=\"C:\\BT32\\FilmLabel.btw\" /D=\"C:\\BT32\\FilmLabel.txt\" /P /x"
                WshShell.run(exeContent);
                alert("操作成功，打印标签：" + document.getElementById('<%=txtLot.ClientID %>').value);
                //刷新页面
                window.location.replace("../Material/LabelReprint.aspx");
            }
            else {
                writeFile(labelContent, 'MouldLabel', 'txt');

                var WshShell = new ActiveXObject("WScript.Shell");
                var exeContent = "bartend.exe /F=\"C:\\BT32\\MouldLabel.btw\" /D=\"C:\\BT32\\MouldLabel.txt\" /P /x"
                WshShell.run(exeContent);
                alert("操作成功，打印标签：" + document.getElementById('<%=txtLot.ClientID %>').value);
                //刷新页面
                window.location.replace("../Material/LabelReprint.aspx");
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="submenu">
        <div id="right_menu">
            <div id="daohang">
                <img src="../image/home.png" />备料模块-补打SN条码<asp:Label ID="lblWorksiteID" runat="server" CssClass="hide"></asp:Label>
            </div>
            <!--end daohang-->
            <div id="button">
                <ul>
                    <li>
                        <asp:Button ID="btnSaveClose" runat="server" Text="打印"
                            CssClass="daohang_btn_saveclose" OnClientClick="" OnClick="btnSaveClose_Click" /></li>
                    <%-- <li>
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
            <legend>条码信息</legend>

            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">条码类型：</label>
                        <asp:DropDownList ID="ddlLabelType" runat="server" CssClass="txtinfo">
                            <asp:ListItem>模具条码</asp:ListItem>
                            <asp:ListItem>制造条码</asp:ListItem>
                            <asp:ListItem>包装条码</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">批次号：</label>
                        <asp:TextBox ID="txtLot" runat="server" CssClass="txtinfo"></asp:TextBox>
                        <asp:Button ID="btnEnter" runat="server" CssClass="hide" Text="Button" OnClick="btnEnter_Click" />
                        <asp:TextBox ID="txtProductType" runat="server" CssClass="hide"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <%--                <tr>
                    <td>
                        <label class="labelinfo">批次流程：</label>
                        <asp:Label ID="lblLotprocess" runat="server" CssClass="txtinfo" Text=""></asp:Label>
                    </td>
                </tr>--%>
            </table>
            <div>
                <table id="tbprocess" style="width: 1062px; margin-left: auto; margin-right: auto">
                    <tr>
                        <td style="text-align: left;">
                            <label class="labelinfo">批次流程：</label>
                            <asp:Label ID="lblLotprocess" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>

    </div>
    <div id="material">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>批次信息</legend>
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">前站卷材长度：</label>
                        <asp:TextBox ID="txtPreLength" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">前站幅宽规格：</label>
                        <asp:TextBox ID="txtPreWidth" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">等级：</label>
                        <asp:TextBox ID="txtLevel" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>

                    </td>
                </tr>
                <!--增加打印条码时间显示-->
                <tr>
                    <td>
                        <label class="labelinfo">UV成型条码时间：</label>
                        <asp:TextBox ID="txtUVCompleteDate" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">贴膜条码时间：</label>
                        <asp:TextBox ID="txtPasteFilmDate" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">分条条码时间：</label>
                        <asp:TextBox ID="txtSubsectionDate" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="labelinfo">品号：</label>
                        <asp:TextBox ID="txtPinHao" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>

                    </td>
                    <td>
                        <label class="labelinfo">有效幅宽规格：</label>
                        <asp:TextBox ID="txtValidWidth" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>

                    </td>
                    <td></td>
                </tr>

            </table>
        </fieldset>
    </div>
    <div id="workorder">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>工单信息</legend>
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">型号：</label>
                        <asp:TextBox ID="txtType" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">PET型号：</label>
                        <asp:TextBox ID="txtPETType" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">厚度：</label>
                        <asp:TextBox ID="txtThinkness" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="labelinfo">品名：</label>
                        <asp:TextBox ID="txtPinMin" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">装箱前净重：</label>
                        <asp:TextBox ID="txtBeforePackageWeight" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">装箱后毛重：</label>
                        <asp:TextBox ID="txtAfterPackageWeight" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                </tr>

            </table>
        </fieldset>
    </div>
</asp:Content>

