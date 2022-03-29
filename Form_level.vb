Imports System.Data.SqlClient
Public Class Form_level

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Private Sub clear()
        txt_cari.Text = ""
        txt_level.Text = ""
        lblID.Text = ""
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_level"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)

        dgv.Columns(0).HeaderText = "ID"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(0).Width = "40"
        dgv.Columns(0).ReadOnly = True
        dgv.Columns(1).ReadOnly = True
    End Sub

    Private Sub searchData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_level WHERE posisi LIKE '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
    End Sub

    Private Sub insertData()
        Call konek_db()
        Dim sql As String

        sql = "insert into t_level values(@level)"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@level", txt_level.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Sukses insert data", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgv.Refresh()
            Call getData()
            Call clear()
            cmd.Dispose()
        Else
            MessageBox.Show("Gagal insert data", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Call clear()
        End If
    End Sub

    Private Sub updateData()
        Call konek_db()
        Dim sql As String

        sql = "UPDATE t_level SET posisi=@posisi WHERE id_level=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        cmd.Parameters.AddWithValue("@posisi", txt_level.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Update Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgv.Refresh()
            Call getData()
            Call clear()
        Else
            MessageBox.Show("Update Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Call clear()
        End If
    End Sub

    Private Sub deleteData()
        Call konek_db()
        Dim sql As String

        sql = "DELETE FROM t_level WHERE id_level=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Delete Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgv.Refresh()
            Call getData()
            Call clear()
        Else
            MessageBox.Show("Delete Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Call clear()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Form_level_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
        lblID.Text = ""
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
                Call deleteData()
            End If

        End If
    End Sub

    Private Sub btn_simpan_Click(sender As Object, e As EventArgs) Handles btn_simpan.Click
        Call konek_db()
        Dim sql As String

        sql = "SELECT id_level FROM t_level WHERE id_level=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            If lblID.Text = "" Or txt_level.Text = "" Then
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
            If txt_level.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Call insertData()
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call clear()
    End Sub

    Private Sub txt_cari_TextChanged(sender As Object, e As EventArgs) Handles txt_cari.TextChanged
        Call searchData()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        lblID.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        txt_level.Text = dgv.Rows(e.RowIndex).Cells(1).Value
    End Sub
End Class