Imports System.Data.SqlClient
Public Class Form_ecomerce_jenis

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader

    Private Sub clear()
        txt_stok.Text = ""
        txt_kategori.Text = ""
        lblID.Text = ""
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "select * from t_ecomerce_jenis"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "30"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub searchData()
        Call konek_db()
        Dim sql As String

        sql = "select * from t_ecomerce_jenis where kategori like '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "30"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub insertData()
        Call konek_db()
        Dim sql As String
        sql = "insert into t_ecomerce_jenis values(@kategori,@stok)"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("kategori", txt_kategori.Text)
        cmd.Parameters.AddWithValue("stok", txt_stok.Text)
        cmd.ExecuteNonQuery()
        dgv.Refresh()
        Call getData()
        Call clear()
    End Sub

    Private Sub updateData()
        Call konek_db()
        Dim sql As String
        sql = "update t_ecomerce_jenis set kategori=@kategori, stok=@stok  where id_jenis=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("kategori", txt_kategori.Text)
        cmd.Parameters.AddWithValue("stok", txt_stok.Text)
        cmd.Parameters.AddWithValue("id", lblID.Text)
        cmd.ExecuteNonQuery()
        dgv.Refresh()
        Call getData()
        Call clear()
    End Sub

    Private Sub deleteData()
        Call konek_db()
        Dim sql As String
        sql = "delete from t_ecomerce_jenis where id_jenis=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("id", lblID.Text)
        cmd.ExecuteNonQuery()
        dgv.Refresh()
        Call getData()
        Call clear()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub txt_stok_TextChanged(sender As Object, e As EventArgs) Handles txt_stok.TextChanged

    End Sub

    Private Sub txt_stok_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_stok.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            btn_masuk.Focus()
            e.Handled = CBool(kata)
        End If
    End Sub

    Private Sub btn_masuk_Click(sender As Object, e As EventArgs) Handles btn_masuk.Click
        Call konek_db()
        Dim sql As String

        sql = "SELECT id_jenis FROM t_ecomerce_jenis WHERE id_jenis=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            If lblID.Text = "" Or txt_stok.Text = "" Or txt_kategori.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Dim dialog As DialogResult
                dialog = MessageBox.Show("Are you sure?", "Update", MessageBoxButtons.YesNo)
                If dialog = Windows.Forms.DialogResult.No Then
                    Call clear()
                Else
                    Call updateData()
                    MessageBox.Show("Success", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Else
            If txt_stok.Text = "" Or txt_kategori.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Call insertData()
                MessageBox.Show("Success", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If lblID.Text = "" Then
            MessageBox.Show("Tidak ada yang di hapus !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim dialog As DialogResult
            dialog = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo)
            If dialog = Windows.Forms.DialogResult.No Then
                Call clear()
            Else
                Call deleteData()
                MessageBox.Show("Success", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        txt_kategori.Text = dgv.Rows(e.RowIndex).Cells(1).Value
        txt_stok.Text = dgv.Rows(e.RowIndex).Cells(2).Value
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form_ecomerce.Enabled = True
        Me.Close()
    End Sub

    Private Sub Form_ecomerce_jenis_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
        lblID.Text = ""
        txt_kategori.Focus()
        Form_ecomerce.Enabled = False
    End Sub

    Private Sub txt_kategori_TextChanged(sender As Object, e As EventArgs) Handles txt_kategori.TextChanged

    End Sub
End Class