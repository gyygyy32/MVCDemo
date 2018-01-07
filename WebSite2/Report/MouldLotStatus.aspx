﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="MouldLotStatus.aspx.cs" Inherits="Report_MouldLotStatus" %>




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

        .tbhistory table {
            border: 1px;
            border-style: solid;
            border-collapse: collapse;
        }

        .tblothistory td {
            width: 175px;
            text-align: center;
            font-family: 'Microsoft YaHei';
            font-size: 14px;
        }

        .tdtitle {
            font-weight: bolder;
            font-size: 16px;
        }

        .tblothistory {
            border-collapse: collapse;
            padding: 2px;
        }

        .trlothistory {
            border-top-style: solid;
            border-top-width: 2px;
            border-top-color: #000000;
        }

        .trlothistory01 {
            border-top-style: solid;
            border-bottom-style: solid;
            border-top-width: 2px;
            border-bottom-width: 2px;
        }
    </style>
    <script src="../JS/My97DatePickerBeta/My97DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">

        function CheckData() {
   
        }
        function loaddata01(content) {
            var div = document.getElementById('divcontent');
            div.innerHTML = content;
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="submenu">
        <div id="right_menu">
            <div id="daohang">
                <img src="../image/home.png" />报表模块-模具追溯
            </div>
            <!--end daohang-->
            <div id="button">
                <ul>
                    <li>
                        <asp:Button ID="btnSaveClose" runat="server" Text="查询"
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
    <div id="content">
        <fieldset style="border-color: #e8e8e8; border-style: solid; border-width: 1px 1px 1px 1px;">
            <legend>查询条件</legend>

            <table class="tb">
                <tr>
                    <td>
                        <label class="labelinfo">所在站点：</label>
                        <asp:DropDownList ID="ddlWorksite" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>

                    <td>
                        <label class="labelinfo">加工方式：</label>
                        <asp:DropDownList ID="DDLMouldStructure" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                    <td>
                        <label class="labelinfo">宽幅：</label>
                        <asp:TextBox ID="txtWidth" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td>
                        <label class="labelinfo">Pitch：</label>
                        <asp:DropDownList ID="ddlPitch" runat="server" CssClass="txtinfo"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label class="labelinfo">雾度：</label>
                        <asp:TextBox ID="txtHaze" runat="server" CssClass="txtinfo"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>


            </table>
        </fieldset>

        <fieldset>
            <legend>过站信息</legend>

            <div align="center">

                <asp:GridView ID="grd" runat="server" Width="100%" CellPadding="4" CellSpacing="1" EnableModelValidation="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="lotid" HeaderText="模具编号" />

                        <asp:BoundField DataField="批次号" HeaderText="批次号" />
                        <asp:BoundField DataField="镀层厚度" HeaderText="镀层厚度" />
                        <asp:BoundField DataField="加工方式" HeaderText="加工方式" />
                        <asp:BoundField DataField="宽幅" HeaderText="宽幅" />
                        <asp:BoundField DataField="Pitch" HeaderText="Pitch" />
                        <asp:BoundField DataField="雾度" HeaderText="雾度" />
                        <asp:BoundField DataField="worksitename" HeaderText="当前站点" />


                    </Columns>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>

            </div>

        </fieldset>

    </div>
    <div>
    </div>

</asp:Content>

