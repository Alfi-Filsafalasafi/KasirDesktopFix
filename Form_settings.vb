Imports System.Data.SqlClient
Public Class Form_settings
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Private Sub clear()
        txt_settings.Text = ""
        lblID.Text = ""
    End Sub

    Private Sub updateData()
        Call konek_db()
        Dim sql As String

        sql = "UPDATE t_settings SET setting=@setting WHERE id=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        cmd.Parameters.AddWithValue("@setting", txt_settings.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Update Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgv.Refresh()
            Call getDataServer()
            Call getDataPrinter()
            Call clear()
        Else
            MessageBox.Show("Update Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Call clear()
        End If
    End Sub

    Private Sub Form_settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getDataServer()
        Call getDataPrinter()
        Call SettingApl()
    End Sub

    Private Sub btn_simpan_Click(sender As Object, e As EventArgs) Handles btn_simpan.Click
        Call konek_db()
        Dim sql As String

        sql = "SELECT id FROM t_settings WHERE id=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            If lblID.Text = "" Or txt_settings.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Dim dialog As DialogResult
                dialog = MessageBox.Show("Are you sure?", "Update", MessageBoxButtons.YesNo)
                If dialog = Windows.Forms.DialogResult.No Then
                    Call clear()
                Else
                    Call updateData()
                End If
            End If
        Else
            If txt_settings.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                MessageBox.Show("Hanya bisa update Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If lblID.Text = "" Then
            MessageBox.Show("Tidak ada yang di hapus !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim dialog As DialogResult
            dialog = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo)
            If dialog = Windows.Forms.DialogResult.No Then
                Call clear()
            Else
                MessageBox.Show("Maaf pengaturan sudah tersetting jadi tidak bisa di delete", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call clear()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        lblID.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        txt_settings.Text = dgv.Rows(e.RowIndex).Cells(1).Value
    End Sub

    Private Sub dgvPrinter_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPrinter.CellContentClick
        lblID.Text = dgvPrinter.Rows(e.RowIndex).Cells(0).Value
        txt_settings.Text = dgvPrinter.Rows(e.RowIndex).Cells(1).Value
    End Sub
End Class