<%@ Control Language="C#" AutoEventWireup="true" Codebehind="CatalogMenu.ascx.cs"
    Inherits="WebUI.UserControls.CatalogMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="subnav"  id="subnav">
    <ul class="ul_nav1">
        <%=html %>
    </ul>
</div> 
<script language="javascript">

$(document).ready(function(){
	
	var menu_active = <%=MIndex%>;
	
	$('#subnav ul li').removeClass('on');
	$('#subnav ul li strong').eq(menu_active).parent().addClass('on');
	
	$('#subnav ul li strong').click( function(){
		
		if ( $(this).parent().hasClass('on') == false )   
		{
			$('#subnav ul li').removeClass('on');
			$(this).parent().addClass('on');
			$('#subnav ul ul').hide();
			$(this).next().slideDown();
		}
		else
		{
			$('#subnav ul li').removeClass('on');
			$(this).next().slideUp();
		}
	});
	
	
	
});

</script>