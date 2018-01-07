﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Package.aspx.cs" Inherits="Product_Package" %>

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
            width: 130px;
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
            Width: 200px;
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
        #rework {
            width: auto;
        }
    </style>
    <script src="../JS/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var PinHaoType = document.getElementById('<%=ddlPinHaoType.ClientID %>').value;
        })


        function EnterTextBox() {
            if (event.keyCode == 13 && document.getElementById("<%=txtLot.ClientID%>").value != "") {
                event.keyCode = 9;
                event.returnValue = false;
                document.getElementById("<%=btnEnter.ClientID %>").click();
            }
        }

        function CheckData() {
            if (document.getElementById("<%=lblLotprocess.ClientID%>").innerText == "" &&
                document.getElementById("<%=lblCurrnentWorksite.ClientID%>").innerText == "" &&
                document.getElementById("<%=lblEndProcess.ClientID%>").innerText == "") {
                alert("请先输入批次号！");
                return false;
            }
            //isNaN(splitLenght)
            if (isNaN(document.getElementById('<%=txtBeforePackageWeight.ClientID %>').value == "")) {
                alert("请输入装箱前净重");
                return false;
            }
            if (isNaN(document.getElementById('<%=txtAfterPackageWeight.ClientID %>').value == "")) {
                alert("请输入装箱后毛重");
                return false;
            }

            var PinHao = '';
            var PinHaoType = document.getElementById('<%=ddlPinHaoType.ClientID %>').value;
            if (PinHaoType == '成品') {
                PinHao = document.getElementById('<%=txtPinHaoFinished.ClientID %>').value;
            }
            else {
                PinHao = document.getElementById('<%=txtPinHaoSemifinished.ClientID %>').value;
            }
            if (PinHao == '') {
                alert('请先生成品号');
                return false;
            }

        }

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
            //var labelContent = document.getElementById('').value;
            //var content = '';
            //var boxqty = 0;
            //var qty = 0;

            //循环取明细表的值
            <%--var gv = document.getElementById('<%=grdSum.ClientID %>');
            //去掉表头行循环i从1开始
            for (var i = 1; i < gv.rows.length; i++) {
                for (var j = 0; j < gv.rows[i].cells.length; j++) {
                    content += gv.rows[i].cells[j].innerHTML + ',';
                    if (j == 2) {
                        boxqty++;
                    }
                    if (j == 3) {
                        qty += parseInt(gv.rows[i].cells[j].innerHTML);
                    }
                }
            }
            

            labelContent = labelContent + ',' + boxqty + ',' + qty + ',' + content.substr(0, content.length - 1);--%>
            //lot
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
            var PinHaoType = document.getElementById('<%=ddlPinHaoType.ClientID %>').value;
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


            var PinHao = '';
            if (PinHaoType == '成品') {
                PinHao = document.getElementById('<%=txtPinHaoFinished.ClientID %>').value;
            }
            else {
                PinHao = document.getElementById('<%=txtPinHaoSemifinished.ClientID %>').value;
            }
            //包装模板域顺序：品名，长度，厚度/1000，宽度，型号，厚度，PET型号，lot号，品号
            //var labelContent = PinMin + ',' + length + ',' + parseFloat(Thinkness) / 1000 + ',' + Width + ',' + Type + ',' + Thinkness + ',' + PETType + ',' + lot + ',' + PinHao;

            //modify by lei.xue on 2017-2-26 标签打印两张
            //packageup：品名，长度，厚度/1000，宽度，型号，厚度，PET型号
            //packagedown：lot号，长度，型号，厚度，宽度，品号，品名，PET型号
            //var labelContentUp = PinMin + ',' + length + ',' + parseFloat(Thinkness) / 1000 + ',' + Width + ',' + Type + ',' + Thinkness + ',' + PETType + ',' + Level;
            //var labelContentDown = lot + ',' + length + ',' + Type + ',' + Thinkness + ',' + Width + ',' + PinHao + ',' + PinMin + ',' + PETType + ',' + Level;
            //down标签修改模板modify by lei.xue on 2017-3-2
            //======================宽度改为有效幅宽 modify by lei.xue on 2017-5-22============
            //======================增加产品等级 modify by lei.xue on 2017-5-25============
            var labelContentDown = Type + ',' + Thinkness + ',' + ValidWidth + ',' + lot + ',' + PinHao + ',' + PinMin + ',' + Length + ',' + Level;

            var labelContentDate = lot + ',' + UVCompleteDate + ',' + PasteFilmDate + ',' + SubsectionDate;
            //add by lei.xue on 2017-3-7 增加黄标签
            //域顺序：型号，厚度，宽度，宽度*长度，长度，装箱前重，装箱后重
            //======================宽度改为有效幅宽 modify by lei.xue on 2017-5-22============
            var labelContentYellow = Type + ',' + Thinkness + ',' + ValidWidth + ',' + ValidWidth * Length / 1000 + ',' + Length + ',' + BeforeWeight + ',' + AfterWeight;
            //writeFile(labelContentUp, 'PackageUp');
            writeFile(labelContentDown, 'PackageDown', 'txt');
            writeFile(labelContentDate, 'PackageDate', 'txt');
            writeFile(labelContentYellow, 'PackageYellow', 'txt');
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
            var WshShell = new ActiveXObject("WScript.Shell");
            //var exeContentUp = "bartend.exe /F=\"C:\\BT32\\PackageUp.btw\" /D=\"C:\\BT32\\PackageUp.txt\" /P /x"
            var exeContentDown = "bartend.exe /F=\"C:\\BT32\\PackageDown.btw\" /D=\"C:\\BT32\\PackageDown.txt\" /P /x"
            var exeContentDate = "bartend.exe /F=\"C:\\BT32\\PackageDate.btw\" /D=\"C:\\BT32\\PackageDate.txt\" /P /x"
            //modify by lei.xue on 2017-3-2 增加打印时间条码
            var exeContentYellow = "bartend.exe /F=\"C:\\BT32\\PackageYellow.btw\" /D=\"C:\\BT32\\PackageYellow.txt\" /P /x"


            //exeContent = "ping 192.168.1.55";
            //WshShell.run(exeContentUp);
            //WshShell.run(exeContentDown);
            //WshShell.run(exeContentDate);
            //WshShell.run(exeContentYellow);
            //modify by lei.xue on 2017-3-7 包装条码采用bat文件执行
            //modify by lei.xue on 2017-5-25 日期条码不需要打印
            //var batContent = exeContentDate + '\r\n' + exeContentDown + '\r\n' + exeContentYellow;
            var batContent = exeContentDown + '\r\n' + exeContentYellow;

            writeFile(batContent, 'usb', 'bat');
            WshShell.Run("C:/BT32/usb.bat", 0, true);


            alert("操作成功，生成标签：" + document.getElementById('<%=txtLot.ClientID %>').value);
            //刷新页面
            window.location.replace("../Product/Package.aspx");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="submenu">
        <div id="right_menu">
            <div id="daohang">
                <img src="../image/home.png" />制造模块-包装
                <asp:Label ID="lblWorksiteID" runat="server" CssClass="hide">M60</asp:Label>
            </div>
            <!--end daohang-->
            <div id="button">
                <ul>
                    <li>
                        <asp:Button ID="btnSaveClose" runat="server" Text="生成"
                            CssClass="daohang_btn_saveclose" OnClick="btnSaveClose_Click" /></li>
                    <li>
                        <asp:Button ID="btnEdit" runat="server" Text="确定"
                            CssClass="daohang_btn_edit" OnClick="btnEdit_Click" OnClientClick=" return CheckData()" /></li>
                    <li>
                        <asp:Button ID="btnReset" runat="server" Text="测试"
                            CssClass="daohang_btn_reset" OnClick="btnReset_Click" /></li>
                    <%--<li>
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
                        <asp:DropDownList ID="ddlWorkshop" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">品号类型：</label>
                        <asp:DropDownList ID="ddlPinHaoType" runat="server" CssClass="txtinfo" OnTextChanged="ddlPinHaoType_TextChanged" AutoPostBack="True">
                            <asp:ListItem>成品</asp:ListItem>
                            <asp:ListItem>半成品</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

            </table>
            <div>
                <table style="width: 1211px; margin-left: auto; margin-right: auto">
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
                <!-------------标签宽度显示改为有效幅宽 modify by lei.xue on 2017-5-22------------------------->
                <tr>
                    <td>
                        <label class="labelinfo">有效幅宽：</label>
                        <asp:TextBox ID="txtValidWidth" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td></td>
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
                    <td></td>
                    <td></td>
                </tr>

            </table>
        </fieldset>
    </div>
    <div id="LabelInfo">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>标签信息</legend>
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">装箱前净重：</label>
                        <asp:TextBox ID="txtBeforePackageWeight" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">装箱后毛重：</label>
                        <asp:TextBox ID="txtAfterPackageWeight" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>

            </table>
        </fieldset>
    </div>
    <div>
        <asp:Panel ID="PanelFinished" runat="server" GroupingText="成品信息" Style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">客户代码：</label>
                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="txtinfo"></asp:DropDownList>

                    </td>

                    <td>
                        <label class="labelinfo">产品种类：</label>
                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">结构：</label>
                        <asp:DropDownList ID="ddlStructure" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="labelinfo">尺寸：</label>
                        <asp:DropDownList ID="ddlSize" runat="server" CssClass="txtinfo"></asp:DropDownList>

                    </td>

                    <td>
                        <label class="labelinfo">品号：</label>
                        <asp:TextBox ID="txtPinHaoFinished" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>

            </table>
        </asp:Panel>
    </div>

    <div>
        <asp:Panel ID="PanelSemifinished" runat="server" GroupingText="半成品信息" Style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">产品种类：</label>
                        <asp:DropDownList ID="ddlSemiProductType" runat="server" CssClass="txtinfo"></asp:DropDownList>

                    </td>

                    <td>
                        <label class="labelinfo">结构：</label>
                        <asp:DropDownList ID="ddlSemiStructure" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">制程：</label>
                        <asp:DropDownList ID="ddlProcess" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="labelinfo">厚度：</label>
                        <asp:DropDownList ID="ddlThinkness" runat="server" CssClass="txtinfo"></asp:DropDownList>

                    </td>

                    <td>
                        <label class="labelinfo">品号：</label>
                        <asp:TextBox ID="txtPinHaoSemifinished" runat="server" CssClass="txtinfo" BackColor="#e8e8e8"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>

            </table>
        </asp:Panel>
    </div>

</asp:Content>

