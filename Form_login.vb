Imports System.Data.SqlClient
Public Class Form_login

    Private Sub ck_lihat_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ck_lihat.CheckedChanged
        If ck_lihat.Checked = True Then
            txt_password.UseSystemPasswordChar = False
        Else
            txt_password.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub btn_masuk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_masuk.Click
        If txt_usernam.Text = "" Then
            MessageBox.Show("Username Harus Di isi", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        ElseIf txt_password.Text = "" Then
            MessageBox.Show("Password Harus Di isi", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Call mAuth()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Yakin akan menutup aplikasi ini?", "APL", MessageBoxButtons.YesNo)
        If dialog = Windows.Forms.DialogResult.No Then

        Else
            Application.Exit()
        End If
    End Sub

    Private Sub Form_login_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub txt_password_TextChanged(sender As Object, e As EventArgs) Handles txt_password.TextChanged

    End Sub

    Private Sub txt_password_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_password.KeyPress
        If e.KeyChar = Chr(13) Then
            btn_masuk.Focus()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Form_settings.Show()

    End Sub
End Class