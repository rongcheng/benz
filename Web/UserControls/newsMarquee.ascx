<%@ Control Language="C#" AutoEventWireup="true" Codebehind="newsMarquee.ascx.cs"
    Inherits="WebUI.UserControls.newsMarquee" %>
<marquee direction="up" onmouseover="stop()" onmouseout="start()" behavior="scroll"
    scrolldelay="90" scrollamount="1" height="100%" width="100%">
    <ul>
        <asp:Repeater ID="newsMarqueeList" runat="server">
            <ItemTemplate>
                <li>
                    <%# GetTitle(Eval("Title").ToString(), Eval("newsId").ToString())%>
                    &nbsp;&nbsp;&nbsp;<%# DateTime.Parse(Eval("createDate").ToString()).ToShortDateString()%></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</marquee>
