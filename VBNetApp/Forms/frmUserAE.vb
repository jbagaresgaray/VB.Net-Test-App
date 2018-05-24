Public Class frmUserAE
    Dim State As FormState
    Dim U As users

    Private Sub ClearItems()
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtFirstName.Text = ""
        txtLastName.Text = ""
        txtMiddleName.Text = ""
    End Sub

    Private Sub EnableControls()
        txtUsername.Enabled = True
        txtPassword.Enabled = True
        txtFirstName.Enabled = True
        txtMiddleName.Enabled = True
        txtLastName.Enabled = True
    End Sub
    Private Sub DisableControls()
        txtUsername.Enabled = False
        txtPassword.Enabled = False
        txtFirstName.Enabled = False
        txtMiddleName.Enabled = False
        txtLastName.Enabled = False
    End Sub

    Public Sub ShowAddForm()
        State = FormState.addFormState
        ClearItems()
        EnableControls()
        Me.ShowDialog()
    End Sub

    Public Sub ShowUpdateForm(ByVal userId As Integer)
        ClearItems()
        If GetUserById(userId, U) Then
            txtFirstName.Text = U.fname
            txtMiddleName.Text = U.mname
            txtLastName.Text = U.lname
            txtUsername.Text = U.username

            txtPassword.Enabled = False
            txtPassword.ReadOnly = True
            txtPassword.Text = ""

            State = FormState.updateFormState
        End If
        Me.ShowDialog()
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        Me.Close()
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        If Not CheckTextBox(txtUsername, "Please enter Username") Then txtUsername.Focus() : Exit Sub

        If (State = FormState.addFormState) Then
            If Not CheckTextBox(txtPassword, "Please enter Password") Then txtPassword.Focus() : Exit Sub
        End If

        If Not CheckTextBox(txtFirstName, "Please enter Firstname") Then txtFirstName.Focus() : Exit Sub
        If Not CheckTextBox(txtMiddleName, "Please enter Middlename") Then txtMiddleName.Focus() : Exit Sub
        If Not CheckTextBox(txtLastName, "Please enter Lastname") Then txtLastName.Focus() : Exit Sub


        With U
            .username = txtUsername.Text
            .fname = txtFirstName.Text
            .mname = txtMiddleName.Text
            .lname = txtLastName.Text
        End With

        Select Case State
            Case FormState.addFormState
                U.password = txtPassword.Text

                If AddUsers(U) Then
                    MsgBox("Record Successfully Saved", vbInformation, Application.ProductName)
                    ClearItems()
                    DisableControls()

                    Me.Close()
                Else
                    MsgBox("WARNING: An unknown error occured while saving the record", vbExclamation)
                    Exit Sub
                End If
            Case FormState.updateFormState
                If UpdateUsers(U) Then
                    MsgBox("Record Successfully Updated", vbInformation, Application.ProductName)
                    ClearItems()
                    DisableControls()

                    Me.Close()
                Else
                    MsgBox("WARNING: An unknown error occured while saving the record", vbExclamation)
                    Exit Sub
                End If
        End Select
    End Sub
End Class