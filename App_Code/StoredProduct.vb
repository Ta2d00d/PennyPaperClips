Imports Microsoft.VisualBasic

Partial Public Class StoredProduct
    Public ReadOnly Property Description() As String
        Get
            Description = Me.Quantity.ToString("0000") & " in Bin " & Me.BinID.ToString("00")
        End Get
    End Property
End Class
