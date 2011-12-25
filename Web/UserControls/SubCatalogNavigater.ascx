<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubCatalogNavigater.ascx.cs"
    Inherits="WebUI.UserControls.SubCatalogNavigater" %>
<link rel="stylesheet" href="/UI/css/zTreeStyle.css" type="text/css" />
<script type="text/javascript" src="/UI/Script/ztree/jquery.ztree.core-3.0.js"></script>
<div class="zTreeDemoBackground left">
    <ul id="treeDemo" class="ztree">
    </ul>
</div>
<script language="JavaScript" type="text/javascript">
		<!--
    var setting = {
        view: {
            showLine: false,
            showIcon: false
        },
        data: {
            simpleData: {
                enable: true,
                rootPId: ""
            }
        }
    };


    $(document).ready(function () {
        var url = "/Handlers/CatalogsHandler.ashx";

        $.getJSON(url,
                {
                    action: "allsub",
                    rootId: $.query.get("rootid"),
                    catalogId: $.query.get("catalogid")
                },
                function (data) {
                    $.fn.zTree.init($("#treeDemo"), setting, data);
                });
    });
		//-->
	</script>
