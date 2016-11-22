<%@ Control Language="VB" AutoEventWireup="false" Inherits="PennyPaperClips.Shop.ShopView" CodeFile="ShopView.ascx.vb" %>

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
                <label class="prompt" for="ProductIDLabel">Product Description:</label>
                <asp:Label ID="ProductDescriptionLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="prompt" for="NumberOnHandLabel">On Hand:</label>
                <asp:Label ID="NumberOnHandLabel" runat="server" Text="0"></asp:Label>
            </li>
            <li>
                <label class="prompt" for="PriceLabel">Price:</label>
                <asp:Label ID="PriceLabel" runat="server" Text="0"></asp:Label>
            </li>
            
            <li>
                <span>&nbsp</span>
            </li>
            <li>
                <label class="prompt" for="QuantityTextBox">Quantity:</label>
                <asp:TextBox ID="QuantityTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <span>&nbsp</span>
                <asp:Button ID="AddButton" runat="server" Text="Add to Cart" />
            </li>
        </ul>
            <asp:Label ID="loginWarningLabel" runat="server" ForeColor="Red"></asp:Label>
    </div>
    <div id="shoppingCart" class="ppDiv">
        <ul>
            <li>
                <h3>Your Shopping Cart:</h3>
                <asp:ListBox ID="CartListBox" runat="server" AutoPostBack="True"></asp:ListBox>
                <asp:Button ID="RemoveItemButton" runat="server" Text="Remove" />
                <asp:Button ID="EmptyCartButton" runat="server" Text="Empty Cart" />
                <asp:Label ID="CartWarningLabel" runat="server" ForeColor="Red"></asp:Label>
            </li>
            <li>
                <label class="prompt" for="subtotalLabel">Subtotal:</label>
                <asp:Label ID="subtotalLabel" runat="server" Text="0.00"></asp:Label>
            </li>
            <li>
                <label class="prompt" for="taxLabel">Tax:</label>
                <asp:Label ID="taxLabel" runat="server" Text="0.00"></asp:Label>
            </li>
            <li>
                <label class="prompt" for="shippingLabel">Shipping:</label>
                <asp:Label ID="shippingLabel" runat="server" Text="0.00"></asp:Label>
            </li>
            <li>
                <label class="prompt" for="totalLabel">Total:</label>
                <asp:Label ID="totalLabel" runat="server" Text="0.00"></asp:Label>
            </li>
        </ul>
    </div>
    <div id="customerInfo" class="ppDiv">
        <ul>
            <li>
                <h3>Shipping Information:</h3>
            </li>
            <li>
                <label class="prompt" for="FirstNameTextBox">First Name:</label>
                <asp:TextBox ID="FirstNameTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <label class="prompt" for="LastNameTextBox">Last Name:</label>
                <asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <label class="prompt" for="StreetTextBox">Street Address:</label>
                <asp:TextBox ID="StreetTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <label class="prompt" for="CityTextBox">City:</label>
                <asp:TextBox ID="CityTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <label class="prompt" for="StateDropDownList">State:</label>
                <asp:DropDownList ID="StateDropDownList" runat="server">
                    <asp:ListItem Value="AL">Alabama</asp:ListItem>
                    <asp:ListItem Value="AK">Alaska</asp:ListItem>
                    <asp:ListItem Value="AZ">Arizona</asp:ListItem>
                    <asp:ListItem Value="AR">Arkansas</asp:ListItem>
                    <asp:ListItem Value="CA">California</asp:ListItem>
                    <asp:ListItem Value="CO">Colorado</asp:ListItem>
                    <asp:ListItem Value="CT">Connecticut</asp:ListItem>
                    <asp:ListItem Value="DE">Delaware</asp:ListItem>
                    <asp:ListItem Value="FL">Florida</asp:ListItem>
                    <asp:ListItem Value="GA">Georgia</asp:ListItem>
                    <asp:ListItem Value="HI">Hawaii</asp:ListItem>
                    <asp:ListItem Value="ID">Idaho</asp:ListItem>
                    <asp:ListItem Value="IL">Illinois</asp:ListItem>
                    <asp:ListItem Value="IN">Indiana</asp:ListItem>
                    <asp:ListItem Value="IA">Iowa</asp:ListItem>
                    <asp:ListItem Value="KS">Kansas</asp:ListItem>
                    <asp:ListItem Value="KY">Kentucky</asp:ListItem>
                    <asp:ListItem Value="LA">Louisiana</asp:ListItem>
                    <asp:ListItem Value="ME">Maine</asp:ListItem>
                    <asp:ListItem Value="MD">Maryland</asp:ListItem>
                    <asp:ListItem Value="MA">Massachusetts</asp:ListItem>
                    <asp:ListItem Value="MI">Michigan</asp:ListItem>
                    <asp:ListItem Value="MN">Minnesota</asp:ListItem>
                    <asp:ListItem Value="MS">Mississippi</asp:ListItem>
                    <asp:ListItem Value="MO">Missouri</asp:ListItem>
                    <asp:ListItem Value="MT">Montana</asp:ListItem>
                    <asp:ListItem Value="NE">Nebraska</asp:ListItem>
                    <asp:ListItem Value="NV">Nevada</asp:ListItem>
                    <asp:ListItem Value="NH">New Hampshire</asp:ListItem>
                    <asp:ListItem Value="NJ">New Jersey</asp:ListItem>
                    <asp:ListItem Value="NM">New Mexico </asp:ListItem>
                    <asp:ListItem Value="NY">New York</asp:ListItem>
                    <asp:ListItem Value="NC">North Carolina</asp:ListItem>
                    <asp:ListItem Value="ND">North Dakota </asp:ListItem>
                    <asp:ListItem Value="OH">Ohio </asp:ListItem>
                    <asp:ListItem Value="OK">Oklahoma</asp:ListItem>
                    <asp:ListItem Value="OR">Oregon</asp:ListItem>
                    <asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
                    <asp:ListItem Value="RI">Rhode Island</asp:ListItem>
                    <asp:ListItem Value="SC">South Carolina</asp:ListItem>
                    <asp:ListItem Value="SD">South Dakota</asp:ListItem>
                    <asp:ListItem Value="TN">Tennessee</asp:ListItem>
                    <asp:ListItem Value="TX">Texas</asp:ListItem>
                    <asp:ListItem Value="UT">Utah</asp:ListItem>
                    <asp:ListItem Value="VT">Vermont</asp:ListItem>
                    <asp:ListItem Value="VA">Virginia </asp:ListItem>
                    <asp:ListItem Value="WA">Washington</asp:ListItem>
                    <asp:ListItem Value="WV">West Virginia</asp:ListItem>
                    <asp:ListItem Value="WI">Wisconsin</asp:ListItem>
                    <asp:ListItem Value="WY">Wyoming</asp:ListItem>
                </asp:DropDownList>
            </li>
            <li>
                <label class="prompt" for="ZipTextBox">Zip Code:</label>
                <asp:TextBox ID="ZipTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <asp:Button ID="SaveAddressButton" runat="server" Text="Save Address" />
                <asp:Label ID="AddressWarningLabel" runat="server" ForeColor="Red"></asp:Label>
            </li>
            <li>
                <span>&nbsp</span>
            </li>
            <li>
                <asp:Button ID="PlaceOrderButton" runat="server" Text="Place Order" />
                <asp:Button ID="CancelOrderButton" runat="server" Text="Cancel Order" />
            </li>
        </ul>
    </div>
</div>


