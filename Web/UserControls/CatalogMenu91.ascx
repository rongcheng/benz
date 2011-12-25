<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CatalogMenu91.ascx.cs" 
Inherits="WebUI.UserControls.CatalogMenu91" %>
<script src="../UI/spryFramework/SpryAssets/SpryAccordion.js" type="text/javascript"></script>
<link href="../UI/spryFramework/SpryAssets/SpryAccordion.css" rel="stylesheet" type="text/css" />

<div id="Accordion1" class="Accordion" tabindex="0">
<%=html %>  
</div>



<script type="text/javascript">
var Accordion1 = new Spry.Widget.Accordion("Accordion1",{fixedPanelHeight: "230",defaultPanel:<%=MIndex%> });


</script>