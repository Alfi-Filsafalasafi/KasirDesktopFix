Imports System.Data.SqlClient
Public Class Form_ecomerce_stok
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader

    Private Sub clear()
        txt_beli.Text = ""
        txt_nominal.Text = ""
        cb_jenis.Text = ""
        lblID.Text = ""
        txt_jual.Text = ""
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "select id_stok,kategori,nominal,harga_beli,harga_jual,stok from t_ecomerce_stok inner join t_ecomerce_jenis on t_ecomerce_stok.id_jenis = t_ecomerce_jenis.id_jenis"
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

        sql = "select id_stok,kategori,nominal,harga_beli,harga_jual,stok from t_ecomerce_stok inner join t_ecomerce_jenis on t_ecomerce_stok.id_jenis = t_ecomerce_jenis.id_jenis where kategori like '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "30"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub getJenis()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_ecomerce_jenis"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        cb_jenis.DataSource = ds.Tables(0)
        cb_jenis.ValueMember = "id_jenis"
        cb_jenis.DisplayMember = "kategori"
    End Sub

    Private Sub insertData()
        Call konek_db()
        Dim sql As String
        sql = "insert into t_ecomerce_stok values(@id_jenis,@nominal,@harga_beli,@harga_jual)"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("id_jenis", cb_jenis.SelectedValue.ToString)
        cmd.Parameters.AddWithValue("nominal", txt_nominal.Text)
        cmd.Parameters.AddWithValue("harga_beli", txt_beli.Text)
        cmd.Parameters.AddWithValue("harga_jual", txt_jual.Text)
        cmd.ExecuteNonQuery()
        dgv.Refresh()
        Call getData()
        Call clear()
    End Sub

    Private Sub updateData()
        Call konek_db()
        Dim sql As String
        sql = "update t_ecomerce_stok set id_jenis=@id_jenis, nominal=@nominal, harga_beli=@harga_beli, harga_jual=@harga_jual  where id_stok=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("id_jenis", cb_jenis.SelectedValue.ToString)
        cmd.Parameters.AddWithValue("nominal", txt_nominal.Text)
        cmd.Parameters.AddWithValue("harga_beli", txt_beli.Text)
        cmd.Parameters.AddWithValue("harga_jual", txt_jual.Text)
        cmd.Parameters.AddWithValue("id", lblID.Text)
        cmd.ExecuteNonQuery()
        dgv.Refresh()
        Call getData()
        Call clear()
    End Sub

    Private Sub deleteData()
        Call konek_db()
        Dim sql As String
        sql = "delete from t_ecomerce_stok where id_stok=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("id", lblID.Text)
        cmd.ExecuteNonQuery()
        dgv.Refresh()
        Call getData()
        Call clear()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form_ecomerce.Enabled = True
        Me.Close()
    End Sub

    Private Sub Form_ecomerce_stok_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form_ecomerce.Enabled = False
        Call getData()
        lblID.Text = ""
        Call getJenis()
        txt_nominal.Focus()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        lblID.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        cb_jenis.Text = dgv.Rows(e.RowIndex).Cells(1).Value
        txt_beli.Text = dgv.Rows(e.RowIndex).Cells(3).Value
        txt_nominal.Text = dgv.Rows(e.RowIndex).Cells(2).Value
        txt_jual.Text = dgv.Rows(e.RowIndex).Cells(4).Value
        txt_nominal.Focus()
    End Sub

    Private Sub btn_masuk_Click(sender As Object, e As EventArgs) Handles btn_masuk.Click
        Call konek_db()
        Dim sql As String

        sql = "SELECT id_stok FROM t_ecomerce_stok WHERE id_stok=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            If lblID.Text = "" Or txt_jual.Text = "" Or txt_beli.Text = "" Or txt_nominal.Text = "" Then
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
            If txt_jual.Text = "" Or txt_beli.Text = "" Or txt_nominal.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Call insertData()
            End If
        End If
    End Sub

    Private Sub txt_stok_TextChanged(sender As Object, e As EventArgs) Handles txt_beli.TextChanged

    End Sub

    Private Sub txt_stok_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_beli.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            btn_masuk.Focus()
            e.Handled = CBool(kata)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call clear()
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
            End If

        End If
    End Sub

    Private Sub txt_nominal_TextChanged(sender As Object, e As EventArgs) Handles txt_nominal.TextChanged

    End Sub

    Private Sub txt_nominal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nominal.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            btn_masuk.Focus()
            e.Handled = CBool(kata)
        End If
    End Sub

    Private Sub cb_jenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_jenis.SelectedIndexChanged
        txt_nominal.Focus()
    End Sub

    Private Sub txt_cari_TextChanged(sender As Object, e As EventArgs)
        Call searchData()
    End Sub
End Class