Imports Microsoft.VisualBasic

Partial Public Class Customer
    Public Shared Function GetCustomerForSession() As Customer
        Dim inventoryContext = ContextHelper(Of InventoryTrackingEntities).GetCurrentContext()
        Dim user As Integer = DotNetNuke.Entities.Users.UserController.GetCurrentUserInfo.UserID
        Dim customer As New Customer
        Dim newCustIndicator = 0

        If user > -1 Then
            Dim customerList =
                (From customers In inventoryContext.Customers
                 Select customers.UserID).ToList
            If customerList.Count > 0 Then
                For Each cust In customerList
                    If cust = user Then
                        newCustIndicator = 1
                        Exit For
                    Else
                        newCustIndicator = 0
                    End If
                Next
            Else

                newCustIndicator = 0
            End If
            If newCustIndicator = 0 Then
                customer = inventoryContext.Customers.Create()
                customer.UserID = user
                inventoryContext.Customers.Add(customer)
                inventoryContext.SaveChanges()
                Return customer
            Else
                customer = (From customers In inventoryContext.Customers
                                Where customers.UserID = user).First
                Return customer
            End If
        End If
        Return customer
    End Function
End Class
