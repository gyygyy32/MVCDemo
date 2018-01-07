﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="WIPReport.aspx.cs" Inherits="Report_WIPReport" %>

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

        .tbhistory table {
            border: 1px;
            border-style: solid;
            border-collapse: collapse;
        }

        .tblothistory td {
            width: 120px;
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
                <img src="../image/home.png" />报表模块-WIP报表
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
                        <label class="labelinfo">查询方式：</label>
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="txtinfo">
                            <asp:ListItem>工单</asp:ListItem>
                            <asp:ListItem Value="型号">产品类型</asp:ListItem>
                        </asp:DropDownList>
                    </td>

                    <td>
                        <label class="labelinfo">Method：</label>
                        <asp:DropDownList ID="ddlMethod" runat="server" CssClass="txtinfo">
                            <asp:ListItem Value="面积">面积</asp:ListItem>
                            <asp:ListItem Value="卷数">卷数</asp:ListItem>
                        </asp:DropDownList>                       
                    </td>
                    <td></td>
                </tr>


            </table>
        </fieldset>

        <fieldset>
            <legend>过站信息</legend>

            <div align="center">

                <asp:GridView ID="grdWO" runat="server" Width="100%" CellPadding="4" CellSpacing="1" EnableModelValidation="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"  OnRowDataBound="grdWO_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="workorder" HeaderText="工单" />
                        <asp:BoundField DataField="mouldtype" HeaderText="Model" />
                        <asp:BoundField DataField="workorderwidth" HeaderText="工单宽度" />
                        <asp:BoundField DataField="workorderthinkness" HeaderText="工单厚度" />
                        <asp:BoundField DataField="ag涂布" HeaderText="AG涂布" />
                        <asp:BoundField DataField="ag涂布检验" HeaderText="AG涂布检验" />
                        <asp:BoundField DataField="uv背涂" HeaderText="UV背涂" />
                        <asp:BoundField DataField="uv背涂检验" HeaderText="UV背涂检验" />
                        <asp:BoundField DataField="uv成型" HeaderText="UV成型" />
                        <asp:BoundField DataField="uv成型检验" HeaderText="UV成型检验" />
                        <asp:BoundField DataField="贴膜" HeaderText="贴膜" />
                        <asp:BoundField DataField="贴膜检验" HeaderText="贴膜检验" />
                        <asp:BoundField DataField="分条" HeaderText="分条" />
                        <asp:BoundField DataField="分条检验" HeaderText="分条检验" />
                        <asp:BoundField DataField="包装" HeaderText="包装" />
                        <asp:BoundField DataField="合计" HeaderText="合计" />
                    </Columns>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
                <asp:GridView ID="grdModel" runat="server" Width="100%" CellPadding="4" CellSpacing="1" EnableModelValidation="True" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"  OnRowDataBound="grdModel_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <EditRowStyle BackColor="#999999" />
                    <EmptyDataTemplate>
                        
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundField DataField="mouldtype" HeaderText="Model" />
                        <asp:BoundField DataField="workorderwidth" HeaderText="工单宽度" />
                        <asp:BoundField DataField="workorderthinkness" HeaderText="工单厚度" />
                        <asp:BoundField DataField="ag涂布" HeaderText="AG涂布" />
                        <asp:BoundField DataField="ag涂布检验" HeaderText="AG涂布检验" />
                        <asp:BoundField DataField="uv背涂" HeaderText="UV背涂" />
                        <asp:BoundField DataField="uv背涂检验" HeaderText="UV背涂检验" />
                        <asp:BoundField DataField="uv成型" HeaderText="UV成型" />
                        <asp:BoundField DataField="uv成型检验" HeaderText="UV成型检验" />
                        <asp:BoundField DataField="贴膜" HeaderText="贴膜" />
                        <asp:BoundField DataField="贴膜检验" HeaderText="贴膜检验" />
                        <asp:BoundField DataField="分条" HeaderText="分条" />
                        <asp:BoundField DataField="分条检验" HeaderText="分条检验" />
                        <asp:BoundField DataField="包装" HeaderText="包装" />
                        <asp:BoundField DataField="合计" HeaderText="合计" />
                    </Columns>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </div>


        </fieldset>

    </div>
    <div>
    </div>
</asp:Content>

