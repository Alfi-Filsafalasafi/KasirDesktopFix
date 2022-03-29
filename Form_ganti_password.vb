Imports System.Data.SqlClient
Public Class Form_ganti_password
    Private oldPassword As String
    Private newPassword As String
    Dim cmd As SqlCommand

    Private Sub clear()
        txt_ketik_ul.Text = ""
        txt_pass_baru.Text = ""
        txt_pass_lama.Text = ""
    End Sub

    Private Sub newEncrypt()
        Call konek_db()
        Dim security As New Security.Cryptography.MD5CryptoServiceProvider
        Dim txtNewPass() As Byte = System.Text.Encoding.ASCII.GetBytes(txt_ketik_ul.Text)
        Dim hash As String

        txtNewPass = security.ComputeHash(txtNewPass)
        For Each bts As Byte In txtNewPass
            hash &= bts.ToString("x2")
        Next
        newPassword = hash.ToString
    End Sub

    Private Sub encrypt()
        Call konek_db()
        Dim security As New Security.Cryptography.MD5CryptoServiceProvider
        Dim txtPass() As Byte = System.Text.Encoding.ASCII.GetBytes(txt_pass_lama.Text)
        Dim hash As String

        txtPass = security.ComputeHash(txtPass)
        For Each bt As Byte In txtPass
            hash &= bt.ToString("x2")
        Next
        oldPassword = hash.ToString
    End Sub

    Private Sub updatePassword()
        Call newEncrypt()
        Call konek_db()
        Dim sql As String

        sql = "UPDATE t_users SET password=@password WHERE uid=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@password", newPassword)
        cmd.Parameters.AddWithValue("@id", mUID)

        If cmd.ExecuteNonQuery() Then
            MessageBox.Show("Change Password success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Call clear()
            Me.Close()
        Else
            MessageBox.Show("Failed Change Password", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub Form_ganti_password_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txt_user.Text = mUsername
        txt_user.Enabled = False
        txt_pass_lama.Focus()
    End Sub

    Private Sub Form_ganti_password_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub ck_lihat_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        txt_ketik_ul.UseSystemPasswordChar = Not CheckBox1.Checked
        txt_pass_baru.UseSystemPasswordChar = Not CheckBox1.Checked
        txt_pass_lama.UseSystemPasswordChar = Not CheckBox1.Checked
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Call encrypt()
        If txt_pass_lama.Text = "" Then
            MessageBox.Show("Old Password silahkan isi", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            txt_pass_lama.Focus()
        ElseIf oldPassword = mPassword Then
            If txt_pass_baru.Text = "" Or txt_ketik_ul.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            ElseIf txt_ketik_ul.Text = txt_pass_baru.Text Then
                Dim dialog As DialogResult
                dialog = MessageBox.Show("Are you sure?", "Update", MessageBoxButtons.YesNo)
                If dialog = Windows.Forms.DialogResult.No Then
                    txt_ketik_ul.Text = ""
                    txt_pass_baru.Text = ""
                    txt_pass_lama.Text = ""
                Else
                    Call updatePassword()
                End If
            Else
                    MessageBox.Show("Confrim Password Tidak Cocok", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                txt_ketik_ul.Text = ""
                txt_ketik_ul.Focus()
            End If
        Else
            MessageBox.Show("Old Password Salah", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub

    Private Sub txt_pass_lama_TextChanged(sender As Object, e As EventArgs) Handles txt_pass_lama.TextChanged

    End Sub

    Private Sub txt_pass_lama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_pass_lama.KeyPress
        If e.KeyChar = Chr(13) Then
            txt_pass_baru.Focus()
        End If
    End Sub

    Private Sub txt_pass_baru_TextChanged(sender As Object, e As EventArgs) Handles txt_pass_baru.TextChanged

    End Sub

    Private Sub txt_pass_baru_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_pass_baru.KeyPress
        If e.KeyChar = Chr(13) Then
            txt_ketik_ul.Focus()
        End If
    End Sub

    Private Sub txt_ketik_ul_TextChanged(sender As Object, e As EventArgs) Handles txt_ketik_ul.TextChanged

    End Sub

    Private Sub txt_ketik_ul_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_ketik_ul.KeyPress
        If e.KeyChar = Chr(13) Then
            Button1.Focus()
        End If
    End Sub
End Class