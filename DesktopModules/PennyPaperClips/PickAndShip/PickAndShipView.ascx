<%@ Control Language="VB" AutoEventWireup="false" Inherits="PennyPaperClips.PickAndShip.PickAndShipView" CodeFile="PickAndShipView.ascx.vb" %>

<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<link href="../../../PpStyles/PennyPaperclips.css" rel="stylesheet" />
<div class="dnnForm dnnEdit dnnClear" id="dnnEdit">

    <div id="PickLineItems" class="ppDiv">
        <ul>
            <li>
                <h3>Select a line item to pick:</h3>
                <asp:ListBox ID="PickLineListBox" runat="server" AutoPostBack="True"></asp:ListBox>
            </li>
            <li>
                <label class="prompt" for="PickOrderIDLabel">OrderID:</label>
                <asp:Label ID="PickOrderIDLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="prompt" for="PickProductIDLabel">ProductID:</label>
                <asp:Label ID="PickProductIDLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="prompt" for="PickQuantityLabel">Pick Quantity:</label>
                <asp:Label ID="PickQuantityLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <h3>Select a bin to pick the item from:</h3>
                <asp:ListBox ID="BinsListBox" runat="server"></asp:ListBox>
            </li>
            <li>
                <label class="prompt" for="PickQuantityTextBox">How many will you take from this bin?</label>
                <asp:TextBox ID="PickQuantityTextBox" runat="server"></asp:TextBox>
            </li>
            <li>
                <asp:Button ID="PickItemButton" runat="server" Text="Pick Item(s)" />
                <asp:Label ID="PickWarningLabel" runat="server" ForeColor="Red"></asp:Label>
            </li>
        </ul>
    </div>
    <div id="ShipItems" class="ppDiv">
        <h3>The following items are ready to be shipped:</h3>
        <span>Select an item and prepare it for shipment. After the item has been shipped click the ship item button.</span>
        <ul>
            <li>
                <asp:ListBox ID="ShipLineListBox" runat="server" AutoPostBack="True"></asp:ListBox>
            </li>
            <li>
                <label class="prompt" for="ShipOrderIDLabel">OrderID:</label>
                <asp:Label ID="ShipOrderIDLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="prompt" for="ShipProductIDLabel">ProductID:</label>
                <asp:Label ID="ShipProductIDLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="prompt" for="ShipQuantityLabel">Shipment Quantity:</label>
                <asp:Label ID="ShipQuantityLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <h3>Shipping Information:</h3>
            </li>
            <li>
                <label class="prompt" for="FirstNameLabel">Name:</label>
                <asp:Label ID="FirstNameLabel" runat="server" Text=""></asp:Label>
                <asp:Label ID="LastNameLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="prompt" for="StreetLabel">Street Address:</label>
                <asp:Label ID="StreetLabel" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label class="prompt" for="CityLabel">City, State, Zip:</label>
                <asp:Label ID="CityLabel" runat="server" Text=""></asp:Label>
                <asp:Label ID="StateLabel" runat="server" Text=""></asp:Label>
                <asp:Label ID="ZipLabel" runat="server" Text=""></asp:Label>
            </li>
        </ul><br />
        <asp:Button ID="ShipItemButton" runat="server" Text="Ship Item" />
    </div>
</div>


