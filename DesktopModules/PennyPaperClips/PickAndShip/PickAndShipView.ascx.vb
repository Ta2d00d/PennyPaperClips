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

Namespace PennyPaperClips.PickAndShip

    Public Partial Class PickAndShipView
        Inherits PortalModuleBase

        #Region "Event Handlers"

        Protected Overrides Sub OnInit(e As EventArgs)
            MyBase.OnInit(e)
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            If Not Page.IsPostBack Then
                DisplayPickList()
                DisplayShipList()
            End If
        End Sub

        Protected Sub PickLineListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PickLineListBox.SelectedIndexChanged
            DisplayBinsForProduct()
            DisplayLineItemLabels()
        End Sub

        Private Sub DisplayPickList()
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim pickList =
                  (From lineItems In inventoryContext.OrderLineItems
                   Order By lineItems.OrderID
                   Where lineItems.IsHeld = True AndAlso lineItems.IsPicked = False).ToList
            PickLineListBox.DataSource = pickList
            PickLineListBox.DataTextField = "PickDescription"
            PickLineListBox.DataValueField = "PickValue"
            PickLineListBox.DataBind()
        End Sub

        Private Sub DisplayBinsForProduct()
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim splitLineItem() As String = PickLineListBox.SelectedValue.Split("~")
            Dim selectedOrderId = Integer.Parse(splitLineItem(0))
            Dim selectedProductID = Integer.Parse(splitLineItem(1))
            Dim selectedProduct As Product =
            (From products In inventoryContext.Products
             Where products.ProductID = selectedProductID).First
            Dim productLocations =
                (From storedProduct In inventoryContext.StoredProducts
                    Where storedProduct.ProductID = selectedProduct.ProductID).ToList

            BinsListBox.DataSource = productLocations
            BinsListBox.DataTextField = "Description"
            BinsListBox.DataValueField = "BinID"
            BinsListBox.DataBind()
        End Sub

        Protected Sub PickItemButton_Click(sender As Object, e As EventArgs) Handles PickItemButton.Click
            If BinsListBox.SelectedIndex > -1 Then
                Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
                Dim splitLineItem() As String = PickLineListBox.SelectedValue.Split("~")
                Dim selectedOrderId = Integer.Parse(splitLineItem(0))
                Dim selectedProductID = Integer.Parse(splitLineItem(1))
                Dim selectedStoredProduct As StoredProduct =
                    (From storedProducts In inventoryContext.StoredProducts
                     Where storedProducts.ProductID = selectedProductID AndAlso storedProducts.BinID = BinsListBox.SelectedValue).First
                Dim selectedLineItem As OrderLineItem =
                    (From lineItems In inventoryContext.OrderLineItems
                     Where lineItems.OrderID = selectedOrderId AndAlso lineItems.ProductID = selectedProductID).First
                Dim quantity = Integer.Parse(PickQuantityTextBox.Text)
                Dim pickQuantity = Integer.Parse(PickQuantityLabel.Text)
                If quantity < pickQuantity Then
                    If quantity = selectedStoredProduct.Quantity Then
                        If selectedLineItem.PickedQuantity IsNot Nothing Then
                            selectedLineItem.PickedQuantity += quantity
                        Else
                            selectedLineItem.PickedQuantity = quantity
                        End If
                        inventoryContext.StoredProducts.Remove(selectedStoredProduct)
                        inventoryContext.SaveChanges()
                        DisplayBinsForProduct()
                        PickWarningLabel.Text = ""
                    End If
                    If quantity < selectedStoredProduct.Quantity Then
                        If selectedLineItem.PickedQuantity IsNot Nothing Then
                            selectedLineItem.PickedQuantity += quantity
                        Else
                            selectedLineItem.PickedQuantity = quantity
                        End If
                        selectedStoredProduct.Quantity -= quantity
                        inventoryContext.SaveChanges()
                        DisplayBinsForProduct()
                        PickWarningLabel.Text = ""
                    End If
                    DisplayLineItemLabels()
                End If
                If quantity = pickQuantity Then
                    If quantity = selectedStoredProduct.Quantity Then
                        If selectedLineItem.PickedQuantity IsNot Nothing Then
                            selectedLineItem.PickedQuantity += quantity
                        Else
                            selectedLineItem.PickedQuantity = quantity
                        End If
                        inventoryContext.StoredProducts.Remove(selectedStoredProduct)
                        selectedLineItem.IsHeld = False
                        selectedLineItem.IsPicked = True
                        inventoryContext.SaveChanges()
                        DisplayPickList()
                        DisplayShipList()
                        ClearPickFields()
                    End If
                    If quantity < selectedStoredProduct.Quantity Then
                        If selectedLineItem.PickedQuantity IsNot Nothing Then
                            selectedLineItem.PickedQuantity += quantity
                        Else
                            selectedLineItem.PickedQuantity = quantity
                        End If
                        selectedStoredProduct.Quantity -= quantity
                        selectedLineItem.IsHeld = False
                        selectedLineItem.IsPicked = True
                        inventoryContext.SaveChanges()
                        DisplayPickList()
                        DisplayLineItemLabels()
                        DisplayShipList()
                        ClearPickFields()
                    End If
                End If
                If quantity > selectedStoredProduct.Quantity Then
                    PickWarningLabel.Text = "You can not remove a greater quantity than is stored in the selected bin."
                    PickQuantityTextBox.Focus()
                End If
                If quantity > pickQuantity Then
                    PickWarningLabel.Text = "This is more of the item than is required for the order."
                    PickQuantityTextBox.Focus()
                End If
            Else
                PickWarningLabel.Text = "Please select a bin."
            End If
        End Sub

        Private Sub DisplayLineItemLabels()
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            If PickLineListBox.SelectedIndex > -1 Then
                Dim splitLineItem() As String = PickLineListBox.SelectedValue.Split("~")
                Dim selectedOrderId = Integer.Parse(splitLineItem(0))
                Dim selectedProductID = Integer.Parse(splitLineItem(1))
                Dim pickQuantity As Integer
                Dim selectedLineItem As OrderLineItem =
                    (From lineItems In inventoryContext.OrderLineItems
                     Where lineItems.OrderID = selectedOrderId AndAlso lineItems.ProductID = selectedProductID).First
                If selectedLineItem.PickedQuantity IsNot Nothing Then
                    pickQuantity = selectedLineItem.Quantity - selectedLineItem.PickedQuantity
                Else
                    pickQuantity = selectedLineItem.Quantity
                End If
                PickOrderIDLabel.Text = selectedOrderId.ToString()
                PickProductIDLabel.Text = selectedProductID.ToString()
                PickQuantityLabel.Text = pickQuantity.ToString()
            Else
                PickOrderIDLabel.Text = ""
                PickProductIDLabel.Text = ""
                PickQuantityLabel.Text = ""
            End If
        End Sub

        Private Sub ClearPickFields()
            PickOrderIDLabel.Text = ""
            PickProductIDLabel.Text = ""
            PickQuantityLabel.Text = ""
            BinsListBox.Items.Clear()
            PickQuantityTextBox.Text = ""
            PickWarningLabel.Text = ""
        End Sub

        Private Sub DisplayShipList()
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim shipList =
                  (From lineItems In inventoryContext.OrderLineItems
                   Order By lineItems.OrderID
                   Where lineItems.IsPicked = True AndAlso lineItems.IsShipped = False).ToList
            ShipLineListBox.DataSource = shipList
            ShipLineListBox.DataTextField = "PickDescription"
            ShipLineListBox.DataValueField = "ShipValue"
            ShipLineListBox.DataBind()
        End Sub

        Protected Sub ShipLineListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ShipLineListBox.SelectedIndexChanged
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim splitLineItem() As String = ShipLineListBox.SelectedValue.Split("~")
            Dim selectedOrderId = Integer.Parse(splitLineItem(0))
            Dim selectedProductID = Integer.Parse(splitLineItem(1))
            Dim selectedLineItem As OrderLineItem =
                (From lineItems In inventoryContext.OrderLineItems
                 Where lineItems.OrderID = selectedOrderId AndAlso lineItems.ProductID = selectedProductID).First

            ShipOrderIDLabel.Text = selectedLineItem.OrderID.ToString()
            ShipProductIDLabel.Text = selectedLineItem.ProductID.ToString()
            ShipQuantityLabel.Text = selectedLineItem.Quantity.ToString()

            Dim customerOrder = (From orders In inventoryContext.Orders
                           Where orders.OrderID = selectedOrderId).First
            Dim customerID = customerOrder.UserID
            Dim customerInfo = DotNetNuke.Entities.Users.UserController.GetUserById(PortalSettings.PortalId, customerID)
            If customerID > -1 Then
                Dim currentCustomer =
                            (From customers In inventoryContext.Customers
                             Where customers.UserID = customerID).First

                FirstNameLabel.Text = customerInfo.Profile.FirstName.ToString & " "
                LastNameLabel.Text = customerInfo.Profile.LastName.ToString

                If currentCustomer.Street IsNot Nothing Then
                    StreetLabel.Text = currentCustomer.Street.ToString
                End If
                If currentCustomer.City IsNot Nothing Then
                    CityLabel.Text = currentCustomer.City.ToString
                End If
                If currentCustomer.StateCode IsNot Nothing Then
                    StateLabel.Text = currentCustomer.StateCode.ToString
                End If
                If currentCustomer.PostalCode IsNot Nothing Then
                    ZipLabel.Text = currentCustomer.PostalCode.ToString
                End If
            End If

            
        End Sub

        Protected Sub ShipItemButton_Click(sender As Object, e As EventArgs) Handles ShipItemButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim splitLineItem() As String = ShipLineListBox.SelectedValue.Split("~")
            Dim selectedOrderId = Integer.Parse(splitLineItem(0))
            Dim selectedProductID = Integer.Parse(splitLineItem(1))
            Dim selectedLineItem As OrderLineItem =
                (From lineItems In inventoryContext.OrderLineItems
                 Where lineItems.OrderID = selectedOrderId AndAlso lineItems.ProductID = selectedProductID).First
            selectedLineItem.IsShipped = True
            inventoryContext.SaveChanges()
            DisplayShipList()
        End Sub
#End Region
    End Class

End Namespace

