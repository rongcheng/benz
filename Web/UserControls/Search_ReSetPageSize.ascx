<%@ Control Language="C#" AutoEventWireup="true" Codebehind="Search_ReSetPageSize.ascx.cs"
    Inherits="WebUI.UserControls.Search_ReSetPageSize" %>
<div>
    <p class="showPic">
        <img src="/images/wi.jpg" alt="" /><b> 显 示 设 置 </b></p>
    <select id="SelectPageSize" class="inputstyle" runat="server">
        <option selected="selected" value="20">每页显示20条</option>
        <option value="40">每页显示40条</option>
        <option value="60">每页显示60条</option>
        <option value="80">每页显示80条</option>
        <option value="100">每页显示100条</option>
    </select>
    <select id="SelectResourceType" class="inputstyle" runat="server">
        <option selected="selected" value="">所有类型</option>        
        <option value="image">图片</option>
        <option value="video">视频</option>
        <option value="document">电子文档</option>
        <option value="other">其它</option>
    </select>
    <input type="image" style="vertical-align: bottom" onclick="GetSrch();" src="/images/btn_format.gif"
        id="Image1" />
    <input name="isChangePageSize" value="" type="hidden" id="hidden_isChangePageSize"
        runat="server" />
    <input name="searchResourceType" value="" type="hidden" id="hidden_searchResourceType"
        runat="server" />
</div>

<script language="javascript" type="text/javascript">
    function GetSrch()
    {        
        var si = document.getElementById('<%=SelectPageSize.ClientID %>').value;             
        SetCookie('QJpageCount',si);//
        document.getElementById('<%=hidden_isChangePageSize.ClientID %>').value = "1";     
    }    
        //给Cookie赋值
     function SetCookie(cookieName, cookieData)
    {
        var expires = new Date ();
        expires.setTime(expires.getTime() + 365 * (24 * 60 * 60 * 1000));   
        document.cookie = cookieName + "=" + escape(cookieData) + ";path=/;expires=" + expires.toGMTString();
    }
    function GetCookie(name)
    {
        var dc = document.cookie;
        var prefix = name + "=";
        var begin = dc.indexOf("; " + prefix);
        if (begin == -1)
        {
            begin = dc.indexOf(prefix);
            if (begin != 0) return null;
        }
        else
        {
            begin += 2;
        }
        var end = document.cookie.indexOf(";", begin);
        if (end == -1)
        {
            end = dc.length;
        }
        return unescape(dc.substring(begin + prefix.length, end));
    } 
</script>