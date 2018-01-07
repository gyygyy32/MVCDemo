﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Report_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>产品追溯</title>
    <style type="text/css">
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
        <!--<script src="../JS/jquery-1.8.3.min.js"></script>-->
    <!--<script src="../Scripts/jquery-1.8.2.js"></script>-->
    <script src="../JS/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        function loaddata01(content) {
            var div = document.getElementById('divcontent');
            div.innerHTML = content;
        }
        function loaddata() {
            var div = document.getElementById('divcontent');
            var content = '       <table class="tblothistory" border="2px" bordercolor="#000000" cellspacing="0">' +
 '           <tr class="trlothistory01">' +
 '               <td class="tdtitle" style="font-weight: bolder; font-size: 16px;">型号</td>' +
 '               <td></td>' +
 '               <td class="tdtitle">厚度</td>' +
 '               <td></td>' +
 '               <td class="tdtitle">宽幅</td>' +
 '               <td></td>' +
 '               <td class="tdtitle">长度</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td rowspan="9" class="tdtitle">AG涂布/UV背涂</td>' +
 '           </tr>' +
 '           <tr class="trlothistory">' +
 '               <td>机台</td>' +
 '               <td></td>' +
 '               <td rowspan="8" class="tdtitle">AG涂布/UV背涂检验</td>' +
 '               <td>厚度</td>' +
 '               <td></td>' +
 '               <td>百格</td>' +
 '               <td></td>' +

 '           </tr>' +
 '           <tr>' +
 '               <td>模具</td>' +
 '               <td></td>' +
 '               <td>雾度</td>' +
 '               <td></td>' +
 '               <td>硬度</td>' +
 '               <td></td>' +

 '           </tr>' +
 '           <tr>' +
 '               <td>PET</td>' +
 '               <td></td>' +
 '               <td>透光率</td>' +
 '               <td></td>' +
 '               <td>时间</td>' +
 '               <td></td>' +

 '           </tr>' +
 '           <tr>' +
 '               <td>胶水</td>' +
 '               <td></td>' +
 '               <td>粒子密度</td>' +
 '               <td></td>' +
 '               <td>人员</td>' +
 '               <td></td>' +

 '           </tr>' +
 '           <tr>' +
 '               <td>宽幅(原始/有效)</td>' +
 '               <td></td>' +
 '               <td rowspan="2">点/线</td>' +

 '               <td colspan="3" rowspan="2"></td>' +

 '           </tr>' +
 '           <tr>' +
 '               <td>长度(原始/有效)</td>' +
 '               <td></td>' +

 '           </tr>' +
 '           <tr>' +
 '               <td>人员</td>' +
 '               <td></td>' +
 '               <td rowspan="2">纹路</td>' +
 '               <td colspan="3" rowspan="2"></td>' +

 '           </tr>' +
 '           <tr>' +
 '               <td>时间</td>' +
 '               <td></td>' +

 '           </tr>' +
 '           <tr class="trlothistory">' +
 '               <td rowspan="8" class="tdtitle">UV成型</td>' +
 '               <td>机台</td>' +
 '               <td></td>' +
 '               <td rowspan="8"class="tdtitle">成型检验</td>' +
 '               <td>厚度</td>' +
 '               <td></td>' +
 '               <td>百格</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>模具</td>' +
 '               <td></td>' +
 '               <td>辉度</td>' +
 '               <td></td>' +
 '               <td>dH</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>PET</td>' +
 '               <td></td>' +
 '               <td>耐磨</td>' +
 '               <td></td>' +
 '               <td>时间</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>胶水</td>' +
 '               <td></td>' +
 '               <td>翘曲</td>' +
 '               <td></td>' +
 '               <td>人员</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>宽幅(原始/有效)</td>' +
 '               <td></td>' +
 '               <td rowspan="2">点/线</td>' +
 '               <td colspan="3" rowspan="2"></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>长度(原始/有效)</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>人员</td>' +
 '               <td></td>' +
 '               <td rowspan="2">纹路</td>' +
 '               <td colspan="3" rowspan="2"></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>时间</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr class="trlothistory">' +
 '               <td rowspan="4"class="tdtitle">贴膜</td>' +
 '               <td>机台</td>' +
 '               <td></td>' +
 '               <td>宽幅</td>' +
 '               <td></td>' +
 '               <td rowspan="4"class="tdtitle">贴膜检验</td>' +
 '               <td>点线</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>正保</td>' +
 '               <td></td>' +
 '               <td>长度</td>' +
 '               <td></td>' +
 '               <td>纹路</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>背保</td>' +
 '               <td></td>' +
 '               <td>时间</td>' +
 '               <td></td>' +
 '               <td>人员</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr>' +
 '               <td>人员</td>' +
 '               <td></td>' +
 '               <td></td>' +
 '               <td></td>' +
 '               <td>时间</td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr class="trlothistory">' +
 '               <td rowspan="2"class="tdtitle">分条</td>' +
 '               <td>机台</td>' +
 '               <td></td>' +
 '               <td>宽幅</td>' +
 '               <td></td>' +
 '               <td>长度</td>' +
 '               <td></td>' +
 '               <td></td>' +
 '           </tr>' +

 '           <tr>' +
 '               <td>人员</td>' +
 '               <td></td>' +
 '               <td>时间</td>' +
 '               <td></td>' +
 '               <td></td>' +
 '               <td></td>' +
 '               <td></td>' +
 '           </tr>' +
 '           <tr class="trlothistory">' +
 '               <td class="tdtitle">包装</td>' +
 '               <td>人员</td>' +
 '               <td></td>' +
 '               <td>时间</td>' +
 '               <td></td>' +
 '               <td></td>' +
 '               <td></td>' +
 '               <td></td>' +
 '           </tr>' +
 '       </table>';
            div.innerHTML = content;
        }
        function getLotData() {
            var value = document.getElementById('txtLot').value;
            var senddata = { "lot": value };
            $.ajax({
                type: "GET", //访问WebService使用Post方式请求
                contentType: "application/json;charset=utf-8", //WebService 会返回Json类型
                url: "http://localhost:12726/Webservice/WebService.asmx?op=QueryLotHistory", //调用WebService
                data: senddata, //Email参数
                dataType: 'json',
                //beforeSend: function (x) { x.setRequestHeader("Content-Type", "application/json; charset=utf-8"); },
                success: function (response) { //回调函数，result，返回值
                    alert(response);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }
            });
        }
        function test()
        {
            $.ajax({
                type: "POST",
                url: "/Webservice/WebService.asmx/SayHelloJson",
                data: "{ firstName: 'Aidy', lastName: 'F' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var myData = JSON.parse(data.d); // data.d is a JSON formatted string, to turn it into a JSON object
                    // we use JSON.parse
                    // now that myData is a JSON object we can access its properties like normal
                    alert(myData.Greeting + " " + myData.Name);
                }
            });
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <input id="Submit1" type="submit" value="submit" onclick="test()" />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <asp:TextBox ID="txtLot" runat="server"></asp:TextBox>
        </div>
      <table class="tblothistory" border="2px" bordercolor="#000000" cellspacing="0">
        <tr class="trlothistory01">
            <td class="tdtitle" style="font-weight: bolder; font-size: 16px;">型号</td>
            <td></td>
            <td class="tdtitle">厚度</td>
            <td></td>
            <td class="tdtitle">宽幅</td>
            <td></td>
            <td class="tdtitle">长度</td>
            <td></td>
        </tr>
        <tr>
            <td rowspan="9" class="tdtitle">AG涂布/UV背涂</td>
        </tr>
        <tr class="trlothistory">
            <td>机台</td>
            <td>ag涂布机台</td>
            <td rowspan="8" class="tdtitle">AG涂布/UV背涂检验</td>
            <td>厚度</td>
            <td>ag检验厚度</td>
            <td>百格</td>
            <td>ag检验百格</td>
        </tr>
        <tr>
            <td>模具</td>
            <td>ag涂布模具</td>
            <td>雾度</td>
            <td>ag检验雾度</td>
            <td>硬度</td>
            <td>ag检验硬度</td>
        </tr>
        <tr>
            <td>PET</td>
            <td>ag涂布pet</td>
            <td>透光率</td>
            <td>ag检验透光率</td>
            <td>时间</td>
            <td>ag检验时间</td>
        </tr>
        <tr>
            <td>胶水</td>
            <td>ag涂布胶水</td>
            <td>粒子密度</td>
            <td></td>
            <td>人员</td>
            <td>ag检验人员</td>
        </tr>
        <tr>
            <td>宽幅(原始/有效)</td>
            <td>ag涂布宽幅</td>
            <td rowspan="2">点/线</td>
            <td colspan="3" rowspan="2">ag检验点线</td>
        </tr>
        <tr>
            <td>长度(原始/有效)</td>
            <td>ag涂布长度</td>
        </tr>
        <tr>
            <td>人员</td>
            <td>ag涂布人员</td>
            <td rowspan="2">纹路</td>
            <td colspan="3" rowspan="2">ag检验纹路</td>
        </tr>
        <tr>
            <td>时间</td>
            <td>ag涂布时间</td>
        </tr>
        <tr class="trlothistory">
            <td rowspan="8" class="tdtitle">UV成型</td>
            <td>机台</td>
            <td>uv成型机台</td>
            <td rowspan="8" class="tdtitle">成型检验</td>
            <td>厚度</td>
            <td>成型检验厚度</td>
            <td>百格</td>
            <td>成型检验百格</td>
        </tr>
        <tr>
            <td>模具</td>
            <td>uv成型模具</td>
            <td>辉度</td>
            <td>成型检验辉度</td>
            <td>dH</td>
            <td>成型检验dH</td>
        </tr>
        <tr>
            <td>PET</td>
            <td>uv成型pet</td>
            <td>耐磨</td>
            <td>成型检验耐磨</td>
            <td>时间</td>
            <td>成型检验时间</td>
        </tr>
        <tr>
            <td>胶水</td>
            <td>uv成型胶水</td>
            <td>翘曲</td>
            <td>成型检验翘曲</td>
            <td>人员</td>
            <td>成型检验人员</td>
        </tr>
        <tr>
            <td>宽幅(原始/有效)</td>
            <td>uv成型宽幅</td>
            <td rowspan="2">点/线</td>
            <td colspan="3" rowspan="2">成型检验点线</td>
        </tr>
        <tr>
            <td>长度(原始/有效)</td>
            <td>uv成型长度</td>
        </tr>
        <tr>
            <td>人员</td>
            <td>uv成型人员</td>
            <td rowspan="2">纹路</td>
            <td colspan="3" rowspan="2">成型检验纹路</td>
        </tr>
        <tr>
            <td>时间</td>
            <td>uv成型时间</td>
        </tr>
        <tr class="trlothistory">
            <td rowspan="4" class="tdtitle">贴膜</td>
            <td>机台</td>
            <td>贴膜机台</td>
            <td>宽幅</td>
            <td>贴膜宽幅</td>
            <td rowspan="4" class="tdtitle">贴膜检验</td>
            <td>点线</td>
            <td>贴膜检验检验</td>
        </tr>
        <tr>
            <td>正保</td>
            <td>贴膜正保</td>
            <td>长度</td>
            <td>贴膜长度</td>
            <td>纹路</td>
            <td>贴膜检验纹路</td>
        </tr>
        <tr>
            <td>背保</td>
            <td>贴膜背保</td>
            <td>时间</td>
            <td>贴膜时间</td>
            <td>人员</td>
            <td>贴膜检验人员</td>
        </tr>
        <tr>
            <td>人员</td>
            <td>贴膜人员</td>
            <td></td>
            <td></td>
            <td>时间</td>
            <td>贴膜检验时间</td>
        </tr>
        <tr class="trlothistory">
            <td rowspan="2" class="tdtitle">分条</td>
            <td>机台</td>
            <td>分条机台</td>
            <td>宽幅</td>
            <td>分条宽幅</td>
            <td>长度</td>
            <td>分条长度</td>
            <td></td>
        </tr>
        <tr>
            <td>人员</td>
            <td>分条人员</td>
            <td>时间</td>
            <td>分条时间</td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr class="trlothistory">
            <td class="tdtitle">包装</td>
            <td>人员</td>
            <td>包装人员</td>
            <td>时间</td>
            <td>包装时间</td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>
        <div id="divcontent"></div>

        
    </form>


</body>
</html>
