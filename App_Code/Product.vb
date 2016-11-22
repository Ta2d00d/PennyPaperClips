Imports Microsoft.VisualBasic

Partial Public Class Product
    Public ReadOnly Property Description() As String
        Get
            Description = Me.ProductID.ToString("0000") & " " & Me.ProductName.ToString()
        End Get
    End Property
End Class
