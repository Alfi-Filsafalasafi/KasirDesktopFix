Imports System.Data.SqlClient
Public Class Form_manajemen_barang
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim reader As SqlDataReader
    Dim kd_brng, idku As String
    Dim index As Integer

    Private Sub clear()
        txt_nama.Text = ""
        txt_barcode.Text = ""
        txt_beli.Text = ""
        txt_jual.Text = ""
        txt_stok.Text = ""
        txt_supplier.Text = "PT Aden Market"
        lblID.Text = ""
        lblIdSupplier.Text = "1"
    End Sub


    Sub get_id()
        konek_db()
        Dim tampil As New SqlCommand("select kd_barang from t_barang order by kd_barang DESC", koneksi)

        reader = tampil.ExecuteReader
        reader.Read()
        If Not reader.HasRows Then
            lblIDKd.Text = 1
        Else
            lblIDKd.Text = reader("kd_barang") + 1
        End If
    End Sub

    Private Sub column()
        dgv.Columns(0).HeaderText = "No"
        dgv.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        dgv.Columns(0).Width = "50"
        dgv.Columns(2).Width = "250"
        dgv.Columns(3).Width = "50"
        dgv.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(4).Width = "120"
        dgv.Columns(5).Width = "40"
        dgv.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10)

        dgv.Columns(0).ReadOnly = True
        dgv.Columns(1).ReadOnly = True
        dgv.Columns(2).ReadOnly = True
        dgv.Columns(3).ReadOnly = True
        dgv.Columns(4).ReadOnly = True
        dgv.Columns(5).ReadOnly = True
        dgv.Columns(6).ReadOnly = True
        dgv.Columns(7).ReadOnly = True
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,barcode,nama_barang,satuan,kategori,stok,harga_beli,harga_jual FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori order by t_barang.kd_barang DESC"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        Call column()
    End Sub

    Private Sub getDatas(id As Integer)
        Call konek_db()
        Dim sql As String

        If Not txt_cari.Text = "" Then
            sql = "SELECT kd_barang,barcode,nama_barang,satuan,kategori,stok,harga_beli,harga_jual FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier WHERE nama_barang LIKE '%" & txt_cari.Text & "%' or barcode LIKE '%" & txt_cari.Text & "%' or kd_barang LIKE '%" & txt_cari.Text & "%' or t_satuan.satuan LIKE '%" & txt_cari.Text & "%' or t_kategori.kategori LIKE '%" & txt_cari.Text & "%' "
            da = New SqlDataAdapter(sql, koneksi)
            ds = New DataSet
            da.Fill(ds, 0)
            dgv.DataSource = ds.Tables(0)
            dgv.Columns(2).Width = "150"
            dgv.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10)
            Call column()
        Else
            sql = "SELECT kd_barang,barcode,nama_barang,satuan,kategori,stok,harga_beli,harga_jual FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori order by t_barang.kd_barang DESC"
            da = New SqlDataAdapter(sql, koneksi)
            ds = New DataSet
            da.Fill(ds, 0)
            dgv.DataSource = ds.Tables(0)
            Call column()
            dgv.CurrentCell = dgv.Item("kd_barang", id - 1)
        End If
    End Sub

    Private Sub searchData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,barcode,nama_barang,satuan,kategori,stok,harga_beli,harga_jual FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier WHERE nama_barang LIKE '%" & txt_cari.Text & "%' or barcode LIKE '%" & txt_cari.Text & "%' or kd_barang LIKE '%" & txt_cari.Text & "%' or t_satuan.satuan LIKE '%" & txt_cari.Text & "%' or t_kategori.kategori LIKE '%" & txt_cari.Text & "%' order by t_barang.kd_barang DESC"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(2).Width = "150"
        dgv.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10)
        Call column()
    End Sub

    Private Sub getKategori()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_kategori"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        cb_kategori.DataSource = ds.Tables(0)
        cb_kategori.ValueMember = "id_kategori"
        cb_kategori.DisplayMember = "kategori"
    End Sub

    Private Sub getSatuan()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_satuan"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        cb_satuan.DataSource = ds.Tables(0)
        cb_satuan.ValueMember = "id_satuan"
        cb_satuan.DisplayMember = "satuan"
    End Sub

    Private Sub insertData()
        Call konek_db()
        Dim sql As String
        Dim harga_beli As Double = txt_beli.Text
        Dim harga_jual As Double = txt_jual.Text

        sql = "insert into t_barang values(@kd, @id_satuan,@id_kategori,@id_supplier,@nama_barang,@stok,@harga_beli,@harga_jual,@expired,@barcode)"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@kd", lblIDKd.Text)
        cmd.Parameters.AddWithValue("@id_satuan", cb_satuan.SelectedValue.ToString)
        cmd.Parameters.AddWithValue("@id_kategori", cb_kategori.SelectedValue.ToString)
        cmd.Parameters.AddWithValue("@id_supplier", lblIdSupplier.Text)
        cmd.Parameters.AddWithValue("@nama_barang", txt_nama.Text)
        cmd.Parameters.AddWithValue("@stok", txt_stok.Text)
        cmd.Parameters.AddWithValue("@harga_beli", txt_beli.Text)
        cmd.Parameters.AddWithValue("@harga_jual", txt_jual.Text)
        cmd.Parameters.AddWithValue("@expired", "2019-10-05")
        cmd.Parameters.AddWithValue("@barcode", txt_barcode.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Sukses insert data", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'dgv.Refresh()
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

        sql = "UPDATE t_barang SET id_satuan=@id_satuan, id_kategori=@id_kategori, nama_barang=@nama_barang, stok=@stok, harga_beli=@harga_beli, harga_jual=@harga_jual, barcode=@barcode WHERE kd_barang=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        cmd.Parameters.AddWithValue("@id_satuan", cb_satuan.SelectedValue.ToString)
        cmd.Parameters.AddWithValue("@id_kategori", cb_kategori.SelectedValue.ToString)
        cmd.Parameters.AddWithValue("@id_supplier", lblIdSupplier.Text)
        cmd.Parameters.AddWithValue("@nama_barang", txt_nama.Text)
        cmd.Parameters.AddWithValue("@stok", txt_stok.Text)
        cmd.Parameters.AddWithValue("@harga_beli", txt_beli.Text)
        cmd.Parameters.AddWithValue("@harga_jual", txt_jual.Text)
        cmd.Parameters.AddWithValue("@barcode", txt_barcode.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Update Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'dgv.Refresh()
            Call getDatas(lblID.Text)
            Call clear()
        Else
            MessageBox.Show("Update Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Call clear()
        End If
    End Sub

    Private Sub deleteData()
        Call konek_db()
        Dim sql As String

        sql = "DELETE FROM t_barang WHERE kd_barang=@id"
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

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Form_manajemen_barang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call getData()
        Call get_id()
        lblIdSupplier.Text = ""
        Call getKategori()
        lblID.Text = ""
        Call getSatuan()
        txt_barcode.Focus()
        lblIdSupplier.Text = "1"
        txt_supplier.Text = "PT Aden Market"
    End Sub

    Private Sub txt_beli_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_beli.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[+,0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            e.Handled = CBool(kata)
        End If
    End Sub

    Private Sub txt_beli_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_beli.TextChanged

    End Sub

    Private Sub txt_stok_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_stok.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[+,0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            e.Handled = CBool(kata)
        End If
    End Sub

    Private Sub txt_stok_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_stok.TextChanged

    End Sub

    Private Sub txt_jual_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_jual.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[+,0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            btn_simpan.Focus()
            e.Handled = CBool(kata)
        End If
    End Sub

    Private Sub txt_jual_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_jual.TextChanged

    End Sub

    Private Sub btn_supplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_supplier.Click
        Form_cari_supplier.Show()
    End Sub

    Private Sub btn_upload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub txt_cari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_cari.TextChanged
        Call searchData()
    End Sub

    Private Sub Form_manajemen_barang_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub btn_refresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txt_cari.Text = ""
    End Sub

    Private Sub lv_menu_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        konek_db()

    End Sub

    Private Sub lv_menu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cb_satuan_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cb_satuan.MouseClick
       
    End Sub

    Private Sub cb_satuan_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_satuan.SelectedIndexChanged

    End Sub

    Private Sub cb_satuan_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_satuan.TextChanged

    End Sub

    Private Sub cb_kategori_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_kategori.SelectedIndexChanged

    End Sub

    Private Sub cb_kategori_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cb_kategori.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btn_hapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_hapus.Click
        If lblID.Text = "" Then
            MessageBox.Show("Tidak ada yang di hapus !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim dialog As DialogResult
            dialog = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo)
            If dialog = Windows.Forms.DialogResult.No Then
                Call clear()
                Call get_id()
            Else
                Call deleteData()
                Call get_id()
            End If

        End If
    End Sub

    Private Sub btn_simpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_simpan.Click
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang FROM t_barang WHERE kd_barang=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            If lblID.Text = "" Or txt_barcode.Text = "" Or txt_beli.Text = "" Or txt_jual.Text = "" Or txt_nama.Text = "" Or txt_stok.Text = "" Or txt_supplier.Text = "" Or cb_kategori.Text = "" Or cb_satuan.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Dim dialog As DialogResult
                dialog = MessageBox.Show("Are you sure?", "Update", MessageBoxButtons.YesNo)
                If dialog = Windows.Forms.DialogResult.No Then
                    Call clear()
                    Call get_id()
                Else
                    Call updateData()
                    Call get_id()
                End If
            End If
        Else
            If txt_barcode.Text = "" Or txt_beli.Text = "" Or txt_jual.Text = "" Or txt_nama.Text = "" Or txt_stok.Text = "" Or txt_supplier.Text = "" Or cb_kategori.Text = "" Or cb_satuan.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Call insertData()
                Call get_id()
            End If
        End If
    End Sub

    Private Sub btn_batal_Click(sender As Object, e As EventArgs) Handles btn_batal.Click
        Call clear()
        Call get_id()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        'lblID.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        'txt_barcode.Text = dgv.Rows(e.RowIndex).Cells(1).Value
        'txt_nama.Text = dgv.Rows(e.RowIndex).Cells(2).Value
        'cb_satuan.Text = dgv.Rows(e.RowIndex).Cells(3).Value
        'cb_kategori.Text = dgv.Rows(e.RowIndex).Cells(4).Value
        'txt_stok.Text = dgv.Rows(e.RowIndex).Cells(5).Value
        'txt_beli.Text = dgv.Rows(e.RowIndex).Cells(6).Value
        'txt_jual.Text = dgv.Rows(e.RowIndex).Cells(7).Value
    End Sub

    Private Sub txt_barcode_TextChanged(sender As Object, e As EventArgs) Handles txt_barcode.TextChanged

    End Sub

    Private Sub txt_barcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_barcode.KeyPress
        If e.KeyChar = Chr(13) Then
            txt_nama.Focus()
        End If
    End Sub

    Private Sub txt_nama_TextChanged(sender As Object, e As EventArgs) Handles txt_nama.TextChanged

    End Sub

    Private Sub txt_nama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nama.KeyPress
        If e.KeyChar = Chr(13) Then
            txt_stok.Focus()
        End If
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectRow As DataGridViewRow
        selectRow = dgv.Rows(index)
        lblID.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        txt_barcode.Text = dgv.Rows(e.RowIndex).Cells(1).Value
        txt_nama.Text = dgv.Rows(e.RowIndex).Cells(2).Value
        cb_satuan.Text = dgv.Rows(e.RowIndex).Cells(3).Value
        cb_kategori.Text = dgv.Rows(e.RowIndex).Cells(4).Value
        txt_stok.Text = dgv.Rows(e.RowIndex).Cells(5).Value
        txt_beli.Text = dgv.Rows(e.RowIndex).Cells(6).Value
        txt_jual.Text = dgv.Rows(e.RowIndex).Cells(7).Value

    End Sub
End Class