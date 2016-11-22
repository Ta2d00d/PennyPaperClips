<%@ Control Language="VB" AutoEventWireup="false" Inherits="PennyPaperClips.ManageProducts.ManageProductsView" CodeFile="ManageProductsView.ascx.vb" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<link href="../../../PpStyles/PennyPaperclips.css" rel="stylesheet" />
<div class="dnnForm dnnEdit dnnClear" id="dnnEdit">

     <div id="productInfo" class="ppDiv">
         <div>
        <h3>Available Products:</h3>
       <asp:ListBox ID="ProductsListBox" runat="server" AutoPostBack="True"></asp:ListBox>
        </div>
             <ul>
            <li>
                <label class="prompt" for="ProductIDTextBox">Product ID:</label>
                <asp:TextBox ID="ProductIDTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <label class="prompt" for="ProductNameTextBox">Name:</label>
                <asp:TextBox ID="ProductNameTextBox" runat="server" Width="200px"></asp:TextBox>
            </li>
            <li>
                <label class="prompt" for="ProductDescriptionTextBox">Product Description:</label>
                <asp:TextBox ID="ProductDescriptionTextBox" runat="server" Width="800px"></asp:TextBox>
            </li>
            <li>
                <label class="prompt" for="UnitPriceTextBox">Unit Price:</label>
                <asp:TextBox ID="UnitPriceTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <label class="prompt" for="NumberOnHandLabel">On Hand:</label>
                <asp:Label ID="NumberOnHandLabel" runat="server" Text="0"></asp:Label>
            </li>
            <li>
                <label class="prompt" for="UPCTextBox">UPC:</label>
                <asp:TextBox ID="UPCTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <asp:Button ID="UpdateProductButton" runat="server" Text="Update Product Info" />
                <asp:Button ID="AddProductButton" runat="server" Text="Add as New Product" />
                <span>&nbsp</span><span>&nbsp</span><span>&nbsp</span><span>&nbsp</span><span>&nbsp</span><span>&nbsp</span>
                <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
            </li>
        </ul>
         <asp:Label ID="warningLabel" runat="server" ForeColor="Red"></asp:Label>
    </div>

</div>


