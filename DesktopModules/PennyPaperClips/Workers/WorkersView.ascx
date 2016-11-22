<%@ Control Language="VB" AutoEventWireup="false" Inherits="PennyPaperClips.Workers.WorkersView" CodeFile="WorkersView.ascx.vb" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<link href="../../../PpStyles/PennyPaperclips.css" rel="stylesheet" />
<div class="dnnForm dnnEdit dnnClear" id="dnnEdit">
    <div class="ppDiv">
    <asp:Button ID="StoreProductsButton" runat="server" Text="Store Products" />
    <asp:Button ID="PickAndShipButton" runat="server" Text="Pick and Ship" />
    <asp:Button ID="ManageProductsButton" runat="server" Text="Manage Products" />
    </div>
</div>


