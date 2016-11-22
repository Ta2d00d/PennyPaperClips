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

Namespace PennyPaperClips.ManageProducts

    Public Partial Class ManageProductsView
        Inherits PortalModuleBase


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
            ProductIDTextBox.Text = selectedProduct.ProductID.ToString()
            ProductNameTextBox.Text = selectedProduct.ProductName.ToString()
            ProductDescriptionTextBox.Text = selectedProduct.ProductDescription.ToString()
            UnitPriceTextBox.Text = selectedProduct.UnitPrice.ToString()
            NumberOnHandLabel.Text = selectedProduct.OnHand.ToString()
            If selectedProduct.UPC = Nothing Then
                UPCTextBox.Text = ""
            Else
                UPCTextBox.Text = selectedProduct.UPC.ToString()
            End If

        End Sub
#End Region

        Protected Sub UpdateProductButton_Click(sender As Object, e As EventArgs) Handles UpdateProductButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim selectedProduct As Product =
            (From products In inventoryContext.Products
             Where products.ProductID = ProductsListBox.SelectedValue).First
            Dim productID = CInt(ProductIDTextBox.Text)
            Dim productName = ProductNameTextBox.Text
            Dim productDescription = ProductDescriptionTextBox.Text
            Dim unitPrice = CDec(UnitPriceTextBox.Text)
            Dim upc = UPCTextBox.Text
            selectedProduct.ProductID = productID
            selectedProduct.ProductName = productName
            selectedProduct.ProductDescription = productDescription
            selectedProduct.UnitPrice = unitPrice
            If upc <> String.Empty Then
                selectedProduct.UPC = upc
            Else
                selectedProduct.UPC = Nothing
            End If
            inventoryContext.SaveChanges()
        End Sub

        Protected Sub AddProductButton_Click(sender As Object, e As EventArgs) Handles AddProductButton.Click
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim newProduct As Product
            Dim idIndicator = 0
            Dim productList =
                    (From products In inventoryContext.Products
                     Order By products.ProductName
                     Select products.ProductID, products.ProductName).ToList
            For Each product In productList
                If product.ProductID = CInt(ProductIDTextBox.Text) Then
                    warningLabel.Text = "This Product ID is already being used. Please choose a different Product ID."
                    idIndicator = 1
                    Exit For
                Else
                    warningLabel.Text = ""
                    idIndicator = 0
                End If
            Next
            If idIndicator = 1 Then
                ProductIDTextBox.Focus()
            Else
                Dim productID = Integer.Parse(ProductIDTextBox.Text)
                Dim productName = ProductNameTextBox.Text
                Dim productDescription = ProductDescriptionTextBox.Text
                Dim unitPrice = Decimal.Parse(UnitPriceTextBox.Text)
                Dim upc = UPCTextBox.Text
                newProduct = inventoryContext.Products.Create()
                newProduct.ProductID = productID
                newProduct.ProductName = productName
                newProduct.ProductDescription = productDescription
                newProduct.UnitPrice = unitPrice
                newProduct.OnHand = 0
                If upc <> String.Empty Then
                    newProduct.UPC = upc
                Else
                    newProduct.UPC = Nothing
                End If
                newProduct.ProductImageUrl = Nothing
                inventoryContext.Products.Add(newProduct)
                inventoryContext.SaveChanges()
                DisplayProductList()
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

        Protected Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
            DisplayProductList()
        End Sub
    End Class

End Namespace

