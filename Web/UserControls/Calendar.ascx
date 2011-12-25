<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="WebUI.UserControls.Calendar" %>
<input id="theDate" runat="server" style="width:80px" type="text" readonly="readonly" />
<img id="btn_<%=this.ClientID%>" alt="日历" name="btnDate" src="<%= AppRootPath%>/UI/Script/calendar/icon-day.gif" style="cursor:hand" />
    <script language="javascript" type="text/javascript">
                SetupJSCalendarLanguage('zh-CN');		//Set Language
                Calendar.setup({
                inputField     :    "<%=this.theDate.ClientID%>",	//Set Input Field
                button         :    "btn_<%=this.ClientID%>",	//assign Button
                ifFormat       :    "%Y-%m-%d",			//set date format
                align          :    "Bl",
                singleClick    :    true,           
                weekNumbers    :    false,			
                firstDay       :    0,           
                showsTime      :    false,			
                timeFormat     :    "12"			
            });
    </script> 