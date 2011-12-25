<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SystemMenu.ascx.cs" Inherits="WebUI.UserControls.SystemMenu" %>

<script src="/UI/spryFramework/SpryAssets/SpryAccordion.js" type="text/javascript"></script>
<link href="/UI/spryFramework/SpryAssets/SpryAccordion.css" rel="stylesheet" type="text/css" />
<link href="/common/common_menu.css" rel="stylesheet" type="text/css" />
<div id="Accordion1" class="Accordion" tabindex="0">
<%=html %>  
</div>
<script type="text/javascript">
var Accordion1 = new Spry.Widget.Accordion("Accordion1",{useFixedPanelHeights: true,defaultPanel:<%=MIndex%> });
</script>