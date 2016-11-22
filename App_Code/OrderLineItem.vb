Imports Microsoft.VisualBasic

Partial Public Class OrderLineItem
    Public ReadOnly Property Description() As String
        Get
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim product =
                (From products In inventoryContext.Products
                 Where products.ProductID = Me.ProductID).First

            Description = Me.Quantity.ToString() & " of " & product.ProductName.ToString() & " at " & Me.UnitPrice.ToString("c2") & " each"
        End Get
    End Property

    Public ReadOnly Property PickDescription() As String
        Get
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
            Dim product =
                (From products In inventoryContext.Products
                 Where products.ProductID = Me.ProductID).First

            PickDescription = "Order " & Me.OrderID.ToString() & " - " & Me.Quantity.ToString() & " of Item# " &
                Me.ProductID.ToString() & " - " & product.ProductName.ToString()
        End Get
    End Property

    Public ReadOnly Property PickValue() As String
        Get
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()

            PickValue = Me.OrderID.ToString() & "~" & Me.ProductID.ToString()
        End Get
    End Property
    Public ReadOnly Property ShipValue() As String
        Get
            Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()

            ShipValue = Me.OrderID.ToString() & "~" & Me.ProductID.ToString()
        End Get
    End Property
End Class
