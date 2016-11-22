<%@ Control Language="VB" AutoEventWireup="false" Inherits="PennyPaperClips.StoreProducts.StoreProductsView" CodeFile="StoreProductsView.ascx.vb" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<link href="../../../PpStyles/PennyPaperclips.css" rel="stylesheet" />
<div class="dnnForm dnnEdit dnnClear" id="dnnEdit">
    <div id="productInfo" class="ppDiv">
        <ul>
            <li>
                <h3>Select an Available Product:</h3>
                <asp:ListBox ID="ProductsListBox" runat="server" AutoPostBack="True"></asp:ListBox>
            </li>
            <li>
                <label class="prompt" for="ProductIDLabel">Product ID:</label>
                <asp:Label ID="ProductIDLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="prompt" for="ProductNameLabel">Product Name:</label>
                <asp:Label ID="ProductNameLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="prompt" for="NumberOnHandLabel">On Hand:</label>
                <asp:Label ID="NumberOnHandLabel" runat="server" Text="0"></asp:Label>
            </li>
            <li>
                <label class="prompt" for="NumberOnHoldLabel">On Hold:</label>
                <asp:Label ID="NumberOnHoldLabel" runat="server" Text="0"></asp:Label>
            </li>
            <li>
                <label class="prompt" for="ProductIDLabel">Product Description:</label>
                <asp:Label ID="ProductDescriptionLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <p>&nbsp;</p>
                <asp:Button ID="AddProductButton" runat="server" Text="Add a New Product" />
                <span>This button will take you to Manage Products.</span>
            </li>
        </ul>
    </div>
    <div id="productStorage" class="ppDiv">
        <ul>
            <li class="ppDiv">
                <h3>The product you have selected is currently stored in the following bin(s):</h3>
                <asp:ListBox ID="CurrentBinsListBox" runat="server"></asp:ListBox>
            </li>
            <li>
                <asp:Label ID="SelectBinLabel" runat="server" Text="Select a bin to add or remove the selected product:"></asp:Label>
                <asp:DropDownList ID="BinsDropDownList" runat="server"></asp:DropDownList>
            </li>
            <li>
                <asp:Label ID="BinQuantityLabel" runat="server" Text="How many would you like to add or remove?"></asp:Label>
                <asp:TextBox ID="BinQuantityTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <span>&nbsp</span>
            <asp:Button ID="PutInBinButton" runat="server" Text="Put in Bin" />
                <span>&nbsp</span><span>&nbsp</span><span>&nbsp</span><span>&nbsp</span><span>&nbsp</span><span>&nbsp</span>
                <asp:Button ID="RemoveFromButton" runat="server" Text="Remove from Bin" />
                <span>&nbsp</span><span>&nbsp</span><span>&nbsp</span><span>&nbsp</span><span>&nbsp</span><span>&nbsp</span>
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" />
            </li>
            <asp:Label ID="warningLabel" runat="server" ForeColor="Red"></asp:Label>
        </ul>
    </div>

</div>


