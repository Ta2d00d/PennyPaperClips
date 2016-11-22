Imports Microsoft.VisualBasic

Partial Public Class Order

    Public Shared Function GetOrderForSession() As Order
        Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
        Dim orderNum As Integer = CType(HttpContext.Current.Session("OrderID"), Integer)
        Dim user As Integer = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo.UserID
        Dim newCustIndicator = 0
        Dim order As New Order
        If orderNum = 0 Then
            Dim newOrderID As Integer = 0
            Dim orderList = (From orders In inventoryContext.Orders
                     Order By orders.OrderID
                     Select orders.OrderID).ToList

            If orderList.Count > 0 Then
                newOrderID = (Aggregate orderIDs In inventoryContext.Orders
                             Into Max(orderIDs.OrderID)) + 1
            End If

            If user > -1 Then
                Customer.GetCustomerForSession()

                order = inventoryContext.Orders.Create()
                order.OrderID = newOrderID
                order.UserID = user
                order.Date = Date.Now
                inventoryContext.Orders.Add(order)
                inventoryContext.SaveChanges()
                HttpContext.Current.Session.Add("OrderID", newOrderID)
                Return order
            End If

        Else
            order = (From orders In inventoryContext.Orders.Include("OrderLineItems")
                        Where orders.OrderID = orderNum).First
            Return order
        End If
        Return order
    End Function

End Class
