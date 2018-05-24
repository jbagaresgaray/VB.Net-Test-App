Public Class frmUserList
    Private Sub frmUserList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FillListView("SELECT fname,lname,mname,username,userId FROM users;", lvUsers)
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        FillListView("SELECT fname,lname,mname,username,userId FROM users;", lvUsers)
    End Sub

    Private Sub NewToolStripButton_Click(sender As Object, e As EventArgs) Handles NewToolStripButton.Click
        Dim frm As New frmUserAE
        frm.ShowAddForm()
    End Sub

    Private Sub AddToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddToolStripMenuItem.Click
        Dim frm As New frmUserAE
        frm.ShowAddForm()

        RefreshToolStripMenuItem_Click(Me, New System.EventArgs())
    End Sub

    Private Sub UpdateToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateToolStripMenuItem.Click
        If lvUsers.Items.Count < 1 Then Exit Sub
        If lvUsers.SelectedItems.Count < 1 Then Exit Sub

        Dim frm As New frmUserAE
        frm.ShowUpdateForm(lvUsers.FocusedItem.SubItems(4).Text)
        RefreshToolStripMenuItem_Click(Me, New System.EventArgs())
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If lvUsers.Items.Count < 1 Then Exit Sub
        If lvUsers.SelectedItems.Count < 1 Then Exit Sub

        If MsgBox("Are you sure to delete this user?", vbQuestion + vbYesNo) = vbNo Then Exit Sub
        If Delete_User(lvUsers.FocusedItem.SubItems(4).Text) Then
            RefreshToolStripMenuItem_Click(Me, New System.EventArgs())
            MsgBox("Record successfully deleted", vbInformation)
        End If
    End Sub

    Private Sub cmdSearchButton_Click(sender As Object, e As EventArgs) Handles cmdSearchButton.Click
        If Len(txtSearch.Text) < 1 Then Exit Sub
        FillListView("SELECT fname,lname,mname,username,userId FROM users WHERE fname LIKE '%" & txtSearch.Text &
                     "%' OR mname LIKE '%" & txtSearch.Text & "%' OR lname LIKE '%" & txtSearch.Text & "%' OR username LIKE '%" & txtSearch.Text & "%';", lvUsers)
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            cmdSearchButton_Click(Me, New System.EventArgs())
        End If
    End Sub
End Class
