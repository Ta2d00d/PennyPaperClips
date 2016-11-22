Imports Microsoft.VisualBasic
'Imports System.Data.Objects
Imports System.Data.Entity

'Public Class ContextHelper(Of EntityContext As {New, ObjectContext})           'VS2010
Public Class ContextHelper(Of EntityContext As {New, DbContext})

    Private Shared contextTypeKey As String

    Public Shared Function GetCurrentContext() As EntityContext
        Dim httpContext As HttpContext = httpContext.Current
        If httpContext IsNot Nothing Then
            'contextTypeKey = "ObjectContext" & GetType(EntityContext).Name     'VS2010
            contextTypeKey = "DbContext" & GetType(EntityContext).Name
            If httpContext.Items(contextTypeKey) Is Nothing Then
                httpContext.Items.Add(contextTypeKey, New EntityContext())
            End If

            Return CType(httpContext.Items(contextTypeKey), EntityContext)
        End If
        Throw New ApplicationException("There is no Http Context available")
    End Function

    Public Shared Sub Dispose()
        Dim entityContext = CType(HttpContext.Current.Items(contextTypeKey), EntityContext)
        If entityContext IsNot Nothing Then
            entityContext.Dispose()
        End If
    End Sub

End Class


