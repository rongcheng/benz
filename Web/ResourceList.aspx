<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MPages/QJ_MainPage.Master"
    EnableSessionState="True" CodeBehind="ResourceList.aspx.cs" Inherits="WebUI.ResourceList" %>

<%@ Register Src="/UserControls/KeywordsArea.ascx" TagName="Keywords" TagPrefix="uc1" %>
<%@ Register Src="/UserControls/Data_List.ascx" TagName="ResourceList" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main_frame">
        <div class="sousuo">
            <div class="sosuo">
                <div class="ssk">
                    <label>
                        <input type="text" class="ssks" name="textfield" id="textfield" />
                    </label>
                </div>
                <div class="sssp">
                    <label>
                        <input type="submit" class="ssp" name="button" id="button" value=" " />
                    </label>
                </div>
                <div class="sssg">
                    <label>
                        <input type="submit" name="button2" class="ssg" id="button2" value=" " />
                    </label>
                </div>
            </div>
        </div>
        <div class="sousuoa">&nbsp;
        </div>
        <div class="sub_frame_left">
            <uc1:Keywords ID="keywords1" runat="server"></uc1:Keywords>
        </div>
        <div class="sub_frame_right">
            <div class="sub_frame_right_a">
                <div class="sub_frame_right_1">
                    <a href="#">Founded</a> in 1968 in the United States and the
                </div>
                <div class="sub_frame_right_2">
                    appliance industry, involved in the logistics and other areas of large-scale comprehensive
                    modern enterprise group, which owns three listed companies, the four major industry
                    groups, is China's largest white goods production base and export base. In 1980,
                </div>
                <div class="sub_frame_right_3">
                    Founded</div>
            </div>
            <uc2:ResourceList ID="featureList" runat="server" ListType="Feature" Width="740px"
                ShowColumnCount="1" PageIndex="1" PageSize="6"></uc2:ResourceList>
        </div>
    </div>
</asp:Content>
