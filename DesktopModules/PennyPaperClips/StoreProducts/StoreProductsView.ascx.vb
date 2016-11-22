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

Namespace PennyPaperClips.StoreProducts

    Public Partial Class StoreProductsView
        Inherits PortalModuleBase

        Private product As Product

        #Region "Event Handlers"

        Protected Overrides Sub OnInit(e As EventArgs)
            MyBase.OnInit(e)
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            If Not IsPostBack Then
                DisplayProductList()

            End If

        End Sub

        Protected Sub ProductsListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ProductsListBox.SelectedIndexChanged
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim selectedProduct As Product =
            (From products In inventoryContext.Products
             Where products.ProductID = ProductsListBox.SelectedValue).First
            Dim productLocations =
                (From storedProduct In inventoryContext.StoredProducts
                    Where storedProduct.ProductID = selectedProduct.ProductID).ToList
            Dim binList =
                (From bins In inventoryContext.StorageBins).ToList
            Dim lineItemList = (From lineItems In inventoryContext.OrderLineItems
                                Where lineItems.IsHeld = True AndAlso lineItems.ProductID = selectedProduct.ProductID).ToList
            Dim heldQuantity = 0
            For Each item In lineItemList
                heldQuantity += item.Quantity
                If item.PickedQuantity IsNot Nothing Then
                    heldQuantity -= item.PickedQuantity
                End If
            Next

            ProductIDLabel.Text = selectedProduct.ProductID.ToString()
            ProductNameLabel.Text = selectedProduct.ProductName.ToString()
            ProductDescriptionLabel.Text = selectedProduct.ProductDescription.ToString()
            NumberOnHandLabel.Text = selectedProduct.OnHand.ToString()
            NumberOnHoldLabel.Text = heldQuantity.ToString()

            CurrentBinsListBox.DataSource = productLocations
            CurrentBinsListBox.DataTextField = "Description"
            CurrentBinsListBox.DataValueField = "BinID"
            CurrentBinsListBox.DataBind()

            BinsDropDownList.DataSource = binList
            BinsDropDownList.DataTextField = "BinID"
            BinsDropDownList.DataValueField = "BinID"
            BinsDropDownList.DataBind()
        End Sub

        Protected Sub AddProductButton_Click(sender As Object, e As EventArgs) Handles AddProductButton.Click
            Dim tc As New TabController()
            Dim ti As DotNetNuke.Entities.Tabs.TabInfo = tc.GetTabByName("Manage Products", 0)
            Dim pageUrl As String = DotNetNuke.Common.Globals.NavigateURL(ti.TabID)
            Response.Redirect(pageUrl, False)
            Context.ApplicationInstance.CompleteRequest()
        End Sub

        Protected Sub PutInBinButton_Click(sender As Object, e As EventArgs) Handles PutInBinButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim newStoredProduct As StoredProduct
            Dim binIndicator = 0
            Dim selectedProduct As Product =
            (From products In inventoryContext.Products
             Where products.ProductID = ProductsListBox.SelectedValue).First
            Dim productLocations =
                (From storedProduct In inventoryContext.StoredProducts
                    Where storedProduct.ProductID = selectedProduct.ProductID).ToList
            
            Dim productID = Integer.Parse(ProductsListBox.SelectedValue)
            Dim binID = Integer.Parse(BinsDropDownList.SelectedValue)
            Dim quantity = Integer.Parse(BinQuantityTextBox.Text)

            For Each location In productLocations
                If location.BinID = binID Then
                    Dim selectedProductLocation =
                        (From storedProduct In inventoryContext.StoredProducts
                         Where storedProduct.BinID = binID).First
                    binIndicator = 1
                    selectedProductLocation.Quantity += quantity
                    selectedProduct.OnHand += quantity
                    inventoryContext.SaveChanges()
                    Exit For
                Else
                    binIndicator = 0
                End If
            Next

            If binIndicator = 0 Then
                newStoredProduct = inventoryContext.StoredProducts.Create()
                newStoredProduct.ProductID = productID
                newStoredProduct.BinID = binID
                newStoredProduct.Quantity = quantity
                selectedProduct.OnHand += quantity
                inventoryContext.StoredProducts.Add(newStoredProduct)
                inventoryContext.SaveChanges()
            End If

            ClearFields()
            'PutInBinButton.Enabled = False
            DisplayProductList()
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
#End Region

        Protected Sub RemoveFromButton_Click(sender As Object, e As EventArgs) Handles RemoveFromButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim binIndicator = 0
            Dim selectedProduct As Product =
            (From products In inventoryContext.Products
             Where products.ProductID = ProductsListBox.SelectedValue).First
            Dim productLocations =
                (From storedProduct In inventoryContext.StoredProducts
                    Where storedProduct.ProductID = selectedProduct.ProductID).ToList

            Dim productID = Integer.Parse(ProductsListBox.SelectedValue)
            Dim binID = Integer.Parse(BinsDropDownList.SelectedValue)
            Dim quantity = Integer.Parse(BinQuantityTextBox.Text)

            For Each location In productLocations
                If location.BinID = binID Then
                    Dim selectedProductLocation =
                        (From storedProduct In inventoryContext.StoredProducts
                         Where storedProduct.BinID = binID).First
                    binIndicator = 1
                    If selectedProductLocation.Quantity >= quantity Then
                        If selectedProductLocation.Quantity = quantity Then
                            inventoryContext.StoredProducts.Remove(selectedProductLocation)
                            selectedProduct.OnHand -= quantity
                            inventoryContext.SaveChanges()
                            ClearFields()
                            DisplayProductList()
                        Else
                            selectedProductLocation.Quantity -= quantity
                            selectedProduct.OnHand -= quantity
                            inventoryContext.SaveChanges()
                            ClearFields()
                            DisplayProductList()
                        End If
                    Else
                        warningLabel.Text = "You cannot remove a greater quantity than is stored in bin."
                        BinQuantityTextBox.Focus()
                    End If
                    Exit For
                Else
                    binIndicator = 0
                End If
            Next

            If binIndicator = 0 Then
                warningLabel.Text = "Select a bin where the product is stored."
            End If
        End Sub

        Private Sub ClearFields()
            ProductIDLabel.Text = ""
            ProductNameLabel.Text = ""
            ProductDescriptionLabel.Text = ""
            NumberOnHandLabel.Text = ""
            CurrentBinsListBox.Items.Clear()
            BinsDropDownList.Items.Clear()
            BinQuantityTextBox.Text = ""
            warningLabel.Text = ""
        End Sub

        Protected Sub cancelButton_Click(sender As Object, e As EventArgs) Handles cancelButton.Click
            ClearFields()
            DisplayProductList()
        End Sub
    End Class

End Namespace

