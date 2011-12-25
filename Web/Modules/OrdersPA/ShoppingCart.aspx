<%@ Page Language="C#" MasterPageFile="~/MPages/QJ_MasterPage.Master" AutoEventWireup="true"
    Codebehind="ShoppingCart.aspx.cs" Inherits="WebUI.Modules.Orders.ShoppingCart"
    Title="购物车" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>
        购物车
    </h3>
    <table align="center" style="width: 90%;">
        <tr>
            <td align="center">
                <asp:GridView ID="gvShoppingCartList" runat="server" AutoGenerateColumns="False"
                    Width="100%" EmptyDataText="对不起，购物车中还没有记录">
                    <Columns>
                        <asp:TemplateField HeaderText="序号">
                            <ItemTemplate>
                                <asp:Literal ID="Literal1" runat="server" Text="<%# gvShoppingCartList.Rows.Count + 1 %>"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="礼品编号">
                            <ItemTemplate>
                                <asp:Label ID="lblGiftId" runat="server" Text='<%# Bind("giftid") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="gifttitle" HeaderText="礼品标题" />
                        <asp:BoundField DataField="Quantity" HeaderText="现有数量" />
                        <asp:TemplateField HeaderText="订货数量">
                            <ItemTemplate>
                                <asp:RequiredFieldValidator runat="server" ID="noempty_ordernum" ControlToValidate="txtCount" ErrorMessage="请填数量"   Display="Dynamic" ></asp:RequiredFieldValidator> 
                                <asp:RegularExpressionValidator runat="server"  ID="val_ordernum"  ControlToValidate="txtCount"  ValidationExpression="^\d+$" ErrorMessage="输入有误!" Display="Dynamic" ></asp:RegularExpressionValidator>
                                <asp:TextBox ID="txtCount" runat="server" Text='<%# bind("giftcount") %>' Width="40px" ></asp:TextBox>
                                </ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="用途">
                            <ItemTemplate>
                                <asp:TextBox ID="txtUsage" runat="server" ></asp:TextBox>
                                </ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:HyperLink ID="hlDelete" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem,"giftid","javascript:deleteShoppingCart(\"{0}\");") %>'>删除</asp:HyperLink></ItemTemplate></asp:TemplateField></Columns></asp:GridView></td></tr><tr>
            <td style="height:20px"></td>
        </tr>      
        <tr>
            <td align="right" runat="server" id="order_bar" >
                <table>
                <tr>
                    <td>收货人: </td>
                    <td align="left" >
                        <asp:TextBox runat="server" ID="Contactor" ></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="noempty_Contactor" ControlToValidate="Contactor" ErrorMessage="请填收货人"   Display="Dynamic" ></asp:RequiredFieldValidator> 

                    </td>
                </tr>
                <tr>
                    <td>联系电话: </td>
                    <td align="left" >
                        <asp:TextBox runat="server" ID="Tel" ></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="noempty_Tel" ControlToValidate="Tel" ErrorMessage="请填联系电话"   Display="Dynamic" ></asp:RequiredFieldValidator> 

                    </td>
                </tr>
                <tr>
                    <td>邮箱: </td>
                    <td align="left" >
                        <asp:TextBox runat="server" ID="Email" ></asp:TextBox>
                        <asp:RequiredFieldValidator runat="server" ID="noempty_Email" ControlToValidate="Email" ErrorMessage="请填邮箱"   Display="Dynamic" ></asp:RequiredFieldValidator> 

                    </td>
                </tr>
                <tr>
                    <td>寄送地址: </td>
                    <td>
                        <asp:TextBox runat="server" ID="address" TextMode="MultiLine"  Rows="6" Columns="30" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:LinkButton ID="lnkCreateOrder" runat="server" OnClick="lnkCreateOrder_Click"  BorderStyle="Solid"  BorderWidth="1px"  >生成订单</asp:LinkButton>
                        <asp:LinkButton ID="lnkSubmitOrder" runat="server" OnClick="lnkSubmitOrder_Click" Visible="False">生成订单并提交</asp:LinkButton>
                    </td>
                </tr>
                
                </table>
                </td>
               </tr>
                <tr>
            <td style="color:Red;">
                <asp:Literal ID="lErrorInfo" runat="server"></asp:Literal></td></tr></table><script type="text/javascript">
                
                
        
        
        function deleteShoppingCart(giftId)
        {
            if(confirm("确定删除么？"))
            {                
              $.ajax({type: "GET", url: "<%= AppRootPath %>/Modules/CallbackExec.aspx",data: "fun=delcart&giftid="+giftId,
            success: function(msg){
            
                if(msg=="true")
                {
                    alert("删除成功");     
                    location.reload();         
                }
                else
                {
                    alert("删除失败");
                }
            }
        });
        
        
            }
        }
        
        
        
//        function deleteShoppingCart(giftId)
//        {
//            if(confirm("确定删除么？"))
//            {
//                WebAppPost("<%= AppRootPath %>/Modules/CallbackExec.aspx?fun=delcart&giftid="+giftId);
//            }
//        }
        
        function OnWebRequestCompleted(executor, eventArgs) 
        {
            if(executor.get_responseAvailable()) 
            {
                 if(executor.get_responseData()=="true")
                 {
                    alert('操作成功');
                    location.reload();
                 }
                 else
                 {
                    alert("操作失败:已存在或配额已满");
                 }

            }
            else
            {
                if (executor.get_timedOut())
                    alert("操作超时");
                else if (executor.get_aborted())
                    alert("请求已经终止");
                else
                    alert("添加失败");
            }
        }
    </script></asp:Content>