#Region "Copyright"

' 
' Copyright (c) 2014
' by PennyPaperClips
' 

#End Region

#Region "Using Statements"

Imports System
Imports DotNetNuke.Entities.Modules

#End Region

Namespace PennyPaperClips.Shop

    Public Partial Class ShopView
        Inherits PortalModuleBase

        Private order As Order
        Private userNum As Integer

        #Region "Event Handlers"

        Protected Overrides Sub OnInit(e As EventArgs)
            MyBase.OnInit(e)
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            If Not Page.IsPostBack Then
                DisplayProductList()
                AddButton.Enabled = False
                RemoveItemButton.Enabled = False
                EmptyCartButton.Enabled = False

                If UserId > -1 Then
                    order = order.GetOrderForSession()
                    DisplayCart()
                    DisplayAddress()
                End If
            End If
        End Sub

        Private Sub DisplayProductList()
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim productList =
                   (From products In inventoryContext.Products
                    Order By products.ProductID).ToList
            ProductsListBox.DataSource = productList
            ProductsListBox.DataTextField = "Description"
            ProductsListBox.DataValueField = "ProductID"
            ProductsListBox.DataBind()
        End Sub

        Protected Sub ProductsListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ProductsListBox.SelectedIndexChanged
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim selectedProduct As Product =
            (From products In inventoryContext.Products
             Where products.ProductID = ProductsListBox.SelectedValue).First

            ProductIDLabel.Text = selectedProduct.ProductID.ToString()
            ProductNameLabel.Text = selectedProduct.ProductName.ToString()
            ProductDescriptionLabel.Text = selectedProduct.ProductDescription.ToString()
            If selectedProduct.OnHand = 0 Then
                NumberOnHandLabel.Text = "Sorry, Sold Out"
                NumberOnHandLabel.ForeColor = Color.Red
                AddButton.Enabled = False
            Else
                NumberOnHandLabel.Text = selectedProduct.OnHand.ToString()
                NumberOnHandLabel.ForeColor = Color.Black
                AddButton.Enabled = True
            End If
            PriceLabel.Text = selectedProduct.UnitPrice.ToString("c2")
        End Sub

        Protected Sub AddButton_Click(sender As Object, e As EventArgs) Handles AddButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Me.userNum = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo.UserID

            'Get selected Product
            Dim selectedProduct As Product =
            (From products In inventoryContext.Products
             Where products.ProductID = ProductsListBox.SelectedValue).First

            If QuantityTextBox.Text = String.Empty Then
                loginWarningLabel.Text = "Please type a quantity to add to cart."
                QuantityTextBox.Focus()
            Else
                If Integer.Parse(QuantityTextBox.Text) > selectedProduct.OnHand Then
                    loginWarningLabel.Text = "I'm sorry, we do not have that quantity in stock."
                Else
                    If UserId > -1 Then
                        order = order.GetOrderForSession()


                        Dim lineItem As OrderLineItem = inventoryContext.OrderLineItems.Create()
                        lineItem.OrderID = order.OrderID
                        lineItem.ProductID = selectedProduct.ProductID
                        lineItem.Quantity = Integer.Parse(QuantityTextBox.Text)
                        lineItem.UnitPrice = selectedProduct.UnitPrice
                        lineItem.IsHeld = False
                        lineItem.IsPicked = False
                        lineItem.IsShipped = False


                        inventoryContext.OrderLineItems.Add(lineItem)
                        inventoryContext.SaveChanges()


                        Me.DisplayCart()
                        loginWarningLabel.Text = ""

                        DisplayAddress()
                    Else
                        loginWarningLabel.Text = "Please log in to order."
                    End If
                End If
            End If
        End Sub

        Protected Sub EmptyCartButton_Click(sender As Object, e As EventArgs) Handles EmptyCartButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            order = order.GetOrderForSession()
            order.OrderLineItems.Clear()
            inventoryContext.SaveChanges()
            EmptyCartButton.Enabled = False
            RemoveItemButton.Enabled = False
            Me.DisplayCart()
        End Sub

        Private Sub DisplayCart()
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            CartListBox.Items.Clear()
            CartListBox.DataSource = order.OrderLineItems
            CartListBox.DataTextField = "Description"
            CartListBox.DataBind()

            Dim subtotal As Decimal = 0
            Dim tax As Decimal = 0
            Dim shipping As Decimal = 2.5
            Dim total As Decimal = 0
            Dim lineItemList =
                (From orderLineItems In inventoryContext.OrderLineItems
                 Where orderLineItems.OrderID = order.OrderID).ToList

            If order.OrderLineItems.Count > 0 Then
                For Each item In lineItemList
                    subtotal += (item.Quantity * item.UnitPrice)
                    shipping += (item.Quantity * 0.001)
                Next
                tax = subtotal * 0.05
                total = subtotal + tax + shipping
                RemoveItemButton.Enabled = True
                EmptyCartButton.Enabled = True
            End If

            subtotalLabel.Text = subtotal.ToString("c2")
            taxLabel.Text = tax.ToString("n2")
            shippingLabel.Text = shipping.ToString("n2")
            totalLabel.Text = total.ToString("n2")
        End Sub

        Protected Sub RemoveItemButton_Click(sender As Object, e As EventArgs) Handles RemoveItemButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            order = order.GetOrderForSession()
            If order.OrderLineItems.Count > 0 Then
                Dim lineItem = order.OrderLineItems(CartListBox.SelectedIndex)
                inventoryContext.OrderLineItems.Remove(lineItem)
                inventoryContext.SaveChanges()
                EmptyCartButton.Enabled = False
                RemoveItemButton.Enabled = False
                Me.DisplayCart()
            Else
                CartWarningLabel.Text = "Please select the item you want to remove."
            End If
        End Sub

        Protected Sub SaveAddressButton_Click(sender As Object, e As EventArgs) Handles SaveAddressButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Me.userNum = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo.UserID
            Dim customer =
                (From customers In inventoryContext.Customers
                 Where customers.UserID = userNum).First
            If UserId > -1 Then
                Dim userInfo = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo
                userInfo.PortalID = Me.PortalId
                userInfo.Profile.FirstName = FirstNameTextBox.Text.ToString
                userInfo.Profile.LastName = LastNameTextBox.Text.ToString
                DotNetNuke.Entities.Users.UserController.UpdateUser(Me.PortalId, userInfo)

                customer.Street = StreetTextBox.Text.ToString
                customer.City = CityTextBox.Text.ToString
                customer.StateCode = StateDropDownList.SelectedValue
                customer.PostalCode = ZipTextBox.Text.ToString
                inventoryContext.SaveChanges()
            Else
                AddressWarningLabel.Text = "Please log in to save your address."
            End If
        End Sub

        Private Sub DisplayAddress()
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Me.userNum = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo.UserID
            Dim userInfo = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo
            If userNum > -1 Then
                Dim currentUser =
                            (From customers In inventoryContext.Customers
                             Where customers.UserID = userNum).First

                FirstNameTextBox.Text = userInfo.Profile.FirstName.ToString
                LastNameTextBox.Text = userInfo.Profile.LastName.ToString

                If currentUser.Street IsNot Nothing Then
                    StreetTextBox.Text = currentUser.Street.ToString
                End If
                If currentUser.City IsNot Nothing Then
                    CityTextBox.Text = currentUser.City.ToString
                End If
                If currentUser.StateCode IsNot Nothing Then
                    StateDropDownList.SelectedValue = currentUser.StateCode.ToString
                End If
                If currentUser.PostalCode IsNot Nothing Then
                    ZipTextBox.Text = currentUser.PostalCode.ToString
                End If
            End If
        End Sub

        Protected Sub PlaceOrderButton_Click(sender As Object, e As EventArgs) Handles PlaceOrderButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            order = order.GetOrderForSession()
            Me.userNum = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo.UserID
            Dim userInfo = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo
            Dim currentUser =
                           (From customers In inventoryContext.Customers
                            Where customers.UserID = userNum).First
            If currentUser.Street IsNot Nothing Then
                If currentUser.City IsNot Nothing Then
                    If currentUser.StateCode IsNot Nothing Then
                        If currentUser.PostalCode IsNot Nothing Then
                            Dim lineItemList =
                                            (From orderLineItems In inventoryContext.OrderLineItems
                                            Where orderLineItems.OrderID = order.OrderID).ToList

                            If order.OrderLineItems.Count > 0 Then
                                For Each item In lineItemList
                                    item.IsHeld = True
                                    Dim lineProduct =
                                        (From products In inventoryContext.Products
                                         Where products.ProductID = item.ProductID).First
                                    lineProduct.OnHand -= item.Quantity
                                Next
                                inventoryContext.SaveChanges()
                                CartListBox.Items.Clear()
                                userNum = -1
                                Dim tc As New TabController()
                                Dim ti As DotNetNuke.Entities.Tabs.TabInfo = tc.GetTabByName("Home", 0)
                                Dim pageUrl As String = DotNetNuke.Common.Globals.NavigateURL(ti.TabID)
                                Response.Redirect(pageUrl, False)
                                Context.ApplicationInstance.CompleteRequest()
                            End If
                        Else
                            AddressWarningLabel.Text = "Please save an address before placing your order."
                        End If
                    Else
                        AddressWarningLabel.Text = "Please save an address before placing your order."
                    End If
                Else
                    AddressWarningLabel.Text = "Please save an address before placing your order."
                End If
            Else
                AddressWarningLabel.Text = "Please save an address before placing your order."
            End If
        End Sub

        Protected Sub CancelOrderButton_Click(sender As Object, e As EventArgs) Handles CancelOrderButton.Click
            EmptyCartButton_Click(Nothing, Nothing)
            Dim tc As New TabController()
            Dim ti As DotNetNuke.Entities.Tabs.TabInfo = tc.GetTabByName("Home", 0)
            Dim pageUrl As String = DotNetNuke.Common.Globals.NavigateURL(ti.TabID)
            Response.Redirect(pageUrl, False)
            Context.ApplicationInstance.CompleteRequest()
        End Sub
#End Region
    End Class

End Namespace

