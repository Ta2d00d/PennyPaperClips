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

Namespace PennyPaperClips.Workers

    Public Partial Class WorkersView
        Inherits PortalModuleBase

        #Region "Event Handlers"

        Protected Overrides Sub OnInit(e As EventArgs)
            MyBase.OnInit(e)
        End Sub

        Protected Overrides Sub OnLoad(e As EventArgs)
            MyBase.OnLoad(e)

            'If Not Page.IsPostBack Then
            '    txtField.Text = DirectCast(Settings("field"), String)
            'End If
        End Sub

        'Protected Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        '    Dim controller As New ModuleController()
        '    controller.UpdateModuleSetting(ModuleId, "field", txtField.Text)
        '    Skins.Skin.AddModuleMessage(Me, "Update Successful", Skins.Controls.ModuleMessage.ModuleMessageType.GreenSuccess)
        'End Sub

        'Protected Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click

        'End Sub

        #End Region

        Protected Sub StoreProductsButton_Click(sender As Object, e As EventArgs) Handles StoreProductsButton.Click
            Dim tc As New TabController()
            Dim ti As DotNetNuke.Entities.Tabs.TabInfo = tc.GetTabByName("Store Products", 0)
            Dim pageUrl As String = DotNetNuke.Common.Globals.NavigateURL(ti.TabID)
            Response.Redirect(pageUrl, False)
            Context.ApplicationInstance.CompleteRequest()
        End Sub

        Protected Sub ManageProductsButton_Click(sender As Object, e As EventArgs) Handles ManageProductsButton.Click
            Dim tc As New TabController()
            Dim ti As DotNetNuke.Entities.Tabs.TabInfo = tc.GetTabByName("Manage Products", 0)
            Dim pageUrl As String = DotNetNuke.Common.Globals.NavigateURL(ti.TabID)
            Response.Redirect(pageUrl, False)
            Context.ApplicationInstance.CompleteRequest()
        End Sub

        Protected Sub PickAndShipButton_Click(sender As Object, e As EventArgs) Handles PickAndShipButton.Click
            Dim tc As New TabController()
            Dim ti As DotNetNuke.Entities.Tabs.TabInfo = tc.GetTabByName("Pick and Ship", 0)
            Dim pageUrl As String = DotNetNuke.Common.Globals.NavigateURL(ti.TabID)
            Response.Redirect(pageUrl, False)
            Context.ApplicationInstance.CompleteRequest()
        End Sub
    End Class

End Namespace

