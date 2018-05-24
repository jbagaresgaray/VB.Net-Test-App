
Option Explicit On

Imports MySql.Data.MySqlClient

Module modUsers

    Public Structure users
        Dim userId As Integer
        Dim fname As String
        Dim lname As String
        Dim mname As String
        Dim username As String
        Dim password As String
    End Structure


    Public Function CheckUsersNameExisting(ByVal fname As String, ByVal lname As String, ByVal mname As String,
                                        ByVal userId As String) As Boolean

        Dim sSQL As String = "SELECT * FROM users WHERE fname='" & fname &
                        "' AND lname='" & fname & "' AND mname='" & mname & "' "

        If Len(userId) > 0 Then
            sSQL = sSQL & "WHERE userId <> '" & userId & "' "
        End If
        sSQL = sSQL & "LIMIT 1"

        If QueryHasRows(sSQL) Then
            CheckUsersNameExisting = True
        Else
            CheckUsersNameExisting = False
        End If
    End Function

    Public Function AddUsers(ByVal E As users) As Boolean
        On Error GoTo err
        Dim con As New MySqlConnection(DB_CONNECTION_STRING)
        con.Open()
        Dim sSQL As String = "INSERT INTO users(fname,lname,mname,username,password) VALUE (@fname,@lname,@mname,@username,md5(@password))"
        Dim com As New MySqlCommand(sSQL, con)
        com.Parameters.AddWithValue("@fname", E.fname)
        com.Parameters.AddWithValue("@lname", E.lname)
        com.Parameters.AddWithValue("@mname", E.mname)
        com.Parameters.AddWithValue("@username", E.username)
        com.Parameters.AddWithValue("@password", E.password)
        com.ExecuteNonQuery()
        com.Parameters.Clear()
        con.Close()
        AddUsers = True
        Exit Function
err:
        AddUsers = False
        DisplayErrorMsg("modUsers", "AddUsers", Err.Number, Err.Description)
    End Function

    Public Function UpdateUsers(ByVal E As users) As Boolean
        On Error GoTo err
        Dim con As New MySqlConnection(DB_CONNECTION_STRING)
        con.Open()
        Dim sSQL As String = "UPDATE users SET fname=@fname,lname=@lname,mname=@mname,username=@username,password=md5(@password) WHERE userId='" & E.userId & "'"
        Dim com As New MySqlCommand(sSQL, con)
        com.Parameters.AddWithValue("@fname", E.fname)
        com.Parameters.AddWithValue("@lname", E.lname)
        com.Parameters.AddWithValue("@mname", E.mname)
        com.Parameters.AddWithValue("@username", E.username)
        com.Parameters.AddWithValue("@password", E.password)
        com.ExecuteNonQuery()
        com.Parameters.Clear()
        con.Close()
        UpdateUsers = True
        Exit Function
err:
        UpdateUsers = False
        DisplayErrorMsg("modUsers", "UpdateUsers", Err.Number, Err.Description)
    End Function

    Public Function Delete_User(ByVal userId As String) As Boolean
        If ExecuteQry("DELETE FROM users WHERE userId='" & userId & "'") Then
            Delete_User = True
        Else
            Delete_User = False
        End If
    End Function

    Public Function GetUserById(ByVal userId As Integer, ByRef E As users) As Boolean
        '  On Error GoTo err
        Dim con As New MySqlConnection(DB_CONNECTION_STRING)
        con.Open()
        Dim com As New MySqlCommand("Select E.userId,E.fname,E.lname,E.mname,E.username FROM users E WHERE E.userId = '" & userId & "'", con)
        Dim vRS As MySqlDataReader = com.ExecuteReader()
        vRS.Read()
        If vRS.HasRows Then
            With E
                .fname = vRS("fname").ToString()
                .userId = vRS("userId").ToString()
                .lname = vRS("lname").ToString()
                .mname = vRS("mname").ToString()
                .username = vRS("username").ToString()
            End With
            GetUserById = True
        Else
            GetUserById = False
        End If
        con.Close()
        'GetUserById = True
        'Exit Function
        'err:
        'GetUserById = False
        'DisplayErrorMsg("modUsers", "GetUserById", Err.Number, Err.Description)
    End Function

End Module
