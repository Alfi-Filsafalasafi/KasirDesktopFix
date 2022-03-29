Imports System.Data.Sql
Imports System.Data.SqlClient
Module Auth
    Public mUID As String
    Public mNama As String
    Public mUsername As String
    Public mPassword As String
    Public mLevel As String
    Public mTelp As String
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader

    Public Sub mEncrypt()
        Dim security As New Security.Cryptography.MD5CryptoServiceProvider
        Dim txtPass() As Byte = System.Text.Encoding.ASCII.GetBytes(Form_login.txt_password.Text)
        Dim hash As String

        txtPass = security.ComputeHash(txtPass)
        For Each bt As Byte In txtPass
            hash &= bt.ToString("x2")
        Next
        mPassword = hash.ToString
    End Sub

    Public Sub mAuth()
        Call konek_db()
        Call mEncrypt()
        Dim username As String = Form_login.txt_usernam.Text
        Dim sql As String

        sql = "select * from t_users where username=@username and password=@password"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@username", username)
        cmd.Parameters.AddWithValue("@password", mPassword)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            mUID = dr("uid")
            mNama = dr("nama")
            mUsername = dr("username")
            mPassword = dr("password")
            mLevel = dr("id_level")
            mTelp = dr("telp")

            Form_login.Hide()
            Form_menu.Show()
        Else
            MessageBox.Show("Invalid username/password", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Module
